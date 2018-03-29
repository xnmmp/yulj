using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using Service;
namespace CRM.Controllers
{
    public class CRM_QProgramController : Controller
    {
        //
        // GET: /CRM_QProgram/

        SAuthoriza sau = new SAuthoriza();
        CRM_QProgramService cqs = new CRM_QProgramService();
        public ActionResult Index()
        {
            return View();
        }

        #region //添加方案报价
        public ActionResult AddQprogram(string Username, string Token, string UserID, string O_consumerpeople, string O_conferaddress, string O_tokleinfor, string O_fileURL, string O_nextplan, string O_customerfeedback, string O_businessID, string O_consumerID, string O_documentaryID, string remark1)
        {
            //Username = "eval";
            //Token = "bd61a3d9ace24358ba8c4025eac71b4b";
            //UserID = "9349db7e4ba840549eaf1064a640c408";
            //O_tokleinfor = "星河系美少女！";
            //O_customerfeedback = "1";
            //O_businessID = "7c95a280a5f4472dba5b74c11b636a67";
            //O_consumerID = "f7d7e1030ad545f9b260198fe7a09c24";
            //O_documentaryID = "12312312312";
            //O_fileURL = "002";
            Result result = new Result();
            if (UserID == null || O_tokleinfor == null || O_customerfeedback == null || O_businessID == null || O_consumerID == null || O_fileURL == null)
            {
                result.state = 3;
                result.msg = "您可能还有一些必须填写的信息没有填写！";
            }
            else if (UserID == "" || O_tokleinfor == "" || O_customerfeedback == "" || O_businessID == "" || O_consumerID == "" || O_fileURL == "")
            {
                result.state = 3;
                result.msg = "您可能还有一些必须填写的信息没有填写！";
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
                    CRM_QProgram cq = new CRM_QProgram();
                    cq.ID = Guid.NewGuid().ToString("N");
                    cq.O_UserID = UserID;
                    cq.O_datetime = DateTime.Now;
                    cq.O_consumerpeople = O_consumerpeople;
                    cq.O_conferaddress = O_conferaddress;
                    cq.O_tokleinfor = O_tokleinfor;
                    cq.O_fileURL = O_fileURL;
                    cq.O_nextplan = O_nextplan;
                    cq.O_customerfeedback = int.Parse(O_customerfeedback);
                    cq.O_businessName = "";
                    cq.O_businessID = O_businessID;
                    cq.O_consumerName = "";
                    cq.O_consumerID = O_consumerID;
                    cq.O_documentaryID = O_documentaryID;
                    cq.remark1 = remark1;
                    cq.remark2 = "";
                    result = cqs.AddQprogram(cq);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //查看方案报价（右导航）
        public ActionResult Qprogram(string Username, string Token, string rows, string page, string UserID, string O_documentaryID, string BussName, string customerName, string Dates1, string Dates2)
        {
            //Username = "pppp";
            //Token = "695cd9b1dd5f45a6a4845444c58c17a0";
            //rows = "10";
            //page = "1";
            //O_documentaryID = "1e3a68f4c1304421ba6423d9c5674b3f3";
            //UserID = "cce19a9951fd4b188deb8319b6835f68";
            //customerName = "邓";



            Result result = new Result();
            if (UserID == null || UserID == "" || O_documentaryID == null || O_documentaryID == "")
            {
                result.state = 3;
                result.msg = "传值失败！";
            }
            else if (rows == null || rows == "" || page == null || page == "")
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
                    if (eau.Role == "1" | eau.Role == "2")
                    {
                        string strss = "O_UserID=" + "'" + UserID + "'";
                        strWhere = SerachQprogram(BussName, customerName, Dates1, Dates2, strss);
                    }
                    else
                    {
                        string strss = "O_documentaryID=" + "'" + O_documentaryID + "'";
                        strWhere = SerachQprogram(BussName, customerName, Dates1, Dates2, strss);
                    }
                    result = cqs.Qprogram(int.Parse(rows), int.Parse(page), strWhere);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //通过方案报价ID查看方案报价详情
        public ActionResult LoadinforQprogram(string Username, string Token, string ID)
        {
            //Username = "eval";
            //Token = "18206140d7bb4b4db6787a9613342cf4";
            //ID = "6b2de29fe942464a9e7611623cff6580";


            Result result = new Result();
            if (ID == null || ID == "")
            {
                result.msg = "传值失败！";
                result.state = 3;
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
                    result = cqs.LoadinforQprogram(ID);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //修改
        public ActionResult UpdataQprogram(string Username, string Token, string ID, string O_consumerpeople, string O_conferaddress, string O_tokleinfor, string O_fileURL, string O_nextplan, string O_customerfeedback, string remark1, string remark2)
        {
            //Username = "pppxxx";
            //Token = "bd2f8f75b1ab4a609bbfd8d0aac44a64";
            //ID = "26956de0d76f4cf3bac2772ffb9714c2";
            //O_consumerpeople = "对方人员";
            //O_conferaddress = "商谈地址";
            //O_tokleinfor = "商谈内容";
            //O_fileURL = "报价文件路径";
            //O_nextplan = "下步计划";
            //O_customerfeedback = "1";
            //remark1 = "备注";
            Result result = new Result();
            if (ID.Contains("'") || ID.Contains("where=") || ID.Contains("="))
            {
                result.msg = "传值失败，您可能有些必填些的信息没有填写！";
                result.state = 3;
            }
            else if (ID == null || ID == "" || O_tokleinfor == "" || O_tokleinfor == null || O_customerfeedback == "" || O_customerfeedback == null)
            {
                result.msg = "传值失败，您可能有些必填些的信息没有填写！";
                result.state = 3;
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
                    CRM_QProgram cq = new CRM_QProgram();
                    cq.ID = ID;
                    cq.O_consumerpeople = O_consumerpeople;
                    cq.O_conferaddress = O_conferaddress;
                    cq.O_tokleinfor = O_tokleinfor;
                    cq.O_fileURL = O_fileURL;
                    cq.O_nextplan = O_nextplan;
                    cq.O_customerfeedback = int.Parse(O_customerfeedback);
                    cq.remark1 = remark1;
                    cq.remark2 = "";
                    result = cqs.UpdataQprogram(cq);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //商机中看到的项目方案报价
        public ActionResult QprogramByBuss(string Username, string Token, string O_businessID)
        {
            //Username = "user100";
            //Token = "6565161680c14405b57334bc350d3a5f";
            //O_businessID = "9d02da7cf9cb4da98c8735b53ff1301d";

            Result result = new Result();
            if (O_businessID == null || O_businessID == "")
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
                    result = cqs.QprogramByBuss(O_businessID);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region /////////////////////////////查询开始--------------------/////////////////////////////////////////////
        public string SerachQprogram(string BussName, string customerName, string Dates1, string Dates2, string strss)
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
                strss += " and  O_datetime between convert(varchar(10),'" + Dates1 + "',111) and convert(varchar(10),'" + Dates2 + "',111)";
            }
            else
            {
                if (Dates1 != null && Dates1 != "")
                {
                    strss += "and O_datetime > convert(varchar(10),'" + Dates1 + "',111)";
                }
                if (Dates2 != null && Dates2 != "")
                {
                    strss += "and  O_datetime < convert(varchar(10),'" + Dates2 + "',111)";
                }
            }
            return strss;
        }
        #endregion


        

    }
}
