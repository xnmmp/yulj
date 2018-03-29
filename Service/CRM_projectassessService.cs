using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;
namespace Service
{
    public class CRM_projectassessService
    {
        //添加项目需求
        public Result Addprojectassess(CRM_projectassess cp)
        {
            Result result = new Result();
            var list = CRM_businessSQL.SearchBussinessInfor(cp.A_businessID);
            if (list.B_needsassessment == 1)
            {
                result.state = 2;
                result.msg = "对不起您已经评估过了不能再次评估，如有需要请修改！";
            }
            else if (CRM_businessSQL.UpdataB_projectQuote(cp.A_businessID, 2, 1))
            {
                if (CRM_projectassessSQL.Addprojectassess(cp))
                {
                    result.state = 1;
                    result.msg = "添加成功！";
                }
                else
                {
                    CRM_businessSQL.UpdataB_projectQuote(cp.A_businessID, 2, 0);
                    result.state = 2;
                    result.msg = "添加失败！";
                }
            }
            else
            {
                result.state = 2;
                result.msg = "添加失败！";
            }
            return result;
        }

        //显示所有需求评估
        public Result projectassess(int rows, int page, string strWhere)
        {
            Result result = new Result();
            string tblName = "ProjectassessView";  //-----表名
            string strGetFields = "ID,B_name,C_name,A_datetime,A_conclusion";//-----查询字段
            string fldName = "A_datetime";
            int doCount = 0;
            int OrderType = 1;
            var list = CRM_projectassessSQL.projectassess(rows, page, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            var count = CRM_businessSQL.BussinessScreenCount(tblName, strWhere);
            if (list == null)
            {
                result.state = 2;
                result.msg = "没有数据！";
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
        //通过ID查看需求评估详情
        public Result projectassessInfor(string ID)
        {
            Result result = new Result();
            var list = CRM_projectassessSQL.projectassessInfor(ID, 2, "");
            if (list == null)
            {
                result.msg = "详情查看失败，请重新操作1";
                result.state = 2;
            }
            else
            {

                result.state = 1;
                result.data = list;
            }
            return result;
        }


        //在商机内看到的需求评估
        public Result projectassessInforBybuss(string A_businessID)
        {
            Result result = new Result();
            var list = CRM_projectassessSQL.projectassessInfor("", 1, A_businessID);
            if (list == null)
            {
                result.msg = "详情查看失败，请重新操作1";
                result.state = 2;
            }
            else
            {
                result.state = 1;
                result.data = list;
            }
            return result;
        }

        //修改需求评估
        public Result Updataprojectassess(CRM_projectassess cp)
        {
            Result result = new Result();
            if (CRM_projectassessSQL.Updataprojectassess(cp))
            {
                result.state = 1;
                result.msg = "修改成功!";
            }
            else
            {
                result.state = 2;
                result.msg = "修改失败!";
            }
            return result;
        }

    }
}
