using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;
using CommonSha;
namespace Service
{
    public class CRM_businessService
    {
        //添加商机
        public Result Addbusiness(CRM_business cb)
        {
            Result result = new Result();
            try
            {
                if (CRM_businessSQL.Addbusiness(cb))
                {
                    result.state = 1;
                    result.msg = "添加商机成功！";
                }
                else
                {
                    result.state = 2;
                    result.msg = "添加商机失败！";
                }
            }
            catch (Exception)
            {

                result.state = 2;
                result.msg = "您填写的文字可能带有符号违反规则请重新输入！";
            }

            return result;
        }
        //显示商机
        public List<CRM_business> SearchBussinessByRole(int page, int rows, string strWhere)
        {
            string tblName = "BussinessandCustomerView";  //-----表名
            string strGetFields = "ID,B_datetime,C_name,C_linkphone,C_address,C_linkname,B_kind,B_needcause,B_name,CustomerID,B_documentaryID";//-----查询字段
            string fldName = "B_datetime";
            int doCount = 0;
            int OrderType = 1;
            var list = CRM_businessSQL.SearchBussinessByRole(page, rows, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            return list;
        }
        //通过商机ID查询商机详情显示
        public Result SearchBussinessInfor(string ID)
        {
            Result result = new Result();
            CRM_business cb = CRM_businessSQL.SearchBussinessInfor(ID);
            if (cb == null)
            {
                result.state = 2;
                result.msg = "该商机不存在请重新选择查看！";
            }
            else
            {
                result.state = 1;
                result.data = cb;
            }
            return result;
        }
        //修改商机
        public Result UpdataBusiness(CRM_business cb)
        {
            Result result = new Result();
            if (CRM_businessSQL.UpdataBusiness(cb))
            {
                result.state = 1;
                result.msg = "修改商机成功！";
            }
            else
            {
                result.state = 2;
                result.msg = "修改商机失败！";
            }
            return result;
        }

        //修改商机筛选状态
        public Result UpdataBusinessStatus(string ID, int B_status, string B_giveupcause)
        {
            Result result = new Result();
            if (CRM_businessSQL.UpdataBusinessStatus(ID, B_status, B_giveupcause))
            {
                if (B_status == 1)
                {
                    result.state = 1;
                    result.msg = "该商机状态已修改为派单状态，您可以点击下方派单为商机派单！";
                }
                else if (B_status == 2)
                {
                    result.state = 2;
                    result.msg = "该商机状态已修改为转单状态！";
                }
                else if (B_status == 3)
                {
                    result.state = 3;
                    result.msg = "该商机状态已修改为放弃！";
                }
            }
            else
            {
                result.state = 4;
                result.msg = "抱歉您为商机分配状态失败！";
            }
            return result;
        }
        //修改跟单人(派单)
        public Result UpdataBusinessDocumentary(string ID, string Addbusinessman, string B_documentaryID, string B_documentaryneed)
        {
            Result result = new Result();
            CRM_User cu = LoginSQL.SearchCrmByID(B_documentaryID);
            if (cu != null)
            {

                if (CRM_businessSQL.UpdataBusinessDocumentary(ID, Addbusinessman, B_documentaryID, B_documentaryneed))
                {
                    if (string.IsNullOrWhiteSpace(cu.T_phone))
                    {
                        result.state = 1;
                        result.msg = "派单成功,该跟单人没有添写手机信息请您及时提醒该跟单人查看派单信息！";
                    }
                    else
                    {
                        result.state = 1;
                        CodeAlert ca = new CodeAlert();
                        int state = ca.SSM(cu.T_phone);
                        if (state == 2)
                        {
                            result.msg = "派单成功,已用短信方式提醒跟单人！";
                        }
                        else
                        {
                            result.msg = "派单成功,短信提醒失败！请你及时提醒跟单人查看派单信息！";
                        }
                    }
                }
                else
                {
                    result.state = 2;
                    result.msg = "派单失败！";
                }
            }
            else
            {
                result.state = 2;
                result.msg = "未找到该跟单人信息，请重新指定跟单人！";
            }
            return result;
        }
        //商机筛选
        public List<CRM_business> BussinessScreen(int rows, int page, string strWhere)
        {
            string tblName = "BussinessandCustomerView";  //-----表名
            string strGetFields = "ID,C_name,B_datetime,B_status,B_giveupcause,B_name";//-----查询字段
            string fldName = "B_datetime";
            int doCount = 0;
            int OrderType = 1;
            var list = CRM_businessSQL.BussinessScreen(rows, page, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            if (list == null)
            {
                return null;
            }
            return list;
        }

        //商机量统计
        public int BussinessScreenCount(string strWhere)
        {
            string tblName = "BussinessandCustomerView";  //-----表名
            var list = CRM_businessSQL.BussinessScreenCount(tblName, strWhere);
            return list;
        }
        //商机派单统计
        public int BussinessPaidanCount(string strWhere)
        {
            string tblName = "PaidanView";  //-----表名
            var list = CRM_businessSQL.BussinessScreenCount(tblName, strWhere);
            return list;
        }

        //商机派单显示（右边）
        public List<CRM_business> BussinessSendorders(int rows, int page, string strWhere)
        {
            string tblName = "PaidanView";  //-----表名
            string strGetFields = "ID,C_name,B_name,T_name,B_documentaryID,B_remark1,B_documentaryneed";//-----查询字段
            string fldName = "B_datetime"; //---排序字段
            int doCount = 0;
            int OrderType = 1;
            var list = CRM_businessSQL.BussinessSendorders(rows, page, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            if (list == null)
            {
                return null;
            }
            return list;
        }

    }
}
