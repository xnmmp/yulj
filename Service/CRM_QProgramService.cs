using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;
namespace Service
{
    public class CRM_QProgramService
    {

        //添加方案报价
        public Result AddQprogram(CRM_QProgram cq)
        {
            Result result = new Result();
            if (CRM_QProgramSQL.AddQprogram(cq))
            {
                    result.state = 1;
                    result.msg = "添加成功！";
            }
            else
            {
                result.state = 2;
                result.msg = "添加失败！";
            }
            return result;
        }

        //查看方案报价
        public Result Qprogram(int rows, int page, string strWhere)
        {

            Result result = new Result();
            string tblName = "QprogramView";  //-----表名
            string strGetFields = "ID,B_name,C_name,O_datetime";//-----查询字段
            string fldName = "O_datetime"; //---排序字段
            int doCount = 0;
            int OrderType = 1;
            var list = CRM_QProgramSQL.Qprogram(rows, page, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            var count = CRM_businessSQL.BussinessScreenCount(tblName, strWhere);
            if (list == null)
            {
                result.state = 2;
                result.msg = "未查到方案报价信息！";
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

        //通过方案报价ID查看方案报价详情
        public Result LoadinforQprogram(string ID)
        {
            Result result = new Result();
            var list = CRM_QProgramSQL.LoadinforQprogram(ID);
            string url = list.O_fileURL.ToString();
            var ids = url.Split(',');
            string one = ids[0];
            string two = "";
            string three = "";
            if (list == null)
            {
                result.state = 2;
                result.msg = "查询不到该方案报价的详细信息！";
            }
            else if (ids[0] == "" || ids[0] == null)
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
                PicturesServiceser pse = new PicturesServiceser();
                var picrlist = pse.SelAll(one, two, three);
                list.O_fileURL = picrlist;
                result.data = list;
                result.state = 1;
            }
            return result;
        }
        //修改
        public Result UpdataQprogram(CRM_QProgram cq)
        {
            Result result = new Result();
            if (CRM_QProgramSQL.UpdataQprogram(cq))
            {
                result.state = 1;
                result.msg = "修改方案报价成功！";
            }
            else
            {
                result.state = 2;
                result.msg = "方案报价修改失败！";
            }
            return result;
        }
         //商机中看到的项目方案报价
        public Result QprogramByBuss(string O_businessID) {
            Result result = new Result();
            var list = CRM_QProgramSQL.QprogramByBuss(O_businessID);
            if (list==null)
            {
                result.state = 2;
                result.msg = "未找到方案报价！";
            }
            else
            {
                result.data = list;
                result.state = 1;
            }
            return result;
        }

    }
}
