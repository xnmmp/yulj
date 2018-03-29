using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Entity;
namespace CRM.Controllers
{
    public class CRM_contractController : Controller
    {
        //
        // GET: /CRM_contract/

        public ActionResult Index()
        {
            return View();
        }

        SAuthoriza sau = new SAuthoriza();
        CRM_contractService cs = new CRM_contractService();

        #region //添加合同
        public ActionResult Addcontract(string Username, string Token, string P_cookiename, string P_startdatetime, string P_enddatetime, string P_kindone, string P_kindtwo, string P_kindthree, string P_name, string P_money, string P_URL1, string P_URL2, string P_businessID, string P_customerID, string P_documentaryID, string UserID, string remark1, string remark2, string remark3)
        {
            //Username = "eval";
            //Token = "bbfebc90627b4575bf68224656cce9b4";
            //P_cookiename = "来嗨2";
            //P_kindone = "1";
            //P_kindtwo = "1";
            //P_kindthree = "0";
            //P_businessID = "64d28d95cfac433ea1f1c6cab9a38717";
            //P_customerID = "93044c0c7cb94dd9ac11591bca823f61";
            //UserID = "4d34d7068b014a61a9a41497329833c3";
            //P_name = "贼嗨2！";
            //P_URL1 = "001,002";

            Result result = new Result();
            if (P_cookiename == null || P_kindone == null || P_kindtwo == null || P_businessID == null || P_customerID == null || UserID == null || P_name == null || P_documentaryID == null || P_documentaryID == "")
            {
                result.state = 3;
                result.msg = "有些重要的字段您可能还没有填写！";
            }
            else if (P_cookiename == "" || P_kindone == "" || P_kindtwo == "" || P_businessID == "" || P_customerID == "" || UserID == "" || P_name == "")
            {
                result.state = 3;
                result.msg = "有些重要的字段您可能还没有填写！";
            }
            else if ((P_URL1 == null || P_URL1 == "") && (P_URL2 == null || P_URL2 == ""))
            {
                result.state = 3;
                result.msg = "请上传文件！";
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
                    Random rd = new Random();
                    int pp = rd.Next(10000);//
                    string txt = "0000" + pp.ToString();
                    int start = txt.Length - 4;
                    string ss = txt.Substring(start, 4);
                    CRM_contract cco = new CRM_contract();
                    string nowtime = DateTime.Now.ToString("yyyyMMdd");
                    cco.ID = nowtime + ss;
                    cco.P_datetime = DateTime.Now;
                    cco.P_cookiename = P_cookiename;
                    cco.P_startdatetime = P_startdatetime;
                    cco.P_enddatetime = P_enddatetime;
                    cco.P_kindone = int.Parse(P_kindone);
                    cco.P_kindtwo = int.Parse(P_kindtwo);
                    cco.P_kindthree = 0;
                    cco.P_name = P_name;
                    cco.P_money = P_money;
                    cco.P_URL1 = P_URL1;
                    cco.P_URL2 = P_URL2;
                    cco.P_businessName = "";
                    cco.P_businessID = P_businessID;
                    cco.P_customerName = "";
                    cco.P_customerID = P_customerID;
                    cco.P_documentaryID = P_documentaryID;
                    cco.UserID = UserID;
                    cco.remark1 = remark1;
                    cco.remark2 = remark2;
                    cco.remark3 = remark3;
                    result = cs.Addcontract(cco);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region //显示合同
        public ActionResult Contract(string Username, string Token, string rows, string page, string P_documentaryID, string UserID, string BussName, string customerName, string Dates1, string Dates2, string P_name, string P_cookiename)
        {
            //Username = "pppp";
            //Token = "93198143fe1a428cb115e68cd7791fc1";
            //rows = "10";
            //page = "1";
            //P_documentaryID = "1e3a68f4c130442ba6423d9c5674b3f3";
            //UserID = "cce19a9951fd4b188deb8319b6835f68";
            //P_name = "大大";


            Result result = new Result();
            if (rows == null || page == null || P_documentaryID == null || UserID == null)
            {
                result.state = 3;
                result.msg = "传值失败！";
            }
            else if (rows == "" || page == "" || P_documentaryID == "" || UserID == "")
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
                        strWhere = Serachcountract(BussName, customerName, Dates1, Dates2, strss, P_name, P_cookiename);
                    }
                    else
                    {
                        string strss = "P_documentaryID=" + "'" + P_documentaryID + "'";
                        strWhere = Serachcountract(BussName, customerName, Dates1, Dates2, strss, P_name, P_cookiename);
                    }
                    result = cs.Contract(int.Parse(rows), int.Parse(page), strWhere);

                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region //通过ID显示详情
        public ActionResult ContractInforByID(string Username, string Token, string ID)
        {
            //Token = "29398a47cb8e4faab5e1259a165a5ed1";
            //Username = "pppp";
            //ID = "201612157927";


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
                    int IsBuss = 2;
                    string P_businessID = "";
                    result = cs.ContractInfor(ID, IsBuss, P_businessID);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //通过商机ID显示详情
        public ActionResult ContractInforByBuss(string Username, string Token, string P_businessID)
        {
            //Token = "ebaed24900d54d1896893e1d1b2d7386";
            //Username = "pppp";
            //P_businessID = "bafee08da1de4067a645fc9734197287";

            Result result = new Result();
            if (P_businessID == null || P_businessID == "")
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
                    int IsBuss = 1;
                    string ID = "";
                    result = cs.ContractInfor(ID, IsBuss, P_businessID);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //修改合同
        public ActionResult UpdataContract(string Username, string Token, string ID, string P_cookiename, string P_startdatetime, string P_enddatetime, string P_kindone, string P_kindtwo, string P_kindthree, string P_name, string P_money, string P_URL1, string P_URL2, string remark1, string remark2, string remark3)
        {
            //Username = "pppxxx";
            //Token = "ae9698bae70a415b83a01d743b477230";
            //ID = "201612062315";
            //P_cookiename = "大发明家" ;
            //P_kindone = "2";
            //P_kindtwo = "2" ;
            //P_kindthree = "2";
            Result result = new Result();
            if (ID.Contains("where=") || ID.Contains("'"))
            {
                result.state = 3;
                result.msg = "您可能还有些重要信息没有填写！";
            }
            else if (ID == null || P_cookiename == null || P_kindone == null || P_kindtwo == null || P_name == null)
            {
                result.state = 3;
                result.msg = "您可能还有些重要信息没有填写！";
            }
            else if (ID == "" || P_cookiename == "" || P_kindone == "" || P_kindtwo == "" || P_name == "")
            {
                result.state = 3;
                result.msg = "您可能还有些重要信息没有填写！";
            }
            else if ((P_URL1 == null || P_URL1 == "") && (P_URL2 == null || P_URL2 == ""))
            {
                result.state = 3;
                result.msg = "请上传文件！";
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
                    CRM_contract cco = new CRM_contract();
                    cco.ID = ID;
                    cco.P_cookiename = P_cookiename;
                    cco.P_startdatetime = P_startdatetime;
                    cco.P_enddatetime = P_enddatetime;
                    cco.P_kindone = int.Parse(P_kindone);
                    cco.P_kindtwo = int.Parse(P_kindtwo);
                    cco.P_kindthree = 0;
                    cco.P_name = P_name;
                    cco.P_money = P_money;
                    cco.P_URL1 = P_URL1;
                    cco.P_URL2 = P_URL2;
                    cco.remark1 = remark1;
                    cco.remark2 = remark2;
                    cco.remark3 = remark3;
                    result = cs.UpdataContract(cco);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region /////////////////////////查询开始///////////////-------------//////////////////////////////////////////////////////
        public string Serachcountract(string BussName, string customerName, string Dates1, string Dates2, string strss, string P_name, string P_cookiename)
        {
            if (BussName != null && BussName != "")
            {
                strss += "and B_name like '%" + BussName + "%'";
            }
            if (customerName != null && customerName != "")
            {
                strss += "and C_name like '%" + customerName + "%'";
            }
            if (P_name != null && P_name != "")
            {
                strss += "and P_name like '%" + P_name + "%'";
            }
            if (P_cookiename != null && P_cookiename != "")
            {
                strss += "and P_cookiename like '%" + P_cookiename + "%'";
            }
            if (Dates1 != null && Dates1 != "" && Dates2 != null && Dates2 != "")
            {
                strss += " and  P_datetime between convert(varchar(10),'" + Dates1 + "',111) and convert(varchar(10),'" + Dates2 + "',111)";
            }
            else
            {
                if (Dates1 != null && Dates1 != "")
                {
                    strss += "and P_datetime > convert(varchar(10),'" + Dates1 + "',111)";
                }
                if (Dates2 != null && Dates2 != "")
                {
                    strss += "and  P_datetime < convert(varchar(10),'" + Dates2 + "',111)";
                }
            }
            return strss;
        }
        #endregion



    }
}
