using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Entity;
namespace CRM.Controllers
{
    public class DocumentaryController : Controller
    {
        //
        // GET: /Documentary/
        DocumentaryService ds = new DocumentaryService();
        SAuthoriza sau = new SAuthoriza();


        #region //用户进入自己信息详情页面查看
        public ActionResult Searchdocumentaryinfor(string Username, string Token, string T_accountnumber)
        {
            T_accountnumber = Username;
            Result result = new Result();
            EAuthentication eau = sau.Authentication(Username, Token);
            if (!eau.state)
            {
                result.state = -1;
                result.msg = "未登录！";
            }
            else
            {
                result = ds.searchCRMUserInfor(T_accountnumber);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion





        #region//用户修改自己的信息
        public ActionResult UpdatauserInfor(string Username, string Token, string T_name, string T_demartment, string T_duct, string T_phone, string T_jobnumber, string T_Birth1, string T_Birth2, string T_Sex, string T_nativeplace, string T_Entrytime, string T_Nowhous, string T_photoUrl, string T_IDcard)
        {
            //Username = "pppxxx";
            //Token = "bd2f8f75b1ab4a609bbfd8d0aac44a64";
            //T_name = "我是pppxxx";
            Result result = new Result();
            EAuthentication eau = sau.Authentication(Username, Token);
            if (!eau.state)
            {
                result.state = -1;
                result.msg = "未登录！";
            }
            else if (T_name == null || T_name == "")
            {
                result.state = 2;
                result.msg = "姓名不能为空！";
            }
            else
            {
                CRM_User cu = new CRM_User();
                cu.T_accountnumber = Username;
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
                cu.T_remark = "";
                cu.T_remark1 = "";
                result = ds.UpdatauserInfor(cu);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion






        #region//显示所有审核状态为1的用户
        public ActionResult SearchUserBystatusStatus(string Username, string Token, string UserID)
        {
            //Username = "pppxxx";
            //Token = "bd2f8f75b1ab4a609bbfd8d0aac44a64";
            //UserID = "cce19a9951fd4b188deb8319b6835f68";
            Result result = new Result();
            if (UserID == null || UserID == "")
            {
                result.state = 2;
                result.msg = "传值错误！";
            }
            else
            {
                EAuthentication eau = sau.Authentication(Username, Token);
                if (!eau.state)
                {
                    result.state = -1;
                    result.msg = "未登录！";
                }
                else
                {
                    result = ds.SearchUserBystatusStatus(UserID);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion






        #region//在用户修改个人密码中，通过账号查找出密码
        public ActionResult SearchIsPwdPRO(string Username, string Token, string pwd)
        {
            //Username = "eval001";
            //Token = "814c22cd4cc149369338757fed0d3e18";
            //pwd = "1234";

            Result result = new Result();
            EAuthentication eau = sau.Authentication(Username, Token);
            if (pwd == null || pwd == "")
            {
                result.state = 3;
                result.msg = "密码未输入！";
            }
            else if (!eau.state)
            {
                result.state = -1;
                result.msg = "未登录！";
            }
            else
            {
                result = ds.SearchIsPwdPRO(Username, pwd);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion




        #region//修改个人密码
        public ActionResult UpdataPWD(string Username, string Token, string pwd, string oldPwd)
        {
            //Username = "pppp";
            //Token = "3822c9309b134d6c8dda1fbe21066222";
            //pwd = "1234";
            //oldPwd = "123";
            Result result = new Result();
            EAuthentication eau = sau.Authentication(Username, Token);
            TokenTableService ts = new TokenTableService();
            if (pwd == null || pwd == "" || oldPwd == "" || oldPwd == null)
            {
                result.state = 3;
                result.msg = "传值失败！";
            }
            else if (pwd.Replace(" ", "") == "")
            {
                result.state = 3;
                result.msg = "密码不能为空！";
            }
            else if (!eau.state)
            {
                result.state = -1;
                result.msg = "未登录！";
            }
            else
            {

                result = ds.UpdataPWD(Username, pwd, eau.Role, oldPwd);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion




    }
}
