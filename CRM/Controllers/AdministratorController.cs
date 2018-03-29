using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Entity;
using CommonSha;
namespace CRM.Controllers
{
    public class AdministratorController : Controller
    {
        //
        // GET: /Administrator/
        SAuthoriza sa = new SAuthoriza();
        AdministratorService ads = new AdministratorService();

        #region //管理员添加子账号
        public ActionResult Addson(string Username, string Token, string UserID, string name, string demartment, string duct, string phone, string jobnumber, string accountnumber, string pwd, string role, string Birth1, string Birth2, string Sex, string nativeplace, string Entrytime, string Nowhous, string photoUrl, string IDcard)
        {
            Result result = new Result();
            EAuthentication eau = sa.Authentication(Username, Token);
            if (UserID == null || UserID == "" || name == null || name == "" || role == "" || role == null || jobnumber == null || jobnumber == "" || accountnumber == "" || accountnumber == null || pwd == "" || pwd == null)
            {
                result.state = 2;
                result.msg = "您可能还有些重要字段没有填写！";
            }
            else if (accountnumber.Length < 4 || pwd.Length < 4)
            {
                result.state = 2;
                result.msg = "账号或密码必须都大于4位数！";
            }
            else if (!eau.state)
            {
                result.state = -1;
                result.msg = "未登录！";
            }
            else if (eau.Role != "1" && eau.Role != "2")
            {
                result.state = -2;
                result.msg = "您的权限不够！！！";
            }
            else
            {
                if (eau.Role == "2" && role == "2")
                {
                    result.state = -2;
                    result.msg = "不能创建总经理";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                accountnumber = accountnumber.Replace(" ", "");
                CRM_User cu = new CRM_User();
                cu.ID = Guid.NewGuid().ToString("N");
                cu.T_UserID = UserID;   //----父级账号ID
                cu.T_name = name;
                cu.T_demartment = demartment;
                cu.T_duct = duct;
                cu.T_phone = phone;
                cu.T_jobnumber = jobnumber;
                cu.T_accountnumber = accountnumber;  //----账号
                cu.T_pwd = encrypt.Sha256(pwd);      //----密码
                cu.T_Birth = Birth1 + "." + Birth2;
                cu.T_Sex = Sex;
                cu.T_nativeplace = nativeplace;
                cu.T_Entrytime = Entrytime;
                cu.T_Nowhous = Nowhous;
                cu.T_photoUrl = photoUrl;
                cu.T_IDcard = IDcard;
                cu.T_role = int.Parse(role);    //----角色
                cu.T_UserRole = 2;
                cu.T_status = 1;
                cu.T_Usekind = 0;
                cu.T_remark = "";
                cu.T_remark = "";
                result = ads.A_Addson(cu);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //管理员显示自己的子账号
        public ActionResult SearchUserByUserid(string Username, string Token, string ID, string rows, string page, string name)
        {
            //Username = "pppp";
            //Token = "91062ca0bc51457895a7626d945bb0e1";
            //ID = "cce19a9951fd4b188deb8319b6835f68";
            //rows = "5";
            //page = "3";
            //name = "嘉";
            Result result = new Result();
            EAuthentication eau = sa.Authentication(Username, Token);
            if (ID == "" || ID == null || rows == null || rows == "" || page == "" || page == null)
            {
                result.state = 2;
                result.msg = "传值失败！";
            }
            else if (!eau.state)
            {
                result.state = -1;
                result.msg = "未登录！";
            }
            else if (eau.Role != "1" && eau.Role != "2")
            {
                result.state = -2;
                result.msg = "您的权限不够！！！";
            }
            else
            {
                string strWhere = string.Empty;
                if (eau.Role == "1")
                {
                    strWhere = "T_UserID='" + ID + "' and T_accountnumber<>'" + Username + "'" + " and T_name like '%" + name + "%'";
                }
                else
                {
                    string fid = CommService.SearchFIDByID(ID);
                    if (fid == null)
                    {
                        result.state = 3;
                        result.msg = "帐号异常！";
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    strWhere = "T_UserID='" + fid + "' and ID<>'" + fid + "'" + " and T_name like '%" + name + "%'";
                }
                result = ads.SearchUserByUserid(int.Parse(page), int.Parse(rows), strWhere);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //管理员显示子账号详情
        public ActionResult SearchUserByUseridinfor(string Username, string Token, string T_accountnumber)
        {
            //Username = "pppp";
            //Token = "8f44db3bb5294efa83e52522628d6104";
            //T_accountnumber = "pppxxx";
            Result result = new Result();
            EAuthentication eau = sa.Authentication(Username, Token);
            if (!eau.state)
            {
                result.state = -1;
                result.msg = "未登录！";
            }
            else if (eau.Role != "1" && eau.Role != "2")
            {
                result.state = -2;
                result.msg = "您的权限不够！！！";
            }
            else
            {
                result = ads.SearchUserByUseridinfor(T_accountnumber);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region //管理员修改自己的子账号
        public ActionResult UpdataSoninfor(string Username, string Token, string ID, string T_pwd, string T_name, string T_demartment, string T_duct, string T_phone, string T_jobnumber, string T_Birth1, string T_Birth2, string T_Sex, string T_nativeplace, string T_Entrytime, string T_Nowhous, string T_photoUrl, string T_IDcard, string T_role, string T_account)
        {
            //Username = "pppp";
            //Token = "4916d6f1c9b946dfaeefa93fabd06e31";
            //ID = "73fc8cb9e9bc45ba8681d37378d60454";
            //T_pwd = "";
            //T_role = "3";
            //T_name = "778899'";
            //T_account = "pppxxx";

            CRM_User cu = new CRM_User();
            Result result = new Result();
            if (T_name == null || T_name == "" || T_role == "" || T_role == null || T_account == "" || T_account == null)
            {
                result.state = 3;
                result.msg = "用户名称和角色必须填写！";
            }
            else if (ID.Contains("'") || ID.Contains("="))
            {
                result.state = 4;
                result.msg = "传值有安全隐患！";
            }
            else
            {
                EAuthentication eau = sa.Authentication(Username, Token);
                if (!eau.state)
                {
                    result.state = -1;
                    result.msg = "未登录！";
                }
                else if (eau.Role != "1" && eau.Role != "2")
                {
                    result.state = -2;
                    result.msg = "您的权限不够！！！";
                }
                else
                {
                    if (T_pwd == null || T_pwd == "" || T_pwd == "null" || T_pwd == "undefined")
                    {
                        string pwd = ads.searchPwdByusername(T_account);
                        cu.T_pwd = pwd;
                    }
                    else if (T_pwd.Replace(" ", "") == "")
                    {
                        result.state = 3;
                        result.msg = "密码不能为空格！";
                    }
                    else
                    {
                        cu.T_pwd = encrypt.Sha256(T_pwd);
                    }
                    cu.ID = ID;
                    cu.T_name = T_name;
                    cu.T_demartment = T_demartment;
                    cu.T_duct = T_duct;
                    cu.T_phone = T_phone;
                    cu.T_jobnumber = T_jobnumber;
                    cu.T_Birth = T_Birth1 + "." + T_Birth2;
                    cu.T_Sex = T_Sex;
                    cu.T_nativeplace = T_nativeplace;
                    cu.T_Entrytime = T_Entrytime;
                    cu.T_Nowhous = T_Nowhous;
                    cu.T_photoUrl = T_photoUrl;
                    cu.T_IDcard = T_IDcard;
                    cu.T_role = int.Parse(T_role);
                    cu.T_remark = "";
                    cu.T_remark1 = "";
                    result = ads.UpdataSoninfor(cu);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}
