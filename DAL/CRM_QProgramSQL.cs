using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entity;
namespace DAL
{
    /// <summary>
    /// 方案报价
    /// </summary>
    public class CRM_QProgramSQL
    {
        //添加方案报价
        public static bool AddQprogram(CRM_QProgram cq)
        {
            string sql = string.Format(@"INSERT INTO [dbo].[CRM_QProgram]
           ([ID]
           ,[O_datetime]
           ,[O_consumerpeople]
           ,[O_conferaddress]
           ,[O_tokleinfor]
           ,[O_fileURL]
           ,[O_nextplan]
           ,[O_customerfeedback]
           ,[O_businessName]
           ,[O_businessID]
           ,[O_consumerName]
           ,[O_consumerID]
           ,[O_documentaryID]
           ,[remark1]
           ,[remark2]
           ,[O_UserID])
     VALUES
           ('{0}'
           ,'{1}'
           ,'{2}'
           ,'{3}'
           ,'{4}'
           ,'{5}'
           ,'{6}'
           ,{7}
           ,'{8}'
           ,'{9}'
           ,'{10}'
           ,'{11}'
           ,'{12}'
           ,'{13}'
           ,'{14}'
           ,'{15}')", cq.ID, cq.O_datetime, cq.O_consumerpeople, cq.O_conferaddress, cq.O_tokleinfor, cq.O_fileURL, cq.O_nextplan, cq.O_customerfeedback, cq.O_businessName, cq.O_businessID, cq.O_consumerName, cq.O_consumerID, cq.O_documentaryID, cq.remark1, cq.remark2, cq.O_UserID);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }
        //查看方案报价
        public static List<CRM_visitLoad> Qprogram(int rows, int page, string tblName, string strWhere, string strGetFields, string fldName, int doCount, int OrderType)
        {
            DataTable dt = CRM_businessSQL.paging(rows, page, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            List<CRM_visitLoad> list = new List<CRM_visitLoad>();
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            foreach (DataRow item in dt.Rows)
            {
                CRM_visitLoad cvload = new CRM_visitLoad();
                cvload.ID = item["ID"].ToString();
                cvload.CV_businessName = item["B_name"].ToString();
                cvload.CV_consumerName = item["C_name"].ToString();
                cvload.CV_datetime = DateTime.Parse(item["O_datetime"].ToString());
                list.Add(cvload);
            }
            return list;
        }

        //通过方案报价ID查看方案报价详情
        public static CRM_QProgram LoadinforQprogram(string ID)
        {
            string sql = "LoadinforQprogramPRO";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@ID",ID)
        };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                CRM_QProgram cq = new CRM_QProgram();
                cq.ID = dt.Rows[0]["ID"].ToString();
                cq.O_datetime = DateTime.Parse(dt.Rows[0]["O_datetime"].ToString());
                cq.O_consumerpeople = dt.Rows[0]["O_consumerpeople"].ToString();
                cq.O_conferaddress = dt.Rows[0]["O_conferaddress"].ToString();
                cq.O_tokleinfor = dt.Rows[0]["O_tokleinfor"].ToString();
                cq.O_fileURL = dt.Rows[0]["O_fileURL"].ToString();
                cq.O_nextplan = dt.Rows[0]["O_nextplan"].ToString();
                cq.O_customerfeedback = int.Parse(dt.Rows[0]["O_customerfeedback"].ToString());
                cq.O_businessName = dt.Rows[0]["O_businessName"].ToString();
                cq.O_businessID = dt.Rows[0]["O_businessID"].ToString();
                cq.O_consumerName = dt.Rows[0]["O_consumerName"].ToString();
                cq.O_consumerID = dt.Rows[0]["O_consumerID"].ToString();
                cq.O_documentaryID = dt.Rows[0]["O_documentaryID"].ToString();
                cq.O_UserID = dt.Rows[0]["O_UserID"].ToString();
                cq.remark1 = dt.Rows[0]["remark1"].ToString();
                cq.remark2 = dt.Rows[0]["remark1"].ToString();
                return cq;
            }
        }
        //修改
        public static bool UpdataQprogram(CRM_QProgram cq)
        {
            string sql = string.Format(@"UPDATE [dbo].[CRM_QProgram]
   SET [O_consumerpeople] ='{1}'
      ,[O_conferaddress] = '{2}'
      ,[O_tokleinfor] = '{3}'
      ,[O_fileURL] = '{4}'
      ,[O_nextplan] = '{5}'
      ,[O_customerfeedback] = {6}
      ,[remark1] = '{7}'
      ,[remark2] = '{8}'
 WHERE ID='{0}'", cq.ID, cq.O_consumerpeople, cq.O_conferaddress, cq.O_tokleinfor, cq.O_fileURL, cq.O_nextplan, cq.O_customerfeedback, cq.remark1, cq.remark2);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }

        //商机中看到的项目方案报价
        public static List<CRM_QProgram> QprogramByBuss(string O_businessID)
        {
            string sql = "QprogramByBussPRO";
            SqlParameter[] parm = new SqlParameter[] { 
             new SqlParameter("@O_businessID",O_businessID)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            List<CRM_QProgram> list = new List<CRM_QProgram>();
            foreach (DataRow item in dt.Rows)
            {
                CRM_QProgram cq = new CRM_QProgram();
                cq.ID = item["ID"].ToString();
                cq.O_datetime = DateTime.Parse(item["O_datetime"].ToString());
                cq.O_consumerpeople = item["O_consumerpeople"].ToString();
                cq.O_customerfeedback = int.Parse(item["O_customerfeedback"].ToString());
                list.Add(cq);
            }
            return list;
        }



    }
}
