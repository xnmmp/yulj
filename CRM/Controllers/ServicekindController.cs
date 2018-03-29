using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Entity;
namespace CRM.Controllers
{
    public class ServicekindController : Controller
    {
        //
        // GET: /Servicekind/
        SAuthoriza sau = new SAuthoriza();
        Service_kindService ss = new Service_kindService();


        #region //服务一二级联查
        public ActionResult ServiceOneSerchTwo(string Username, string Token, string index, string childId)
        {
            //Username = "Mearemy";
            //Token = "13d5eaef57844d2198cae988f923fae9";
            //index = "1";
            //childId = "4";
            Result result = new Result();
            if (index == null || index == "")
            {
                result.state = 3;
                result.msg = "没有选择服务类型！";
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
                    result = ss.ServiceOneSerchTwo(int.Parse(index), childId);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        



        #region//地址三级联查
        public ActionResult AdressSearch(string Username, string Token, string index, string childId)
        {
            //Username = "pppxxx";
            //Token = "bd2f8f75b1ab4a609bbfd8d0aac44a64";
            //index = "1";
            //childId = "110000";
            Result result = new Result();
            if (index == null || index == "")
            {
                result.state = 3;
                result.msg = "没有选择地址！";
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
                    result = ss.AdressSearch(int.Parse(index), childId);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        
    }
}
