using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;
namespace Service
{
    public class CRM_visitService
    {
        //添加客户拜访
        public Result AddVisit(CRM_visit cv)
        {
            Result result = new Result();
            if (CRM_visitSQL.AddVisit(cv))
            {
                result.state = 1;
                result.msg = "添加客户拜访成功！";
            }
            else
            {
                result.state = 2;
                result.msg = "添加客户拜访失败！";
            }
            return result;
        }
        CRM_businessService cbs = new CRM_businessService();
        //查看客户拜访（右导航栏）
        public Result CustomerVisit(int rows, int page, string strWhere)
        {
            Result result = new Result();
            string tblName = "VisitView";  //-----表名
            string strGetFields = "ID,B_name,C_name,CV_datetime";//-----查询字段
            string fldName = "CV_datetime"; //---排序字段
            int doCount = 0;
            int OrderType = 1;
            var list = CRM_visitSQL.CustomerVisit(rows, page, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            int count = CustomerVisitCount(strWhere);
            if (list == null)
            {
                result.state = 2;
                result.msg = "未查到客户拜访！";
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
        //查看客户拜访数量统计
        public int CustomerVisitCount(string strWhere)
        {
            string tblName = "VisitView";  //-----表名
            var list = CRM_businessSQL.BussinessScreenCount(tblName, strWhere);
            return list;
        }

        //通过客户拜访ID查看客户拜访详情
        public Result SearchVisitByID(string ID)
        {
            Result result = new Result();
            var list = CRM_visitSQL.SearchVisitByID(ID);
            if (list == null)
            {
                result.state = 2;
                result.msg = "查询不到数据！";
            }
            else
            {
                string url = list.CV_fileurl.ToString();
                var ids = url.Split(',');
                string one = ids[0];
                string two = "";
                string three = "";
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
                list.CV_fileurl = picrlist;
                result.state = 1;
                result.data = list;
            }




            //else
            //{
            //    result.state = 1;
            //    result.data = list;
            //}
            return result;
        }

        //修改客户拜访
        public Result UpdataVisit(CRM_visit cv)
        {
            Result result = new Result();
            if (CRM_visitSQL.UpdataVisit(cv))
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

    }
}
