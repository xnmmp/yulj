using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Entity;
namespace CRM.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        CustomerService cus = new CustomerService();
        SAuthoriza sau = new SAuthoriza();



        #region //添加客户数据
        public ActionResult AddCustomer(string Username, string Token, string C_UserID, string C_name, string C_address, string C_linkname, string C_linkphone, string C_province, string C_city, string C_district, string C_kind, string C_cooperation, string C_scale, string C_industry, string C_remark1, string C_remark2)
        {
            //Username = "pppp";
            //Token = "3a4f1e713a3048e7a4e6118e07395f3b";
            //C_name = "牛逼";
            //C_address = "松江";
            //C_linkname = "小丽";
            //C_linkphone = "10086";
            //C_province = "110000";
            //C_city = "110100";
            //C_district = "110102";
            //C_kind = "1";
            //C_cooperation = "2";
            //C_scale = "2";
            //C_industry = "IT";
            Result result = new Result();
            if (C_UserID == null || C_UserID == "undefined" || C_name == null || C_address == null || C_linkname == null || C_linkphone == null || C_province == null || C_city == null || C_district == null || C_kind == null || C_cooperation == null || C_scale == null)
            {
                result.state = 5;
                result.msg = "可能还有些填写信息不完整！";
            }
            else if (C_UserID == "" || C_name == "" || C_address == "" || C_linkname == "" || C_linkphone == "" || C_province == "" || C_city == "" || C_district == "" || C_kind == "" || C_cooperation == "" || C_scale == "")
            {
                result.state = 5;
                result.msg = "可能还有些填写信息不完整！";
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
                    string ID = cus.SearchID_ByName(Username);
                    CRM_Customer cc = new CRM_Customer();
                    cc.ID = Guid.NewGuid().ToString("N");
                    cc.C_UserID = C_UserID;
                    cc.C_name = C_name;
                    cc.C_address = C_address;
                    cc.C_linkname = C_linkname;
                    cc.C_linkphone = C_linkphone;
                    cc.C_province = C_province;
                    cc.C_city = C_city;
                    cc.C_district = C_district;
                    cc.C_kind = int.Parse(C_kind);
                    cc.C_cooperation = int.Parse(C_cooperation);
                    cc.C_scale = int.Parse(C_scale);
                    cc.C_industry = C_industry;
                    cc.C_remark1 = ID;
                    cc.C_remark2 = "";
                    result = cus.Add_Customer(cc);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion





        #region//通过名称模糊查询
        public ActionResult SearchByname(string Username, string Token, string C_name, string C_UserID)
        {
            //Username = "pppp";
            //Token = "695cd9b1dd5f45a6a4845444c58c17a0";
            //C_name = "";
            //C_UserID = "cce19a9951fd4b188deb8319b6835f68";
            Result result = new Result();
            if (C_UserID == null || C_UserID == "")
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
                    result = cus.SearchByname(C_name, C_UserID);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion






        #region////通过ID查询出客户所有信息
        public ActionResult SearchCustomerAllByID(string Username, string Token, string ID)
        {
            //Username = "pppxxx";
            //Token = "ae9698bae70a415b83a01d743b477230";
            //ID = "5138e76420244df894aa1dccf80291ae";
            EAuthentication eau = sau.Authentication(Username, Token);
            Result result = new Result();
            if (!eau.state)
            {
                result.state = -1;
                result.msg = "未登录！";
            }
            else
            {
                result = cus.SearchCustomerAllByID(ID);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion





        #region //查看客户
        public ActionResult loadCustomer(string Username, string Token, string rows, string page, string UserID, string customerName)
        {
            //Username = "qwert";
            //Token = "cdd1e026e9704ce39fe0a2f4eb33a8a6";
            //rows = "10";
            //page = "1";
            //UserID = "cce19a9951fd4b188deb8319b6835f68";

            Result result = new Result();
            if (rows == null || rows == "" || page == null || page == "" || UserID == "" || UserID == null)
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
                else if (eau.Role != "1" && eau.Role != "2" && eau.Role != "3")
                {
                    result.state = -2;
                    result.msg = "权限不够！";
                }
                else
                {
                    string strss = "";
                    string strWhere = "";
                    if (eau.Role == "1" || eau.Role == "2")
                    {
                        strss = "C_UserID=" + "'" + UserID + "'";
                        strWhere = searchSaixuan(strss, customerName);
                    }
                    else
                    {
                        //当前人的ID
                        string ID = cus.SearchID_ByName(Username);
                        strss = "C_remark1='" + ID + "'";
                        strWhere = searchSaixuan(strss, customerName);
                    }
                    result = cus.loadCustomer(int.Parse(rows), int.Parse(page), strWhere);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion





        #region//修改客户
        public ActionResult UpdataCustomer(string Username, string Token, string ID, string C_name, string C_address, string C_linkname, string C_linkphone, string C_province, string C_city, string C_district, string C_kind, string C_cooperation, string C_scale, string C_industry, string C_remark1, string C_remark2)
        {
            //Username = "pppp";
            //Token = "171701c5467f452f9d5ee5eec4f3c670";
            //ID = "3ecf1e9b5cd64cf193987910e3ee79b5";
            //C_name = "弱智邓111";
            //C_address = "三亚111";
            //C_linkname = "凤姐11";
            //C_linkphone = "1141111";
            //C_province = "150000111";
            //C_city = "15060011";
            //C_district = "150624111";
            //C_kind = "111";
            //C_cooperation = "211";
            //C_scale = "1111";


            Result result = new Result();
            if (ID == null || C_name == null || C_address == null || C_linkname == null || C_linkphone == null || C_province == null || C_city == null || C_district == null || C_kind == null || C_cooperation == null || C_scale == null)
            {
                result.state = 3;
                result.msg = "您可能还有些重要信息没有填写！";
            }
            else if (ID == "" || C_name == "" || C_address == "" || C_linkname == "" || C_linkphone == "" || C_province == "" || C_city == "" || C_district == "" || C_kind == "" || C_cooperation == "" || C_scale == "")
            {
                result.state = 3;
                result.msg = "您可能还有些重要信息没有填写！";
            }
            else
            {
                EAuthentication eau = sau.Authentication(Username, Token);
                if (!eau.state)
                {
                    result.state = -1;
                    result.msg = "未登录！";
                }
                else if (eau.Role != "1" && eau.Role != "2" && eau.Role != "3")
                {
                    result.state = -2;
                    result.msg = "权限不够！";
                }
                else
                {
                    CRM_Customer cc = new CRM_Customer();
                    cc.ID = ID;
                    cc.C_name = C_name;
                    cc.C_address = C_address;
                    cc.C_linkname = C_linkname;
                    cc.C_linkphone = C_linkphone;
                    cc.C_province = C_province;
                    cc.C_city = C_city;
                    cc.C_district = C_district;
                    cc.C_kind = int.Parse(C_kind);
                    cc.C_cooperation = int.Parse(C_cooperation);
                    cc.C_scale = int.Parse(C_scale);
                    cc.C_industry = C_industry;
                    cc.C_remark1 = C_remark1;
                    cc.C_remark2 = C_remark2;
                    result = cus.UpdataCustomer(cc);
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion





        #region//查询
        public string searchSaixuan(string strss, string customerName)
        {
            if (customerName != null && customerName != "")
            {
                strss += "and  C_name like '%" + customerName + "%'";
            }
            return strss;
        }
        #endregion

    }
}
