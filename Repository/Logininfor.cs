using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;
using Service;
namespace Repository
{
    public class Logininfor
    {
        TokenTableService tos = new TokenTableService();
        SAuthoriza saa = new SAuthoriza();
        ////验证登录状态
        //public Result R_loginstate(string Username, string Token)
        //{


        //    Result result = new Result();
        //    EAuthentication eau = new EAuthentication();
        //    eau = saa.Authentication(Username, Token);
        //    if (!eau.state)  //------------------与token表对比  
        //    {
        //        result.state = -1;
        //        result.msg = "未登录";
        //    }
        //    else
        //    {
        //        result.state = 1;
        //        result.msg = eau.name;  //---------登录的账号
        //        result.guid = eau.token; //--------登录后的token
        //    }
        //    return result;
        //}

        //app传cookie与返回新guid
        public Result R_Changetoken(string Username, string Token)
        {
            Result result = new Result();
            EAuthentication eau = new EAuthentication();
            eau = saa.Authentication(Username, Token);
            if (!eau.state)
            {
                result.state = -1;
                result.msg = "未登录";
            }
            else
            {
                DateTime now = DateTime.Now;
                DateTime lasttime = DateTime.Parse(eau.loginTime);
                int resulttime = (now - lasttime).Days;
                if (resulttime >= 10)
                {
                    result.state = 2;
                    result.msg = "用户长时间未登录，请重新登录！";

                }
                else
                {
                    string newToken = Guid.NewGuid().ToString("N");
                    if (tos.UpdataToken(eau.name, newToken, now.ToString(), eau.Role, ""))
                    {
                        CRM_User cu = LoginSQL.SearchCrmuser(eau.name);
                        if (cu==null)
                        {
                            result.state = 3;
                            result.msg = "未找到该用户！";
                        }
                        else
                        {
                            if (cu.T_photoUrl == null)
                            {
                                cu.T_photoUrl = "";
                            }
                            else
                            {
                                string url = cu.T_photoUrl.ToString();
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
                                cu.T_photoUrl = picrlist;
                            }
                            result.state = 1;
                            result.guid = newToken;
                            cu.T_pwd = null;
                            result.data = cu;
                        }
                        
                    }
                }

            }
            return result;
        }  



    }
}
