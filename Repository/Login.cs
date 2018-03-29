using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;
using CommonSha;
using System.Web;
using System.Transactions;
using Service;
namespace Repository
{
    public class LoginService
    {
        //查询CRM用户表是否有登录用户
        public Result IsLogin(string name, string pwd)
        {
            //HttpCookie _cookie = new HttpCookie("User");
            Result result = new Result();
            var Crmuer = LoginSQL.SearchCrmuser(name);
            var Firstuesr = LoginSQL.Searchfirstuser(name);
            int trytime1 = LoginSQL.SearchCRM_Tryon();
            string guid = Guid.NewGuid().ToString("N");
            CRM_User cu = new CRM_User();
            pwd = encrypt.Sha256(pwd);
            if (Crmuer == null)         //------------------------当在二级用户表找不到该用户时
            {
                if (Firstuesr == null) //-------------------------当一级用户找不到该用户时
                {
                    result.state = -1;
                    result.msg = "账号不存在！";
                }
                else if (Firstuesr.PWD == pwd)  //-----------------当一级用户登录成功时候（默认为试用）
                {
                    cu.ID = Firstuesr.ID;
                    cu.T_name = "管理员";
                    cu.T_phone = Firstuesr.mobile;
                    cu.T_accountnumber = Firstuesr.name;
                    cu.T_pwd = Firstuesr.PWD;
                    cu.T_UserID = Firstuesr.ID;
                    cu.T_UserRole = 1;
                    cu.T_role = 1;
                    cu.T_Usekind = 1;
                    if (Firstuesr.role == 10)  //-------------------我们自己平台的管理员
                    {
                        cu.T_Usekind = 10;
                        using (TransactionScope sc = new TransactionScope())
                        {
                            if (LoginSQL.AddCRMforfirst(cu))
                            {
                                var now = DateTime.Now.ToString();
                                if (TokenTableSQL.UpdataToken(name, guid, now, "1", ""))
                                {
                                    sc.Complete();
                                    result.state = 8;
                                }
                                else
                                {
                                    result.state = 3;
                                    result.msg = "对不起，登录失败请重新操作！";
                                }
                            }
                        }
                    }
                    else
                    {
                        ////
                        CRM_trial ct = new CRM_trial();
                        ct.ID = Guid.NewGuid().ToString("N");
                        ct.UserID = Firstuesr.ID;
                        if (trytime1 == 0)
                        {
                            result.state = 3;
                            result.msg = "对不起，您还不是本平台的会员！";
                            return result;
                        }
                        ct.endTime = DateTime.Now.AddDays(trytime1);///////试用给trytime1天！
                        using (TransactionScope sc = new TransactionScope())
                        {
                            if (LoginSQL.AddCRMforfirst(cu) && LoginSQL.AddCRMtrial(ct))//------------将一级用户信息添加到二级，将一级用的ID添加到试用表
                            {
                                var now = DateTime.Now.ToString();
                                if (TokenTableSQL.UpdataToken(name, guid, now, "1", ""))
                                {
                                    sc.Complete();
                                    result.state = 8;
                                }
                                else
                                {
                                    result.state = 3;
                                    result.msg = "对不起，登录失败请重新操作！";
                                }
                            }
                            else
                            {
                                result.state = 3;
                                result.msg = "对不起，登录失败请重新操作！";
                            }
                        }
                    }

                }
                else
                {
                    result.state = -2;
                    result.msg = "密码错误！";
                }
            }
            else if (Crmuer.T_pwd == pwd)  //当二级用户登录成时
            {
                var trialend = new CRM_trial();
                var UserID = Crmuer.T_UserID;
                var usekind = 0;
                if (Crmuer.T_UserRole == 1)   ///-------------------------------当角色为管理员的时候
                {
                    if (Crmuer.T_status == 2)
                    {
                        result.state = -2;
                        result.msg = "帐号被冻结！";
                        return result;
                    }
                    usekind = Crmuer.T_Usekind;//------------------------------拿到用户类型（1.试用  2.付费）
                    if (usekind == 10) //自己平台第二次登录(永久的用户)
                    {
                        result.state = 7;
                    }
                }
                else if (Crmuer.T_UserRole == 2)   //-----------------------------------当角色为子帐号的时候
                {
                    var Crmuserinfor = LoginSQL.SearchCrmByID(UserID); //----------------通过父级id拿到父级用户类型
                    if (Crmuserinfor == null)
                    {
                        result.state = -2;
                        result.msg = "帐号异常！";
                        return result;
                    }
                    usekind = Crmuserinfor.T_Usekind;  //------------------------------拿到用户类型（1.试用  2.付费）
                    if (usekind == 10)
                    {
                        result.state = 7;
                    }
                }
                ///////////
                if (usekind == 1)   //-------------------当用户类型为试用
                {

                    trialend = LoginSQL.Searchtrialend(UserID);
                    if (trialend == null)
                    {
                        result.state = 10;
                        result.msg = "服务异常！";
                    }
                    var triaendtime = trialend.endTime;
                    var nowtime = DateTime.Now;
                    if (triaendtime <= nowtime) //-----当现在时候大于等于试用截至时间
                    {
                        result.state = 5;
                        result.msg = "对不起您的用户试用到期！";
                    }
                    else
                    {
                        var ss = triaendtime - nowtime;
                        if (ss.Days <= 3)
                        {
                            if (ss.Days == 0)
                            {
                                result.msg = "您的试用已到期，如需继续使用请联系平台客服！";
                            }
                            else
                            {
                                result.msg = "您为试用类型，目前距离到期时间还有" + ss.Days + "天，如需继续使用请联系平台客服！";
                            }
                        }
                        else
                        {
                            result.msg = "登录成功！";
                        }
                        result.state = 7;
                    }
                }
                else if (usekind == 2)   //-----------当用户类型为付费
                {
                    var Payend = LoginSQL.searchPayend(UserID);
                    if (Payend == null)
                    {
                        result.state = 10;
                        result.msg = "服务异常！";
                    }
                    else
                    {
                        var payendtime = Payend.endtime;
                        var nowtime = DateTime.Now;
                        if (payendtime <= nowtime)  //---------当前时间大于等于付费试用截至时间
                        {
                            result.state = 6;
                            result.msg = "对不起您的用户使用到期！";
                        }
                        else
                        {
                            var payss = payendtime - nowtime;
                            if (payss.Days <= 3)
                            {
                                if (payss.Days == 0)
                                {
                                    result.msg = "您的试用已到期，如需继续使用请联系平台客服！";
                                }
                                else
                                {
                                    result.msg = "您的使用时间还有" + payss.Days + "天，如需继续使用请联系平台客服！";
                                }
                            }
                            else
                            {
                                result.msg = "登录成功！";
                            }
                            result.state = 7;
                        }
                    }

                }
            }
            else
            {
                result.state = -2;
                result.msg = "密码错误！";
            }
            if (result.state == 8)
            {
                result.state = 1;
                result.msg = "开始试用，试用时间为" + trytime1 + "天！";
                cu.T_pwd = null;
                result.data = cu;
                result.guid = guid;
                //_cookie.Values.Add("UserName", name);//登录的账号
                //_cookie.Values.Add("Name", cu.T_name);//保存的姓名
                //_cookie.Values.Add("Token", guid);
                //_cookie.Values.Add("ID", cu.ID);
            }
            else if (result.state == 7)
            {
                var now = DateTime.Now.ToString();
                string crmrole = Crmuer.T_role.ToString();
                if (TokenTableSQL.UpdataToken(name, guid, now, crmrole, ""))
                {
                    result.state = 2;
                    Crmuer.T_pwd = null;
                    string url = Crmuer.T_photoUrl.ToString();
                    var ids = url.Split(',');
                    string one = ids[0];
                    string two = "";
                    string three = "";
                    if (ids.Length == 2)
                    {
                        two = ids[1];
                    }
                    else if (ids.Length == 3)
                    {
                        two = ids[1];
                        three = ids[2];
                    }
                    PicturesServiceser pse = new PicturesServiceser();
                    var picrlist = pse.SelAll(one, two, three);
                    Crmuer.T_photoUrl = picrlist;
                    result.data = Crmuer;
                    result.guid = guid;
                    //_cookie.Values.Add("UserName", name); //登录的账号
                    //_cookie.Values.Add("Name", cu.T_name);//保存的姓名
                    //_cookie.Values.Add("Token", guid);
                    //_cookie.Values.Add("ID", Crmuer.ID);
                    //result.msg = "登录成功！";
                }
                else
                {
                    result.state = 3;
                    result.msg = "对不起，登录失败请重新操作！";
                }
            }
            //_cookie.Expires = DateTime.Now.AddDays(10);
            return result;
        }
    }
}
