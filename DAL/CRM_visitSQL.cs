using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Entity;
namespace DAL
{
    /// <summary>
    /// 客户拜访
    /// </summary>
    public class CRM_visitSQL
    {
        //添加客户拜访
        public static bool AddVisit(CRM_visit cv)
        {
            string sql = string.Format(@"INSERT INTO [dbo].[CRM_visit]
           ([ID]
           ,[CV_datetime]
           ,[CV_ways]
           ,[CV_oppositepeople]
           ,[CV_purpose]
           ,[CV_talkdetails]
           ,[CV_feedback]
           ,[CV_lowerplan]
           ,[CV_fileurl]
           ,[CV_documentaryID]
           ,[CV_businessName]
           ,[CV_businessID]
           ,[CV_consumerName]
           ,[CV_consumerID]
           ,[CV_UserID]
           ,[CV_remark1]
           ,[CV_remark2])
     VALUES
           ('{0}'
           ,'{1}'
           ,{2}
           ,'{3}'
           ,{4}
           ,'{5}'
           ,{6}
           ,'{7}'
           ,'{8}'
           ,'{9}'
           ,'{10}'
           ,'{11}'
           ,'{12}'
           ,'{13}'
           ,'{14}'
           ,'{15}'
           ,'{16}')", cv.ID, cv.CV_datetime, cv.CV_ways, cv.CV_oppositepeople, cv.CV_purpose, cv.CV_talkdetails, cv.CV_feedback, cv.CV_lowerplan, cv.CV_fileurl, cv.CV_documentaryID, cv.CV_businessName, cv.CV_businessID, cv.CV_consumerName, cv.CV_consumerID, cv.CV_UserID, cv.CV_remark1, cv.CV_remark2);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }

        //查看客户拜访（右导航栏）
        public static List<CRM_visitLoad> CustomerVisit(int rows, int page, string tblName, string strWhere, string strGetFields, string fldName, int doCount, int OrderType)
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
                cvload.CV_datetime = DateTime.Parse(item["CV_datetime"].ToString());
                list.Add(cvload);
            }
            return list;
        }

        //通过客户拜访ID查看客户拜访详情
        public static CRM_visit SearchVisitByID(string ID)
        {
            string sql = "SearchVisitByIDPRO";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@ID",ID)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            CRM_visit cv = new CRM_visit();
            cv.ID = dt.Rows[0]["ID"].ToString();
            cv.CV_datetime = DateTime.Parse(dt.Rows[0]["CV_datetime"].ToString());
            cv.CV_ways = int.Parse(dt.Rows[0]["CV_ways"].ToString());
            cv.CV_oppositepeople = dt.Rows[0]["CV_oppositepeople"].ToString();
            cv.CV_purpose = int.Parse(dt.Rows[0]["CV_purpose"].ToString());
            cv.CV_talkdetails = dt.Rows[0]["CV_talkdetails"].ToString();
            cv.CV_feedback = int.Parse(dt.Rows[0]["CV_feedback"].ToString());
            cv.CV_lowerplan = dt.Rows[0]["CV_lowerplan"].ToString();
            cv.CV_fileurl = dt.Rows[0]["CV_fileurl"].ToString();
            cv.CV_businessName = dt.Rows[0]["CV_businessName"].ToString();
            cv.CV_consumerName = dt.Rows[0]["CV_consumerName"].ToString();
            cv.CV_documentaryID = dt.Rows[0]["CV_documentaryID"].ToString();
            cv.CV_businessID = dt.Rows[0]["CV_businessID"].ToString();
            cv.CV_consumerID = dt.Rows[0]["CV_consumerID"].ToString();
            cv.CV_UserID = dt.Rows[0]["CV_UserID"].ToString();
            cv.CV_remark1 = dt.Rows[0]["CV_remark1"].ToString();
            cv.CV_remark2 = dt.Rows[0]["CV_remark2"].ToString();
            return cv;
        }

        //修改客户拜访
        public static bool UpdataVisit(CRM_visit cv)
        {
            string sql = string.Format(@"UPDATE [dbo].[CRM_visit]
   SET [CV_ways] = {1}
      ,[CV_oppositepeople] = '{2}'
      ,[CV_purpose] = {3}
      ,[CV_talkdetails] = '{4}'
      ,[CV_feedback] = {5}
      ,[CV_lowerplan] = '{6}'
      ,[CV_fileurl] = '{7}'
      ,[CV_consumerName] = '{8}'
      ,[CV_remark1] = '{9}'
 WHERE ID='{0}'", cv.ID, cv.CV_ways, cv.CV_oppositepeople, cv.CV_purpose, cv.CV_talkdetails, cv.CV_feedback, cv.CV_lowerplan, cv.CV_fileurl, cv.CV_consumerName, cv.CV_remark1);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }

    }
}
