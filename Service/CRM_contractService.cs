using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;
using System.Transactions;
namespace Service
{
    public class CRM_contractService
    {


        //添加合同
        public Result Addcontract(CRM_contract cco)
        {

            Result result = new Result();
            var list = CRM_businessSQL.SearchBussinessInfor(cco.P_businessID);
            if (list.B_projectQuote == 1)
            {
                result.state = 2;
                result.msg = "对不起您已经添加过此合同了不能再次添加，如有需要请修改！！";
            }
            else
            {
                using (TransactionScope sc = new TransactionScope())
                {
                    try
                    {
                        if (CRM_businessSQL.UpdataB_projectQuote(cco.P_businessID, 1, 1))
                        {

                            if (CRM_contractSQL.Addcontract(cco))
                            {
                                sc.Complete();
                                result.state = 1;
                                result.msg = "添加成功！";
                            }
                            else
                            {
                                result.state = 2;
                                result.msg = "添加失败！";
                            }
                        }
                        else
                        {
                            result.state = 2;
                            result.msg = "添加失败！";
                        }
                    }
                    catch (Exception)
                    {
                        result.state = 2;
                        result.msg = "添加失败！";
                    }
                }
            }
            return result;
        }

        //显示合同
        public Result Contract(int rows, int page, string strWhere)
        {
            Result result = new Result();
            string tblName = "ContractView";  //-----表名
            string strGetFields = "ID,B_name,C_name,P_datetime";//-----查询字段
            string fldName = "P_datetime";
            int doCount = 0;
            int OrderType = 1;
            var list = CRM_contractSQL.Contract(rows, page, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            var count = CRM_businessSQL.BussinessScreenCount(tblName, strWhere);
            if (list == null)
            {
                result.state = 2;
                result.msg = "未查询到合同！";
            }
            else
            {
                result.data = list;
                result.count = count;
                result.state = 1;
                result.page = page;
            }
            return result;
        }


        //显示合同详情
        public Result ContractInfor(string ID, int IsBuss, string P_businessID)
        {
            Result result = new Result();
            var list = CRM_contractSQL.ContractInfor(ID, IsBuss, P_businessID);
            if (list == null)
            {
                result.state = 2;
                result.msg = "未查询到详情！";
            }
            else
            {
                string url = list.P_URL1.ToString();
                string url2 = list.P_URL2.ToString();
                var ids = url.Split(',');
                var ids2 = url2.Split(',');
                string one = ids[0];
                string one2 = ids2[0];
                string two = "";
                string two2 = "";
                string three = "";
                string three2 = "";

                if ((ids[0] == "" || ids[0] == null) && (ids2[0] == "" || ids2[0] == null))
                {
                    result.state = 2;
                    result.msg = "没有文件数据！";
                }
                else
                {
                    if (ids.Length == 2)
                    {
                        two = ids[1];
                    }
                    else if (ids.Length == 3)
                    {
                        two = ids[1];
                        three = ids[2];
                    }
                    if (ids2.Length == 2)
                    {
                        two2 = ids2[1];
                    }
                    else if (ids2.Length == 3)
                    {
                        two2 = ids2[1];
                        three2 = ids2[2];
                    }
                    PicturesServiceser pse = new PicturesServiceser();
                    var picrlist1 = pse.SelAll(one, two, three);
                    var picrlist2 = pse.SelAll(one2, two2, three2);
                    list.P_URL1 = picrlist1;
                    list.P_URL2 = picrlist2;
                    result.state = 1;
                    result.data = list;
                }
            }

            return result;
        }


        //修改合同
        public Result UpdataContract(CRM_contract cco)
        {
            Result result = new Result();
            if (CRM_contractSQL.UpdataContract(cco))
            {
                result.state = 1;
                result.msg = "修改合同成功！";
            }
            else
            {
                result.state = 2;
                result.msg = "修改合同失败！";
            }
            return result;
        }


    }
}
