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
    public class CRM_contractSQL
    {
        //添加合同
        public static bool Addcontract(CRM_contract cco)
        {
            string sql = string.Format(@"INSERT INTO [dbo].[CRM_contract]
           ([ID]
           ,[P_cookiename]
           ,[P_startdatetime]
           ,[P_enddatetime]
           ,[P_kindone]
           ,[P_kindtwo]
           ,[P_kindthree]
           ,[P_name]
           ,[P_money]
           ,[P_URL1]
           ,[P_URL2]
           ,[P_businessName]
           ,[P_businessID]
           ,[P_customerName]
           ,[P_customerID]
           ,[P_documentaryID]
           ,[UserID]
           ,[remark1]
           ,[remark2]
           ,[remark3]
           ,[P_datetime])
     VALUES
           ('{0}'
           ,'{1}'
           ,'{2}'
           ,'{3}'
           ,{4}
           ,{5}
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
           ,'{16}'
           ,'{17}'
           ,'{18}'
           ,'{19}'
           ,'{20}')", cco.ID, cco.P_cookiename, cco.P_startdatetime, cco.P_enddatetime, cco.P_kindone, cco.P_kindtwo, cco.P_kindthree, cco.P_name, cco.P_money, cco.P_URL1, cco.P_URL2, cco.P_businessName, cco.P_businessID, cco.P_customerName, cco.P_customerID, cco.P_documentaryID, cco.UserID, cco.remark1, cco.remark2, cco.remark3, cco.P_datetime);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }

        //显示合同
        public static List<CRM_visitLoad> Contract(int rows, int page, string tblName, string strWhere, string strGetFields, string fldName, int doCount, int OrderType)
        {
            DataTable dt = CRM_businessSQL.paging(rows, page, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            List<CRM_visitLoad> list = new List<CRM_visitLoad>();
            foreach (DataRow item in dt.Rows)
            {
                CRM_visitLoad cv = new CRM_visitLoad();
                cv.CV_businessName = item["B_name"].ToString();
                cv.CV_consumerName = item["C_name"].ToString();
                cv.P_startdatetime = item["P_datetime"].ToString();
                cv.ID = item["ID"].ToString();
                list.Add(cv);
            }
            return list;
        }


        //显示合同详情
        public static CRM_contract ContractInfor(string ID, int IsBuss, string P_businessID)
        {

            string sql = "ContractInforPRO";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@ID",ID),
            new SqlParameter("@IsBuss",IsBuss),
            new SqlParameter("@P_businessID",P_businessID)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                CRM_contract cco = new CRM_contract();
                cco.ID = dt.Rows[0]["ID"].ToString();
                cco.P_datetime = DateTime.Parse(dt.Rows[0]["P_datetime"].ToString());
                cco.P_cookiename = dt.Rows[0]["P_cookiename"].ToString();
                cco.P_startdatetime = dt.Rows[0]["P_startdatetime"].ToString();
                cco.P_enddatetime = dt.Rows[0]["P_enddatetime"].ToString();
                cco.P_kindone = int.Parse(dt.Rows[0]["P_kindone"].ToString());
                cco.P_kindtwo = int.Parse(dt.Rows[0]["P_kindtwo"].ToString());
                cco.P_kindthree = int.Parse(dt.Rows[0]["P_kindthree"].ToString());
                cco.P_name = dt.Rows[0]["P_name"].ToString();
                cco.P_money = dt.Rows[0]["P_money"].ToString();
                cco.P_URL1 = dt.Rows[0]["P_URL1"].ToString();
                cco.P_URL2 = dt.Rows[0]["P_URL2"].ToString();
                cco.P_businessName = dt.Rows[0]["P_businessName"].ToString();
                cco.P_businessID = dt.Rows[0]["P_businessID"].ToString();
                cco.P_customerName = dt.Rows[0]["P_customerName"].ToString();
                cco.P_customerID = dt.Rows[0]["P_customerID"].ToString();
                cco.P_documentaryID = dt.Rows[0]["P_documentaryID"].ToString();
                cco.UserID = dt.Rows[0]["UserID"].ToString();
                cco.remark1 = dt.Rows[0]["B_datetime"].ToString();
                cco.remark2 = dt.Rows[0]["remark2"].ToString();
                cco.remark3 = dt.Rows[0]["remark3"].ToString();
                return cco;
            }
        }

        //修改合同
        public static bool UpdataContract(CRM_contract cco)
        {
            string sql = string.Format(@"UPDATE [dbo].[CRM_contract]
   SET [P_cookiename] = '{1}'
      ,[P_startdatetime] = '{2}'
      ,[P_enddatetime] = '{3}'
      ,[P_kindone] = {4}
      ,[P_kindtwo] = {5}
      ,[P_kindthree] = {6}
      ,[P_name] = '{7}'
      ,[P_money] = '{8}'
      ,[P_URL1] = '{9}'
      ,[P_URL2] = '{10}'
      ,[remark1] = '{11}'
      ,[remark2] = '{12}'
      ,[remark3] = '{13}'
 WHERE ID='{0}'", cco.ID, cco.P_cookiename, cco.P_startdatetime, cco.P_enddatetime, cco.P_kindone, cco.P_kindtwo, cco.P_kindthree, cco.P_name, cco.P_money, cco.P_URL1, cco.P_URL2, cco.remark1, cco.remark2, cco.remark3);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }






    }
}
