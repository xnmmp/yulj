using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;
namespace Service
{
    public class CustomerService
    {
        //添加客户(必须插入管理员ID)
        public Result Add_Customer(CRM_Customer cc)
        {
            Result result = new Result();

            if (CustomerSQL.Add_Customer(cc))
            {
                result.state = 1;
                result.msg = "添加客户成功！";
            }
            else
            {
                result.state = 2;
                result.msg = "添加失败！";
            }
            return result;

        }

        //通过ID查询客户名称
        public CRM_Customer seachNamerbyID(string ID)
        {
            return CustomerSQL.seachNamerbyID(ID);
        }

        //通过名称模糊查询
        public Result SearchByname(string C_name, string C_UserID)
        {

            Result result = new Result();
            var list = CustomerSQL.SearchByname(C_name, C_UserID);
            if (list == null)
            {
                result.state = 2;
            }
            else
            {
                result.state = 1;
                result.data = list;
            }
            return result;
        }

        ////客户通过ID查询出所有信息
        public Result SearchCustomerAllByID(string ID)
        {
            Result result = new Result();
            var list = CustomerSQL.SearchCustomerAllByID(ID);
            if (list == null)
            {
                result.state = 2;
                result.msg = "客户信息显示失败！";
            }
            else
            {
                result.state = 1;
                result.data = list;
            }
            return result;
        }


        //查看客户
        public Result loadCustomer(int rows, int page, string strWhere)
        {
            Result result = new Result();
            string tblName = "CRM_Customer";  //-----表名
            string strGetFields = "ID,C_name,C_linkphone,C_linkname,C_cooperation";//-----查询字段
            string fldName = "C_name"; //---排序字段
            int doCount = 0;
            int OrderType = 1;
            var list = CustomerSQL.loadCustomer(rows, page, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            var count = CRM_businessSQL.BussinessScreenCount(tblName, strWhere);
            if (list == null)
            {
                result.state = 2;
                result.msg = "未查询到客户信息！";
            }
            else
            {
                result.state = 1;
                result.data = list;
                result.count = count;
                result.page = page;
            }
            return result;
        }
        //修改客户
        public Result UpdataCustomer(CRM_Customer cc)
        {

            Result result = new Result();
            if (CustomerSQL.UpdataCustomer(cc))
            {
                result.state = 1;
                result.msg = "修改成功！";
            }
            else
            {
                result.state = 2;
                result.msg = "修改失败！";
            }

            return result;
        }



        #region 在添加客户的时候通过UserName查询出用户的ID
        public string SearchID_ByName(string name)
        {
            Result result = new Result();
            var UserInfo = LoginSQL.SearchCrmuser(name);
            string ID = UserInfo.ID;
            return ID;
        }
        #endregion


    }
}
