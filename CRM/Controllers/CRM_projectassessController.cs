using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using Service;
namespace CRM.Controllers
{
    public class CRM_projectassessController : Controller
    {
        //
        // GET: /CRM_projectassess/

        SAuthoriza sau = new SAuthoriza();
        CRM_projectassessService cps = new CRM_projectassessService();
        public ActionResult Index()
        {
            return View();
        }

        #region //添加项目需求
        public ActionResult Addprojectassess(string Username, string Token, string A_durationneed, string A_constructneed, string A_acceptanceneed, string A_defaultmakeup, string A_aptitudes, string A_fundsbackfront, string A_projectscale, string A_budget, string A_businessmass, string A_compete, string A_consumerkey, string A_kickbackneed, string A_consumerFocus, string A_competitiveness, string A_projectdrag, string A_overorder, string A_conclusion, string A_businessID, string A_customerID, string A_documentaryID, string UserID, string A_remark1, string A_remark2)
        {
            //Username = "pppxxx";
            //Token = "ae9698bae70a415b83a01d743b477230";
            //A_durationneed = "1";
            //A_constructneed = "2";
            //A_acceptanceneed = "3";
            //A_defaultmakeup = "4";
            //A_aptitudes = "5";
            //A_fundsbackfront = "1";
            //A_projectscale = "2";
            //A_budget = "3";
            //A_businessmass = "4";
            //A_compete = "5";
            //A_consumerkey = "1";
            //A_kickbackneed = "2";
            //A_consumerFocus = "3";
            //A_competitiveness = "4";
            //A_projectdrag = "5";
            //A_overorder = "1";
            //A_conclusion = "2";
            //A_businessID = "fd2f4712627b4df1b61e4d8d805e0e99";
            //A_customerID = "5138e76420244df894aa1dccf80291ae";
            //A_documentaryID = "1e3a68f4c130442ba6423d9c5674b3f3";
            //UserID = "cce19a9951fd4b188deb8319b6835f68";
            //A_remark1 = "很厉害！";
            //A_remark2 = "这条数据很牛逼！"; 

            Result result = new Result();
            if (UserID == null || A_durationneed == null || A_constructneed == null || A_acceptanceneed == null || A_defaultmakeup == null || A_aptitudes == null || A_fundsbackfront == null || A_projectscale == null || A_budget == null || A_businessmass == null || A_compete == null || A_consumerkey == null || A_kickbackneed == null || A_consumerFocus == null || A_competitiveness == null || A_projectdrag == null || A_overorder == null || A_conclusion == null || A_businessID == null || A_customerID == null)
            {
                result.state = 3;
                result.msg = "您可能还有些必需填写的信息没有填写！";
            }
            else if (UserID == "" || A_durationneed == "" || A_constructneed == "" || A_acceptanceneed == "" || A_defaultmakeup == "" || A_aptitudes == "" || A_fundsbackfront == "" || A_projectscale == "" || A_budget == "" || A_businessmass == "" || A_compete == "" || A_consumerkey == "" || A_kickbackneed == "" || A_consumerFocus == "" || A_competitiveness == "" || A_projectdrag == "" || A_overorder == "" || A_conclusion == "" || A_businessID == "" || A_customerID == "")
            {
                result.state = 3;
                result.msg = "您可能还有些必需填写的信息没有填写！";
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
                    CRM_projectassess cp = new CRM_projectassess();
                    cp.ID = Guid.NewGuid().ToString("N");
                    cp.A_datetime = DateTime.Now;
                    cp.A_durationneed = int.Parse(A_durationneed);
                    cp.A_constructneed = int.Parse(A_constructneed);
                    cp.A_acceptanceneed = int.Parse(A_acceptanceneed);
                    cp.A_defaultmakeup = int.Parse(A_defaultmakeup);
                    cp.A_aptitudes = int.Parse(A_aptitudes);
                    cp.A_fundsbackfront = int.Parse(A_fundsbackfront);
                    cp.A_projectscale = int.Parse(A_projectscale);
                    cp.A_budget = int.Parse(A_budget);
                    cp.A_businessmass = int.Parse(A_businessmass);
                    cp.A_compete = int.Parse(A_compete);
                    cp.A_consumerkey = int.Parse(A_consumerkey);
                    cp.A_kickbackneed = int.Parse(A_kickbackneed);
                    cp.A_consumerFocus = int.Parse(A_consumerFocus);
                    cp.A_competitiveness = int.Parse(A_competitiveness);
                    cp.A_projectdrag = int.Parse(A_projectdrag);
                    cp.A_overorder = int.Parse(A_overorder);
                    cp.A_conclusion = int.Parse(A_conclusion);
                    cp.A_businessName = "";
                    cp.A_businessID = A_businessID;
                    cp.A_customerName = "";
                    cp.A_customerID = A_customerID;
                    cp.A_documentaryID = A_documentaryID;
                    cp.UserID = UserID;
                    cp.A_remark1 = A_remark1;
                    cp.A_remark2 = A_remark2;
                    result = cps.Addprojectassess(cp);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region //显示所有需求评估
        public ActionResult projectassess(string Username, string Token, string rows, string page, string A_documentaryID, string UserID, string BussName, string customerName, string Dates1, string Dates2, string bussmass1, string bussmass2, string Over1, string Over2)
        {
            //Username = "pppp";
            //Token = "8b16c6c2f46e419582728c33cc15db53";
            //page = "1";
            //rows = "5";
            //A_documentaryID = "1e3a68f4c130442ba6423d9c5674b3f3";
            //UserID = "cce19a9951fd4b188deb8319b6835f68";
            //customerName = "貂";

            //Over2 = "100";
            //bussmass2 = "100";
            Result result = new Result();
            if (rows == null || page == null || A_documentaryID == null || UserID == null)
            {
                result.state = 3;
                result.msg = "传值失败！";
            }
            else if (rows == "" || page == "" || A_documentaryID == "" || UserID == "")
            {
                result.state = 3;
                result.msg = "传值失败！";
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
                    string strWhere = "";
                    if (eau.Role == "1" || eau.Role == "2")
                    {
                        string strss = "UserID=" + "'" + UserID + "'";
                        strWhere = Serachproject(BussName, customerName, Dates1, Dates2, strss, bussmass1, bussmass2, Over1, Over2);
                    }
                    else
                    {
                        string strss = "A_documentaryID=" + "'" + A_documentaryID + "'";
                        strWhere = Serachproject(BussName, customerName, Dates1, Dates2, strss, bussmass1, bussmass2, Over1, Over2);
                    }
                    result = cps.projectassess(int.Parse(rows), int.Parse(page), strWhere);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //通过ID查看需求评估详情
        public ActionResult projectassessInfor(string Username, string Token, string ID)
        {
            //Username = "pppxxx";
            //Token = "ae9698bae70a415b83a01d743b477230";
            //ID = "c73d9edc9b8748e8afd14d8e3622f4ad";

            Result result = new Result();
            if (ID == null || ID == "")
            {
                result.state = 3;
                result.msg = "传值失败！";
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
                    result = cps.projectassessInfor(ID);
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //在商机内看到的需求评估
        public ActionResult projectassessInforBybuss(string Username, string Token, string A_businessID)
        {
            //Username = "pppxxx";
            //Token = "bd2f8f75b1ab4a609bbfd8d0aac44a64";
            //A_businessID = "fd2f4712627b4df1b61e4d8d805e0e99";

            Result result = new Result();
            if (A_businessID == null || A_businessID == "")
            {
                result.state = 3;
                result.msg = "传值失败！";
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
                    result = cps.projectassessInforBybuss(A_businessID);
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion




        #region //修改需求评估
        public ActionResult Updataprojectassess(string Username, string Token, string ID, string A_durationneed, string A_constructneed, string A_acceptanceneed, string A_defaultmakeup, string A_aptitudes, string A_fundsbackfront, string A_projectscale, string A_budget, string A_businessmass, string A_compete, string A_consumerkey, string A_kickbackneed, string A_consumerFocus, string A_competitiveness, string A_projectdrag, string A_overorder, string A_conclusion, string A_remark1, string A_remark2)
        {
            //Username = "pppxxx";
            //Token = "ae9698bae70a415b83a01d743b477230";
            //ID = "768561f9afd34eb38586d51135b13209";
            //A_durationneed = "2";
            //A_constructneed = "1";
            //A_acceptanceneed = "1";
            //A_defaultmakeup = "2";
            //A_aptitudes = "1";
            //A_fundsbackfront = "1";
            //A_projectscale = "2";
            //A_budget = "1";
            //A_businessmass = "1";
            //A_compete = "1";
            //A_consumerkey = "1";
            //A_kickbackneed = "1";
            //A_consumerFocus = "1";
            //A_competitiveness = "1";
            //A_projectdrag = "1";
            //A_overorder = "1";
            //A_conclusion = "1";
            //A_remark1 = "1";
            //A_remark2 = "1";

            //Username = "pppp";
            //Token = "038741887cc14c26beaaa44419904e24";
            //ID = "768561f9afd34eb38586d51135b13209";
            //A_durationneed = "5";
            //A_constructneed = "5";
            //A_acceptanceneed = "5";
            //A_defaultmakeup = "1";
            //A_aptitudes = "0";
            //A_fundsbackfront = "20";
            //A_projectscale = "20";
            //A_businessmass = "61";
            //A_budget = "5";
            //A_compete = "20";
            //A_consumerkey = "0";
            //A_kickbackneed = "0";
            //A_consumerFocus = "5";
            //A_competitiveness = "5";
            //A_projectdrag = "10";
            //A_overorder = "40";
            //A_conclusion = "0";


            Result result = new Result();
            if (ID.Contains("where=") || ID.Contains("'"))
            {
                result.state = 3;
                result.msg = "您可能还有些重要的字段没有填写！";
            }
            else if (ID == null || A_durationneed == null || A_constructneed == null || A_acceptanceneed == null || A_defaultmakeup == null || A_aptitudes == null || A_fundsbackfront == null || A_projectscale == null || A_budget == null || A_businessmass == null || A_compete == null || A_consumerkey == null || A_kickbackneed == null || A_consumerFocus == null || A_competitiveness == null || A_projectdrag == null || A_overorder == null || A_conclusion == null)
            {
                result.state = 3;
                result.msg = "您可能还有些重要的字段没有填写！";
            }
            else if (ID == "" || A_durationneed == "" || A_constructneed == "" || A_acceptanceneed == "" || A_defaultmakeup == "" || A_aptitudes == "" || A_fundsbackfront == "" || A_projectscale == "" || A_budget == "" || A_businessmass == "" || A_compete == "" || A_consumerkey == "" || A_kickbackneed == "" || A_consumerFocus == "" || A_competitiveness == "" || A_projectdrag == "" || A_overorder == "" || A_conclusion == "")
            {
                result.state = 3;
                result.msg = "您可能还有些重要的字段没有填写！";
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
                    CRM_projectassess cp = new CRM_projectassess();
                    cp.ID = ID;
                    cp.A_durationneed = int.Parse(A_durationneed);
                    cp.A_constructneed = int.Parse(A_constructneed);
                    cp.A_acceptanceneed = int.Parse(A_acceptanceneed);
                    cp.A_defaultmakeup = int.Parse(A_defaultmakeup);
                    cp.A_aptitudes = int.Parse(A_aptitudes);
                    cp.A_fundsbackfront = int.Parse(A_fundsbackfront);
                    cp.A_projectscale = int.Parse(A_projectscale);
                    cp.A_budget = int.Parse(A_budget);
                    cp.A_businessmass = int.Parse(A_businessmass);
                    cp.A_compete = int.Parse(A_compete);
                    cp.A_consumerkey = int.Parse(A_consumerkey);
                    cp.A_kickbackneed = int.Parse(A_kickbackneed);
                    cp.A_consumerFocus = int.Parse(A_consumerFocus);
                    cp.A_competitiveness = int.Parse(A_competitiveness);
                    cp.A_projectdrag = int.Parse(A_projectdrag);
                    cp.A_overorder = int.Parse(A_overorder);
                    cp.A_conclusion = int.Parse(A_conclusion);
                    cp.A_remark1 = "";
                    cp.A_remark2 = "";
                    result = cps.Updataprojectassess(cp);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion




        #region //////////注意注意////查询------------///////////////////////////////////////////////////////////////////////////////////////////////////////
        public string Serachproject(string BussName, string customerName, string Dates1, string Dates2, string strss, string bussmass1, string bussmass2, string Over1, string Over2)
        {
            if (BussName != null && BussName != "")
            {
                strss += "and B_name like '%" + BussName + "%'";
            }
            if (customerName != null && customerName != "")
            {
                strss += "and C_name like '%" + customerName + "%'";
            }
            if (Dates1 != null && Dates1 != "" && Dates2 != null && Dates2 != "")
            {
                strss += " and  A_datetime between convert(varchar(10),'" + Dates1 + "',111) and convert(varchar(10),'" + Dates2 + "',111)";
            }
            else
            {
                if (Dates1 != null && Dates1 != "")
                {
                    strss += "and A_datetime > convert(varchar(10),'" + Dates1 + "',111)";
                }
                if (Dates2 != null && Dates2 != "")
                {
                    strss += "and  A_datetime < convert(varchar(10),'" + Dates2 + "',111)";
                }
            }
            if (bussmass1 != null && bussmass1 != "" && bussmass2 != null && bussmass2 != "")
            {
                strss += " and  A_businessmass between " + int.Parse(bussmass1) + "  and  " + int.Parse(bussmass2);
            }
            else
            {
                if (bussmass1 != null && bussmass1 != "")
                {
                    strss += " and  A_businessmass >=" + int.Parse(bussmass1);
                }
                if (bussmass2 != null && bussmass2 != "")
                {
                    strss += " and  A_businessmass <=" + int.Parse(bussmass2);
                }
            }
            if (Over1 != null && Over1 != "" && Over2 != null && Over2 != "")
            {
                strss += " and  A_overorder between " + int.Parse(Over1) + "  and  " + int.Parse(Over2);
            }
            else
            {
                if (Over1 != null && Over1 != "")
                {
                    strss += " and  A_overorder >=" + int.Parse(Over1);
                }
                if (Over2 != null && Over2 != "")
                {
                    strss += " and  A_overorder <=" + int.Parse(Over2);
                }
            }
            return strss;
        }
        #endregion

       

    }
}
