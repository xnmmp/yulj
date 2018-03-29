using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using Service;
using CRM.Models;
using DAL;
namespace CRM.Controllers
{
    public class BusinessController : Controller
    {
        //
        // GET: /CRM_business/

        public ActionResult Index()
        {
            return View();
        }
        SAuthoriza sau = new SAuthoriza();
        CRM_businessService cbs = new CRM_businessService();
        CustomerService cs = new CustomerService();
        DocumentaryService ds = new DocumentaryService();
        /////////////////////////////////////////////////////////////////////////////////
        #region //商机显示循环抽出
        public List<CRM_BusinessVM> PubLoad(List<CRM_business> list)
        {

            List<CRM_BusinessVM> listVM = new List<CRM_BusinessVM>();
            foreach (var item in list)
            {
                CRM_BusinessVM cbvm = new CRM_BusinessVM();
                cbvm.ID = item.ID;
                cbvm.C_name = item.C_name;
                cbvm.B_name = item.B_name;
                cbvm.CustomerID = item.CustomerID;
                cbvm.C_address = item.C_address;
                cbvm.C_linkname = item.C_linkname;
                cbvm.C_linkphone = item.C_linkphone;
                cbvm.B_documentaryID = item.B_documentaryID;
                cbvm.B_datetime = item.B_datetime.ToString("yyyy-MM-dd");
                cbvm.B_needcause = item.B_needcause;
                int kind = item.B_kind;
                if (kind == 1)
                {
                    cbvm.B_kind = "项目施工";
                }
                else if (kind == 2)
                {
                    cbvm.B_kind = "服务保养";
                }
                else
                {
                    cbvm.B_kind = "无";
                }
                listVM.Add(cbvm);
            }
            return listVM;
        }
        #endregion


        #region //商机筛选循环抽出
        public List<CRM_BusinessVM> PubSaiXuan(List<CRM_business> list)
        {
            List<CRM_BusinessVM> listvm = new List<CRM_BusinessVM>();
            foreach (var item in list)
            {
                CRM_BusinessVM cbvm = new CRM_BusinessVM();
                cbvm.ID = item.ID;
                cbvm.B_datetime = item.B_datetime.ToString("yyyy-MM-dd");
                cbvm.B_status = item.B_status.ToString();
                cbvm.B_giveupcause = item.B_giveupcause;
                cbvm.B_name = item.B_name;
                cbvm.C_name = item.C_name;
                listvm.Add(cbvm);
            }
            return listvm;
        }
        #endregion


        #region //商机派单循环抽出
        public List<CRM_BusinessVM> PubPaiDan(List<CRM_business> list)
        {

            List<CRM_BusinessVM> listvm = new List<CRM_BusinessVM>();
            foreach (var item in list)
            {
                CRM_BusinessVM cbvm = new CRM_BusinessVM();
                cbvm.ID = item.ID;
                cbvm.C_name = item.C_name;
                cbvm.B_name = item.B_name;
                cbvm.B_documentaryID = item.B_documentaryID;
                cbvm.B_remark1 = item.B_remark1;
                cbvm.B_documentaryneed = item.B_documentaryneed;
                cbvm.B_documentaryName = item.C_address;
                listvm.Add(cbvm);
            }
            return listvm;
        }
        #endregion






