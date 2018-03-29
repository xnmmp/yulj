using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using Service;
namespace CRM.Controllers
{
    public class CRM_visitController : Controller
    {
        //
        // GET: /CRM_ visit/
        SAuthoriza asu = new SAuthoriza();
        CRM_visitService cvs = new CRM_visitService();



        #region //添加客户拜访
        public ActionResult AddVisit(string Username, string Token, string UserID, string CV_documentaryID, string CV_consumerID, string CV_businessID, string CV_consumerName, string CV_ways, string CV_oppositepeople, string CV_purpose, string CV_talkdetails, string CV_feedback, string CV_lowerplan, string CV_fileurl, string CV_remark1)
        {
            //Username = "pppp";
            //Token = "9d0c31969a014a8284510e4af3eea6ff";
            //UserID = "cce19a9951fd4b188deb8319b6835f68";
            //CV_documentaryID = "1e3a68f4c130442ba6423d9c5674b3f3";
            //CV_businessName = "小商机";
            //CV_businessID = "fd2f4712627b4df1b61e4d8d805e0e99";
            //CV_consumerID = "5138e76420244df894aa1dccf80291ae";
            //CV_consumerName = "满舒克";
            //CV_feedback = "1";
            //CV_talkdetails = "项目很好";
            //CV_purpose = "1";
            //CV_ways = "1";

            //CV_fileurl = "10086";
            //UserID = "4d34d7068b014a61a9a41497329833c3";
            //Token = "fc8226243f54477ea3aa290a8a2dccba";
            //Username = "eval";
            //CV_remark1 = "备注";
            //CV_lowerplan = "计划";
            //CV_feedback = "1";
            //CV_talkdetails = "内容";
            //CV_purpose = "4";
            //CV_oppositepeople = "对方";
            //CV_ways = "1";
            //CV_businessID = "fd2f4712627b4df1b61e421305e0e99";
            //CV_consumerID = "5138e76420244df894aa1dccf80291ae";
            //CV_documentaryID = "4d34d7068b014a61a9a41497329833c3";
            //CV_consumerName = "邓振旭";







            Result result = new Result();
            if (CV_consumerName == null || CV_ways == null || CV_purpose == null || CV_talkdetails == null || CV_feedback == null || UserID == null || CV_documentaryID == null || CV_consumerID == null || CV_businessID == null)
            {
                result.state = 3;
                result.msg = "您可能还有些重要字段没有填写！";
            }
            else if (CV_consumerName == "" || CV_ways == "" || CV_purpose == "" || CV_talkdetails == "" || CV_feedback == "" || UserID == "" || CV_documentaryID == "" || CV_consumerID == "" || CV_businessID == "")
            {
                result.state = 3;
                result.msg = "您可能还有些重要字段没有填写！";
            }
            else
            {
                EAuthentication eau = asu.Authentication(Username, Token);
                if (!eau.state)
                {
                    result.state = -1;
                    result.msg = "未登录！";
                }
                else
                {
                    CRM_visit cv = new CRM_visit();
                    cv.ID = Guid.NewGuid().ToString("N");
                    cv.CV_datetime = DateTime.Now;
                    cv.CV_ways = int.Parse(CV_ways); //-------方式(int)
                    cv.CV_oppositepeople = CV_oppositepeople;   //--对方人员
                    cv.CV_purpose = int.Parse(CV_purpose);  //---目的(int)
                    cv.CV_talkdetails = CV_talkdetails;
                    cv.CV_feedback = int.Parse(CV_feedback);  //----反馈态度（int）
                    cv.CV_lowerplan = CV_lowerplan; //--下部计划
                    cv.CV_fileurl = CV_fileurl;  //---上传的文件
                    cv.CV_businessID = CV_businessID; //----商机ID
                    cv.CV_consumerID = CV_consumerID; //--客户ID
                    cv.CV_documentaryID = CV_documentaryID; //----跟单人ID(商机详情有)
                    cv.CV_businessName = ""; //---商机名称
                    cv.CV_consumerName = CV_consumerName;//---客户名称
                    cv.CV_UserID = UserID;   //--添加人的父级ID（商机父级ID 一样的）
                    cv.CV_remark1 = CV_remark1;  //---备注
                    result = cvs.AddVisit(cv);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        



        #region//查看客户拜访（右导航栏）
        public ActionResult CustomerVisit(string Username, string Token, string rows, string page, string CV_documentaryID, string UserID, string documentaryID, string BussName, string customerName, string Dates1, string Dates2)
        {
            //Username = "pppxxx";
            //Token = "787380eed3da49dd8540cd4bf7a56870";
            //rows = "5";
            //page = "1";
            //CV_documentaryID = "1e3a68f4c130442ba6423d9c5674b3f3";
            //UserID = "cce19a9951fd4b188deb8319b6835f68";
            Result result = new Result();
            if (rows == null || rows == "" || page == null || page == "" || CV_documentaryID == null || CV_documentaryID == "" || UserID == null || UserID == "")
            {
                result.state = 3;
                result.msg = "传值失败！";
            }
            else
            {
                EAuthentication eau = asu.Authentication(Username, Token);
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
                        string strss = "CV_UserID=" + "'" + UserID + "'";
                        strWhere = SearchVist(documentaryID, BussName, customerName, Dates1, Dates2, strss);
                    }
                    else
                    {
                        string strss = "CV_documentaryID=" + "'" + CV_documentaryID + "'";
                        strWhere = SearchVist(documentaryID, BussName, customerName, Dates1, Dates2, strss);
                    }
                    result = cvs.CustomerVisit(int.Parse(rows), int.Parse(page), strWhere);
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        




        #region//通过客户拜访ID查看客户拜访详情
        public ActionResult SearchVisitByID(string Username, string Token, string ID)
        {
            //Username = "pppxxx";
            //Token = "ae9698bae70a415b83a01d743b477230";
            //ID = "6f52472b008b40e59a43c0a91e3996be";
            Result result = new Result();
            if (ID == null || ID == "")
            {
                result.state = 3;
                result.msg = "传值失败！";
            }
            else
            {
                EAuthentication eau = asu.Authentication(Username, Token);
                if (!eau.state)
                {
                    result.state = -1;
                    result.msg = "未登录！";
                }
                else
                {
                    result = cvs.SearchVisitByID(ID);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        




        #region//进入商机详情看的客户拜访
        public ActionResult VisitByBusinessID(string Username, string Token, string CV_businessID)
        {
            //Username = "pppxxx";
            //Token = "787380eed3da49dd8540cd4bf7a56870";
            //CV_businessID = "fd2f4712627b4df1b61e4d8d805e0e99";
            Result result = new Result();
            if (CV_businessID == null || CV_businessID == "")
            {
                result.state = 3;
                result.msg = "传值为空！";
            }
            else
            {
                EAuthentication eau = asu.Authentication(Username, Token);
                if (!eau.state)
                {
                    result.state = -1;
                    result.msg = "未登录！";
                }
                else
                {
                    int rows = 10;
                    int page = 1;
                    string strWhere = "CV_businessID=" + "'" + CV_businessID + "'";
                    result = cvs.CustomerVisit(rows, page, strWhere);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        



        #region//修改客户拜访
        public ActionResult UpdataVisit(string Username, string Token, string ID, string CV_ways, string CV_oppositepeople, string CV_purpose, string CV_talkdetails, string CV_feedback, string CV_lowerplan, string CV_fileurl, string CV_consumerName, string CV_remark1)
        {
            //Username = "pppp";
            //Token = "0c14890dd0ab40f28b3032d22581ee5a";
            //ID = "320df8b7d6384a6abb28af4c697f6df6";
            //CV_ways = "2";
            //CV_purpose = "3";
            //CV_feedback = "1";
            //CV_consumerName = "大表哥";
            //CV_talkdetails = "3123";
            Result result = new Result();
            if (ID == null || CV_ways == null || CV_purpose == null || CV_feedback == null || CV_consumerName == null || CV_talkdetails == null)
            {
                result.state = 3;
                result.msg = "传值失败！";
            }
            else if (ID == "" || CV_ways == "" || CV_purpose == "" || CV_feedback == "" || CV_consumerName == "" || CV_talkdetails == "")
            {
                result.state = 3;
                result.msg = "传值失败！";
            }

            else
            {
                EAuthentication eau = asu.Authentication(Username, Token);
                if (!eau.state)
                {
                    result.state = -1;
                    result.msg = "未登录！";
                }
                else
                {
                    CRM_visit cv = new CRM_visit();
                    cv.ID = ID;
                    cv.CV_ways = int.Parse(CV_ways);
                    cv.CV_oppositepeople = CV_oppositepeople;
                    cv.CV_purpose = int.Parse(CV_purpose);
                    cv.CV_talkdetails = CV_talkdetails;
                    cv.CV_feedback = int.Parse(CV_feedback);
                    cv.CV_lowerplan = CV_lowerplan;
                    cv.CV_fileurl = CV_fileurl;
                    cv.CV_consumerName = CV_consumerName;
                    cv.CV_remark1 = CV_remark1;
                    result = cvs.UpdataVisit(cv);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        





        #region///////////////////////////////注意下方查询--------------------------------------------------/////////////////////////////////////
        public string SearchVist(string documentaryID, string BussName, string customerName, string Dates1, string Dates2, string strss)
        {
            if (documentaryID != null && documentaryID != "")
            {
                strss += "and CV_documentaryID='" + documentaryID + "'";
            }
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
                strss += " and  CV_datetime between convert(varchar(10),'" + Dates1 + "',111) and convert(varchar(10),'" + Dates2 + "',111)";
            }
            else
            {
                if (Dates1 != null && Dates1 != "")
                {
                    strss += "and CV_datetime > convert(varchar(10),'" + Dates1 + "',111)";
                }
                if (Dates2 != null && Dates2 != "")
                {
                    strss += "and  CV_datetime < convert(varchar(10),'" + Dates2 + "',111)";
                }
            }
            return strss;
        }
        #endregion

       



    }
}
