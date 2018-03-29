using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    public class CRM_businessSQL
    {
        //公共分页带条件查询
        public static DataTable paging(int rows, int page, string tblName, string strWhere, string strGetFields, string fldName, int doCount, int OrderType)
        {
            string sql = "pagination";
            SqlParameter[] parm = new SqlParameter[] { 
            
                new SqlParameter("@tblName",tblName),
                new SqlParameter("@strGetFields",strGetFields),
                new SqlParameter("@fldName",fldName),
                new SqlParameter("@PageSize",rows),
                new SqlParameter("@PageIndex",page),
                new SqlParameter("@doCount",doCount),
                new SqlParameter("@OrderType",OrderType),
                new SqlParameter("@strWhere",strWhere),
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            return dt;
        }



        //显示商机
        public static List<CRM_business> SearchBussinessByRole(int page, int rows, string tblName, string strWhere, string strGetFields, string fldName, int doCount, int OrderType)
        {
            DataTable dt = paging(rows, page, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                List<CRM_business> list = new List<CRM_business>();
                foreach (DataRow item in dt.Rows)
                {
                    CRM_business cb = new CRM_business();
                    cb.ID = item["ID"].ToString();
                    cb.CustomerID = item["CustomerID"].ToString();
                    cb.C_name = item["C_name"].ToString();
                    cb.B_name = item["B_name"].ToString();
                    cb.C_address = item["C_address"].ToString();
                    cb.C_linkphone = item["C_linkphone"].ToString();
                    cb.C_linkname = item["C_linkname"].ToString();
                    cb.B_datetime = DateTime.Parse(item["B_datetime"].ToString());
                    cb.B_documentaryID = item["B_documentaryID"].ToString();
                    cb.B_kind = int.Parse(item["B_kind"].ToString());
                    cb.B_needcause = item["B_needcause"].ToString();
                    list.Add(cb);
                }
                return list;
            }

        }

        //通过跟单人ID查询出跟单人名称
        public static string CRM_searchdocumentaryname(string ID)
        {
            string sst = "";
            string sql = "CRM_searchdocumentaryname";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@ID",ID)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                sst = "";
            }
            else
            {
                sst = dt.Rows[0]["T_name"].ToString();
            }
            return sst;
        }
        //通过商机ID查询商机详情显示
        public static CRM_business SearchBussinessInfor(string ID)
        {
            string sql = "SearchBussinessInforPRO";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@ID",ID)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            CRM_business cb = new CRM_business();
            var kinds = new Kinds();
            Service_one so = new Service_one();
            Service_two st = new Service_two();
            cb.ID = dt.Rows[0]["ID"].ToString();
            cb.B_name = dt.Rows[0]["B_name"].ToString();
            cb.B_datetime = DateTime.Parse(dt.Rows[0]["B_datetime"].ToString());
            cb.B_source = int.Parse(dt.Rows[0]["B_source"].ToString());
            cb.B_kind = int.Parse(dt.Rows[0]["B_kind"].ToString());
            cb.B_servicekind1 = dt.Rows[0]["B_servicekind1"].ToString();
            cb.B_servicekind2 = dt.Rows[0]["B_servicekind2"].ToString();
            so = Service_kindSQL.SelectonenameByid(cb.B_servicekind1);
            st = Service_kindSQL.SelecttwonameByid(cb.B_servicekind2);
            cb.B_needcause = dt.Rows[0]["B_needcause"].ToString();
            cb.B_neednumber = dt.Rows[0]["B_neednumber"].ToString();
            cb.Addbusinessman = dt.Rows[0]["Addbusinessman"].ToString();
            cb.CustomerID = dt.Rows[0]["CustomerID"].ToString();
            cb.B_documentaryID = dt.Rows[0]["B_documentaryID"].ToString();
            if (cb.B_documentaryID!=null&&cb.B_documentaryID!="")
            {
                cb.B_documentaryname = CRM_searchdocumentaryname(cb.B_documentaryID);
            }
            else
            {
                cb.B_documentaryname = "";
            }
            cb.B_fistuseerID = dt.Rows[0]["B_fistuseerID"].ToString();
            cb.B_status = int.Parse(dt.Rows[0]["B_status"].ToString());
            cb.B_giveupcause = dt.Rows[0]["B_giveupcause"].ToString();
            cb.B_documentaryneed = dt.Rows[0]["B_documentaryneed"].ToString();
            cb.B_needsassessment = int.Parse(dt.Rows[0]["B_needsassessment"].ToString());
            cb.B_projectQuote = int.Parse(dt.Rows[0]["B_projectQuote"].ToString());
            cb.C_address = dt.Rows[0]["C_address"].ToString();
            cb.C_name = dt.Rows[0]["C_name"].ToString();
            cb.C_linkname = dt.Rows[0]["C_linkname"].ToString();
            cb.C_linkphone = dt.Rows[0]["C_linkphone"].ToString();
            cb.B_remark1 = dt.Rows[0]["B_remark1"].ToString();
            cb.B_remark2 = dt.Rows[0]["B_remark2"].ToString();
            kinds.so = so;
            kinds.st = st;
            cb.Kinds = kinds;
            return cb;
        }

        //添加商机
        public static bool Addbusiness(CRM_business cb)
        {
            string sql = string.Format(@"INSERT INTO [dbo].[CRM_business]
           ([ID]
           ,[B_name]
           ,[B_datetime]
           ,[B_source]
           ,[B_kind]
           ,[B_servicekind1]
           ,[B_servicekind2]
           ,[B_needcause]
           ,[B_neednumber]
           ,[Addbusinessman]
           ,[CustomerID]
           ,[B_documentaryID]
           ,[B_fistuseerID]
           ,[B_status]
           ,[B_giveupcause]
           ,[B_documentaryneed]
           ,[B_needsassessment]
           ,[B_projectQuote]
           ,[B_remark1]
           ,[B_remark2])
     VALUES
           ('{0}'
           ,'{1}'
           ,'{2}'
           ,{3}
           ,{4}
           ,'{5}'
           ,'{6}'
           ,'{7}'
           ,'{8}'
           ,'{9}'
           ,'{10}'
           ,'{11}'
           ,'{12}'
           ,{13}
           ,'{14}'
           ,'{15}'
           ,{16}
           ,{17}
           ,'{18}'
           ,'{19}')", cb.ID, cb.B_name, cb.B_datetime, cb.B_source, cb.B_kind, cb.B_servicekind1, cb.B_servicekind2, cb.B_needcause, cb.B_neednumber, cb.Addbusinessman, cb.CustomerID, cb.B_documentaryID, cb.B_fistuseerID, cb.B_status, cb.B_giveupcause, cb.B_documentaryneed, cb.B_needsassessment, cb.B_projectQuote, cb.B_remark1, cb.B_remark2);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }
        //修改商机
        public static bool UpdataBusiness(CRM_business cb)
        {
            string sql = string.Format(@"UPDATE [dbo].[CRM_business]
   SET [B_name] = '{1}'
      ,[B_source] = {2}
      ,[B_kind] = {3}
      ,[B_servicekind1] ='{4}'
      ,[B_servicekind2] = '{5}'
      ,[B_needcause] = '{6}'
      ,[B_neednumber] = '{7}'
      ,[CustomerID] = '{8}'
 WHERE ID='{0}'", cb.ID, cb.B_name, cb.B_source, cb.B_kind, cb.B_servicekind1, cb.B_servicekind2, cb.B_needcause, cb.B_neednumber, cb.CustomerID);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }

        //修改商机筛选状态
        public static bool UpdataBusinessStatus(string ID, int B_status, string B_giveupcause)
        {
            string sql = string.Format(@"UPDATE [dbo].[CRM_business]
   SET [B_status] = {1}
      ,[B_giveupcause] ='{2}'
 WHERE ID='{0}'", ID, B_status, B_giveupcause);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }


        //修改跟单人(派单)
        public static bool UpdataBusinessDocumentary(string ID, string Addbusinessman, string B_documentaryID, string B_documentaryneed)
        {
            string sql = string.Format(@"UPDATE [dbo].[CRM_business]
   SET [B_remark1]='{1}'
      ,[B_documentaryID] = '{2}'
      ,[B_documentaryneed] = '{3}'
 WHERE ID='{0}'", ID, Addbusinessman, B_documentaryID, B_documentaryneed);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }

        //显示商机筛选（右边导航）
        public static List<CRM_business> BussinessScreen(int rows, int page, string tblName, string strWhere, string strGetFields, string fldName, int doCount, int OrderType)
        {
            DataTable dt = paging(rows, page, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            List<CRM_business> list = new List<CRM_business>();
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            foreach (DataRow item in dt.Rows)
            {
                CRM_business cb = new CRM_business();
                cb.ID = item["ID"].ToString();
                cb.B_name = item["B_name"].ToString();
                cb.C_name = item["C_name"].ToString();
                cb.B_giveupcause = item["B_giveupcause"].ToString();
                cb.B_datetime = DateTime.Parse(item["B_datetime"].ToString());
                cb.B_status = int.Parse(item["B_status"].ToString());
                list.Add(cb);
            }
            return list;
        }
        //显示商机筛选总数量（右边导航）
        public static int BussinessScreenCount(string tblName, string strWhere)
        {
            Result result = new Result();
            string strGetFields = "ID"; //----查询的字段
            string fldName = "";//-----排序字段
            string sql = "pagination";
            int page = 0;
            int rows = 0;
            int OrderType = 0;
            int doCount = 1;
            SqlParameter[] parm = new SqlParameter[] { 
            
                new SqlParameter("@tblName",tblName),
                new SqlParameter("@strGetFields",strGetFields),
                new SqlParameter("@fldName",fldName),
                new SqlParameter("@PageSize",page),
                new SqlParameter("@PageIndex",rows),
                new SqlParameter("@doCount",doCount),
                new SqlParameter("@OrderType",OrderType),
                new SqlParameter("@strWhere",strWhere),
            };
            int count = DbHelperSQL.ExecuteCheck(sql, parm, CommandType.StoredProcedure);
            return count;
        }

        //显示派单（右边导航）
        public static List<CRM_business> BussinessSendorders(int rows, int page, string tblName, string strWhere, string strGetFields, string fldName, int doCount, int OrderType)
        {
            DataTable dt = paging(rows, page, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            List<CRM_business> list = new List<CRM_business>();
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            foreach (DataRow item in dt.Rows)
            {
                CRM_business cb = new CRM_business();
                cb.ID = item["ID"].ToString();
                cb.C_name = item["C_name"].ToString();
                cb.B_name = item["B_name"].ToString();
                cb.B_documentaryID = item["B_documentaryID"].ToString();
                cb.C_address = item["T_name"].ToString();  //------跟单人名称
                cb.B_remark1 = item["B_remark1"].ToString();//-----派单人名称
                cb.B_documentaryneed = item["B_documentaryneed"].ToString();
                list.Add(cb);
            }
            return list;
        }

        //添加合同j或者需求评估修改商机中报价字段
        public static bool UpdataB_projectQuote(string ID, int isB_projectQuote, int status)
        {
            string sql = "";
            if (isB_projectQuote == 1)
            {
                sql = string.Format(@"UPDATE [dbo].[CRM_business]
   SET [B_projectQuote] ={1}
 WHERE ID='{0}'", ID, status);
            }
            else if (isB_projectQuote == 2)
            {
                sql = string.Format(@"UPDATE [dbo].[CRM_business]
   SET [B_needsassessment]={1}
 WHERE ID='{0}'", ID, status);
            }
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }

    }
}
