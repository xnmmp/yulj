using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Repository;
using CommonSha;
using Entity;
namespace CRM.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /test/

        LoginService ls = new LoginService();
        Logininfor log = new Logininfor();
        SAuthoriza sau = new SAuthoriza();
        TokenTableService tts = new TokenTableService();
        AdministratorService ads = new AdministratorService();
        #region //首页
        public ActionResult Index()
        {
            return Redirect(Url.Content("CRM/CRM_log.html"));
        }
        #endregion


        //public ActionResult cheshi() {
        //    string T_phone = "18771119362";
        //    CodeAlert ca = new CodeAlert();
        //    int state = ca.SSM(T_phone);
        //    if (state==2)
        //    {
        //        return Content("ok");
                
        //    }
        //    else
        //    {
        //        return Content("No");
        //    }
        //}

        #region //验证登录
        public ActionResult loginfrom(string name, string pwd)
        {
            //name = "test2";
            //pwd = "123";
            Result result = new Result();
            result = ls.IsLogin(name, pwd);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        


        #region//注销 (删除Token表中对于的账号的token)
        public ActionResult Clean(string Username, string Token)
        {
            //Username = "pppp";
            //Token = "2b0cefb638914a6ba470cd570107c14e";
            Result result = new Result();
            EAuthentication eau = sau.Authentication(Username, Token);
            if (!eau.state)
            {
                result.state = -1;
                result.msg = "未登录！";
            }
            else
            {
                if (tts.UpdataToken(Username, "", "", eau.Role, ""))
                {
                    result.state = 1;

                }
                else
                {
                    result.state = 2;
                    result.msg = "注销失败！";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        


        #region//app传cookie与返回新guid
        public ActionResult Changetoken(string Username, string Token)
        {
            //Username = "pppp";
            //Token = "1ccfb55e0c3f4e3bbfaf11b3170013e9";

            Result result = new Result();
            result = log.R_Changetoken(Username, Token);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

       

    }
}