        ///////////////////////////////////////////////////////////////////////////////////
        #region //添加商机
        public ActionResult Addbusiness(string Username, string Token, string B_name, string B_source, string B_kind, string B_servicekind1, string B_servicekind2, string B_needcause, string B_neednumber, string Addbusinessman, string CustomerID, string B_fistuseerID)
        {
            //Username = "pppp";
            //Token = "b06507714cc24b37b860a86cfd647a26";
            //B_name = "3232132";
            //B_source = "2";
            //B_kind = "1";
            //Addbusinessman = "这喵~＞▽＜不太萌！";
            //CustomerID = "035f220dc09c4237b3ed2e2eac5b4d59";
            //B_fistuseerID = "cce19a9951fd4b188deb8319b6835f68";
            //B_servicekind1 = "4";
            //B_servicekind2 = "1e7abc4618bc4ae8932b6055b8c2c065";
            //B_needcause = "31313";
            //B_neednumber = "3313";
            CustomerService cus = new CustomerService();
            Result result = new Result();
            if (B_name == null || B_source == null || B_kind == null || B_servicekind1 == null || B_servicekind2 == null || Addbusinessman == null || CustomerID == null || B_fistuseerID == null)
            {
                result.state = 3;
                result.msg = "您可能还有些必填字段没有填写！";
            }
            else if (B_name == "" || B_source == "" || B_kind == "" || B_servicekind1 == "" || B_servicekind2 == "" || Addbusinessman == "" || CustomerID == "" || B_fistuseerID == "")
            {
                result.state = 3;
                result.msg = "您可能还有些必填字段没有填写！";
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
                    CRM_business cb = new CRM_business();
                    string ID = cus.SearchID_ByName(Username);
                    cb.ID = Guid.NewGuid().ToString("N");
                    cb.B_name = B_name;
                    cb.B_datetime = DateTime.Now;
                    cb.B_source = int.Parse(B_source);
                    cb.B_kind = int.Parse(B_kind);
                    cb.B_servicekind1 = B_servicekind1;
                    cb.B_servicekind2 = B_servicekind2;
                    cb.B_needcause = B_needcause;
                    cb.B_neednumber = B_neednumber;
                    cb.Addbusinessman = Addbusinessman;
                    cb.CustomerID = CustomerID;
                    cb.B_documentaryID = "";
                    cb.B_fistuseerID = B_fistuseerID;
                    cb.B_status = 0;
                    cb.B_giveupcause = "";
                    cb.B_documentaryneed = "";
                    cb.B_needsassessment = 0;
                    cb.B_projectQuote = 0;
                    cb.B_remark1 = "";
                    cb.B_remark2 = ID;
                    result = cbs.Addbusiness(cb);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //显示商机
        public ActionResult SearchBussinessByRole(string Username, string Token, string page, string rows, string UserID, string B_documentaryID, string customerName, string Kind, string BussName, string Dates1, string Dates2)
        {

            //Username = "baiqw";
            //Token = "6cc76f1a915f47a6b759c5569d586d6b";
            //page = "1";
            //rows = "10";
            //UserID = "bae7dfd0f95842c290a42fa3b99c3d8b";
            //B_documentaryID = "0ba138fb969d46dead9e9dc54e5453e6";

            ////customerName = "1";
            ////Kind = "2";
            ////BussName = "哈";
            ////Dates1 = "2017/03/15";
            ////Dates2 = "2017/03/28";

            Result result = new Result();
            if (rows == null || rows == "" || page == "" || page == null || UserID == "" || UserID == null || B_documentaryID == null || B_documentaryID == "")
            {
                result.state = 3;
                result.msg = "还有些重要字段您可能还没输入！";
            }
            else if (rows == "0" || page == "0")
            {
                result.state = 2;
                result.msg = "没有数据了！";
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
                        string strss = "B_fistuseerID=" + "'" + UserID + "'";
                        strWhere = searchSaixuan(BussName, "", Dates1, Dates2, strss, "", customerName, Kind);
                    }
                    else
                    {
                        string strss = "B_documentaryID=" + "'" + B_documentaryID + "' or B_remark2='" + B_documentaryID + "'";
                        strWhere = searchSaixuan(BussName, "", Dates1, Dates2, strss, "", customerName, Kind);
                    }
                    var list = cbs.SearchBussinessByRole(int.Parse(page), int.Parse(rows), strWhere);
                    var count = cbs.BussinessScreenCount(strWhere);
                    if (list == null)
                    {
                        result.state = 2;
                        result.msg = "没有数据了！";
                    }
                    else
                    {
                        result.state = 1;
                        result.data = PubLoad(list);
                        result.count = count;
                        result.page = int.Parse(page);
                    }
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //显示商机详情
        public ActionResult SearchBussinessInfor(string Username, string Token, string ID)
        {
            //Username = "pppp";
            //Token = "cfa8bac707464b77ae46e18c4540a3cf";
            //ID = "b753f17017a94a32b53579ae7a3140f6";

            Result result = new Result();
            if (ID == null || ID == "")
            {
                result.state = 3;
                result.msg = "传值失败";
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
                    result = cbs.SearchBussinessInfor(ID);

                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //修改商机
        public ActionResult UpdataBusiness(string Username, string Token, string ID, string B_name, string B_source, string B_kind, string B_servicekind1, string B_servicekind2, string B_needcause, string B_neednumber, string CustomerID, string B_remark1, string B_remark2)
        {
            //Username = "pppp";
            //Token = "c7674371163641be8e10e14338f28659";
            //ID = "fd2f4712627b4df1b61e4d8d805e0e99";
            //B_name = "小商机";
            //B_source = "1";
            //B_kind = "1";
            //B_servicekind1 = "123";
            //B_servicekind2 = "456";
            //CustomerID = "5138e76420244df894aa1dccf80291ae";
            Result result = new Result();
            if (ID.Contains("=") || ID.Contains("where"))
            {
                result.state = 3;
                result.msg = "输入的字段不符合规则！";
            }
            else if (ID == null || B_name == null || B_source == null || B_kind == null || B_servicekind1 == null || B_servicekind2 == null || CustomerID == null)
            {
                result.state = 4;
                result.msg = "您可能还有些信息填写不完整，请填写完整再来提交！";
            }
            else if (ID == "" || B_name == "" || B_source == "0" || B_kind == "0" || B_servicekind1 == "" || B_servicekind2 == "" || CustomerID == "")
            {
                result.state = 4;
                result.msg = "您可能还有些信息填写不完整，请填写完整再来提交！";
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
                    CRM_business cb = new CRM_business();
                    cb.ID = ID;
                    cb.B_name = B_name;
                    cb.B_source = int.Parse(B_source);
                    cb.B_kind = int.Parse(B_kind);
                    cb.B_servicekind1 = B_servicekind1;
                    cb.B_servicekind2 = B_servicekind2;
                    cb.B_needcause = B_needcause;
                    cb.B_neednumber = B_neednumber;
                    cb.CustomerID = CustomerID;
                    cb.B_remark1 = "";
                    cb.B_remark2 = "";
                    result = cbs.UpdataBusiness(cb);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //修改筛选状态
        public ActionResult UpdataBusinessStatus(string Username, string Token, string ID, string B_status, string B_giveupcause)
        {
            //Username = "pppp";
            //Token = "987913c6d2cc4b06bfefc86cb7ca88a7";
            //ID = "123123123123";
            //B_status = "3";
            //B_giveupcause = "12312312";
            Result result = new Result();
            if (ID.Contains("=") || ID.Contains("where") || ID.Contains("'") || ID.Contains("%") || ID == null || ID == "")
            {
                result.state = 5;
                result.msg = "信息传递错误！";
            }
            else if (B_status == "" || B_status == null || B_status == "0")
            {
                result.state = 6;
                result.msg = "没有选择状态！";
            }
            else if (B_status == "3")
            {
                if (B_giveupcause == null || B_giveupcause == "")
                {
                    result.state = 7;
                    result.msg = "当选放弃状态时应该填写放弃理由！";
                }
            }
            if (result.state != 5 && result.state != 6 && result.state != 7)
            {
                EAuthentication eau = sau.Authentication(Username, Token);
                if (!eau.state)
                {
                    result.state = -1;
                    result.msg = "未登录！";
                }
                else if (eau.Role != "1" && eau.Role != "2")
                {
                    result.state = -2;
                    result.msg = "没有权限！";
                }
                else
                {
                    result = cbs.UpdataBusinessStatus(ID, int.Parse(B_status), B_giveupcause);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //修改跟单人(派单)
        public ActionResult UpdataBusinessDocumentary(string Username, string Token, string ID, string Addbusinessman, string B_documentaryID, string B_documentaryneed)
        {
            //Username = "pppp";
            //Token = "1e900e9ae2ce407c9170b73af27995e8";
            //ID = "fd2f4712627b4df1b61e4d8d805e0e99";
            //B_documentaryID = "1e3a68f4c130442ba6423d9c5674b3f3";
            //Addbusinessman = "大哥";
            //B_documentaryneed = "小伙子好好干！";

            Result result = new Result();
            if (ID.Contains("=") || ID.Contains("where") || ID.Contains("'") || ID.Contains("%") || ID == null || ID == "")
            {
                result.state = 4;
                result.msg = "信息传递错误！";
            }
            else if (B_documentaryID == null || B_documentaryID == "" || Addbusinessman == null || Addbusinessman == "")
            {
                result.state = 4;
                result.msg = "信息传递错误！";
            }
            else
            {
                EAuthentication eau = sau.Authentication(Username, Token);
                if (!eau.state)
                {
                    result.state = -1;
                    result.msg = "未登录！";
                }
                else if (eau.Role != "1" && eau.Role != "2")
                {
                    result.state = -2;
                    result.msg = "没有权限！";
                }
                else
                {
                    result = cbs.UpdataBusinessDocumentary(ID, Addbusinessman, B_documentaryID, B_documentaryneed);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //商机筛选
        public ActionResult BussinessScreen(string Username, string Token, string UserID, string rows, string page, string BussName, string status, string Dates1, string Dates2)
        {
            //Username = "eval";
            //Token = "8e88e5206bb745c7a67cb799af39e855";
            //UserID = "4d34d7068b014a61a9a41497329833c3";
            //rows = "10";
            //page = "1";
            //Username = "pppp";
            //Token = "89007fa790514344b45905416b7d95de";
            //BussName = "陆家嘴";
            //UserID = "cce19a9951fd4b188deb8319b6835f68";
            //status = "1";
            //Dates1 = "2016/5/1";
            //Dates2 = "2016/12/30";

            Result result = new Result();
            if (rows == null || rows == "" || page == "" || page == null)
            {
                result.state = 3;
                result.msg = "传值失败，请重新操作！";
            }
            else if (UserID == "" || UserID == null)
            {
                result.state = 3;
                result.msg = "传值失败，请重新操作！";
            }
            else
            {
                EAuthentication eau = sau.Authentication(Username, Token);
                if (!eau.state)
                {
                    result.state = -1;
                    result.msg = "未登录！";
                }
                else if (eau.Role != "1" && eau.Role != "2")
                {
                    result.state = -2;
                    result.msg = "没有权限！";
                }
                else
                {
                    string strss = "B_fistuseerID=" + "'" + UserID + "'";
                    string strWhere = searchSaixuan(BussName, status, Dates1, Dates2, strss, "", "", "");
                    var list = cbs.BussinessScreen(int.Parse(rows), int.Parse(page), strWhere);
                    int count = cbs.BussinessScreenCount(strWhere);
                    if (list == null)
                    {
                        result.state = 2;
                        result.msg = "没有数据了！";
                    }
                    else
                    {
                        result.data = PubSaiXuan(list);
                        result.state = 1;
                        result.count = count;
                        result.page = int.Parse(page);
                    }
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region //商机派单（右导航）
        public ActionResult BussinessSendorders(string Username, string Token, string UserID, string rows, string page, string BussName, string Dates1, string Dates2, string documentaryname)
        {
            //Username = "pppp";
            //Token = "6e1f55c9f6bf441d901a4a2203a56690";
            //UserID = "cce19a9951fd4b188deb8319b6835f68";
            //rows = "10";
            //page = "1";
            //documentaryname = "飞";
            //BussName = "哈";




            Result result = new Result();
            if (rows == null || rows == "" || page == "" || page == null)
            {
                rows = "10";
                page = "1";
            }
            if (UserID == "" || UserID == null)
            {
                result.state = 3;
                result.msg = "传值失败，请重新操作！";
            }
            else
            {
                EAuthentication eau = sau.Authentication(Username, Token);
                if (!eau.state)
                {
                    result.state = -1;
                    result.msg = "未登录！";
                }
                else if (eau.Role != "1" && eau.Role != "2")
                {
                    result.state = -2;
                    result.msg = "没有权限！";
                }
                else
                {
                    int B_status = 1;
                    string strss = "B_fistuseerID=" + "'" + UserID + "'" + "and" + " " + "B_status" + "=" + B_status;
                    string strWhere = searchSaixuan(BussName, "", Dates1, Dates2, strss, documentaryname, "", "");  //-----查询条件
                    var list = cbs.BussinessSendorders(int.Parse(rows), int.Parse(page), strWhere);
                    int count = cbs.BussinessPaidanCount(strWhere);
                    if (list == null)
                    {
                        result.state = 2;
                        result.msg = "没有数据了！";
                    }
                    else
                    {

                        result.data = PubPaiDan(list);
                        result.state = 1;
                        result.count = count;
                        result.page = int.Parse(page);
                    }
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region /////////////////////////////下方预警----------------查询部分！！！////////////////////////////////////////
        public string searchSaixuan(string BussName, string status, string Dates1, string Dates2, string strss, string documentaryname, string customerName, string Kind)
        {
            if (BussName != null && BussName != "")
            {
                strss += "and  B_name like '%" + BussName + "%'";
            }
            if (customerName != null && customerName != "")
            {
                strss += "and C_name like '%" + customerName + "%'";
            }
            if (Kind != null && Kind != "")
            {
                strss += "and B_kind=" + int.Parse(Kind);
            }
            if (status != null && status != "")
            {
                strss += "and  B_status=" + int.Parse(status);
            }
            if (documentaryname != null && documentaryname != "")
            {
                strss += "and T_name like'%" + documentaryname + "%'";
            }
            if (Dates1 != null && Dates1 != "" && Dates2 != null && Dates2 != "")
            {
                strss += "and  B_datetime between convert(varchar(10),'" + Dates1 + "',111) and convert(varchar(10),'" + Dates2 + "',111)";
            }
            else
            {
                if (Dates1 != null && Dates1 != "")
                {
                    strss += "and B_datetime > convert(varchar(10),'" + Dates1 + "',111)";
                }
                if (Dates2 != null && Dates2 != "")
                {
                    strss += "and  B_datetime < convert(varchar(10),'" + Dates2 + "',111)";
                }
            }

            return strss;
        }
        #endregion




    }
}
