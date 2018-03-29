using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data;
using System.Data.SqlClient;
using DAL;
namespace DAL
{
    public class CRM_projectassessSQL
    {

        //添加项目需求
        public static bool Addprojectassess(CRM_projectassess cp)
        {
            string sql = string.Format(@"INSERT INTO [dbo].[CRM_projectassess]
           ([ID]
           ,[A_datetime]
           ,[A_durationneed]
           ,[A_constructneed]
           ,[A_acceptanceneed]
           ,[A_defaultmakeup]
           ,[A_aptitudes]
           ,[A_fundsbackfront]
           ,[A_projectscale]
           ,[A_budget]
           ,[A_businessmass]
           ,[A_compete]
           ,[A_consumerkey]
           ,[A_kickbackneed]
           ,[A_consumerFocus]
           ,[A_competitiveness]
           ,[A_projectdrag]
           ,[A_overorder]
           ,[A_conclusion]
           ,[A_businessName]
           ,[A_businessID]
           ,[A_customerName]
           ,[A_customerID]
           ,[A_documentaryID]
           ,[UserID]
           ,[A_remark1]
           ,[A_remark2])
     VALUES
           ('{0}'
           ,'{1}'
           ,{2}
           ,{3}
           ,{4}
           ,{5}
           ,{6}
           ,{7}
           ,{8}
           ,{9}
           ,{10}
           ,{11}
           ,{12}
           ,{13}
           ,{14}
           ,{15}
           ,{16}
           ,{17}
           ,{18}
           ,'{19}'
           ,'{20}'
           ,'{21}'
           ,'{22}'
           ,'{23}'
           ,'{24}'
           ,'{25}'
           ,'{26}')", cp.ID, cp.A_datetime, cp.A_durationneed, cp.A_constructneed, cp.A_acceptanceneed, cp.A_defaultmakeup, cp.A_aptitudes, cp.A_fundsbackfront, cp.A_projectscale, cp.A_budget, cp.A_businessmass, cp.A_compete, cp.A_consumerkey, cp.A_kickbackneed, cp.A_consumerFocus, cp.A_competitiveness, cp.A_projectdrag, cp.A_overorder, cp.A_conclusion, cp.A_businessName, cp.A_businessID, cp.A_customerName, cp.A_customerID, cp.A_documentaryID, cp.UserID, cp.A_remark1, cp.A_remark2);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }



        //显示所有需求评估
        public static List<CRM_visitLoad> projectassess(int rows, int page, string tblName, string strWhere, string strGetFields, string fldName, int doCount, int OrderType)
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
                cv.ID = item["ID"].ToString();
                cv.CV_businessName = item["B_name"].ToString();
                cv.CV_consumerName = item["C_name"].ToString();
                cv.CV_datetime = DateTime.Parse(item["A_datetime"].ToString());
                cv.Over = item["A_conclusion"].ToString();
                list.Add(cv);
            }
            return list;
        }

        //通过ID查看需求评估详情(在商机内看到的需求评估)
        public static CRM_projectassess projectassessInfor(string ID,int IsBuss, string A_businessID)
        {
            string sql = "projectassessInforPRO";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@ID",ID),
            new SqlParameter("@IsBuss",IsBuss),
            new SqlParameter("@A_businessID",A_businessID)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            CRM_projectassess cp = new CRM_projectassess();
            cp.ID = dt.Rows[0]["ID"].ToString();
            cp.A_datetime = DateTime.Parse(dt.Rows[0]["A_datetime"].ToString());
            cp.A_durationneed = int.Parse(dt.Rows[0]["A_durationneed"].ToString());
            cp.A_constructneed = int.Parse(dt.Rows[0]["A_constructneed"].ToString());
            cp.A_acceptanceneed = int.Parse(dt.Rows[0]["A_acceptanceneed"].ToString());
            cp.A_defaultmakeup = int.Parse(dt.Rows[0]["A_defaultmakeup"].ToString());
            cp.A_aptitudes = int.Parse(dt.Rows[0]["A_aptitudes"].ToString());
            cp.A_fundsbackfront = int.Parse(dt.Rows[0]["A_fundsbackfront"].ToString());
            cp.A_projectscale = int.Parse(dt.Rows[0]["A_projectscale"].ToString());
            cp.A_budget = int.Parse(dt.Rows[0]["A_budget"].ToString());
            cp.A_businessmass =int.Parse(dt.Rows[0]["A_businessmass"].ToString());
            cp.A_compete = int.Parse(dt.Rows[0]["A_compete"].ToString());
            cp.A_consumerkey = int.Parse(dt.Rows[0]["A_consumerkey"].ToString());
            cp.A_kickbackneed = int.Parse(dt.Rows[0]["A_kickbackneed"].ToString());
            cp.A_consumerFocus = int.Parse(dt.Rows[0]["A_consumerFocus"].ToString());
            cp.A_competitiveness = int.Parse(dt.Rows[0]["A_competitiveness"].ToString());

            cp.A_projectdrag = int.Parse(dt.Rows[0]["A_projectdrag"].ToString());
            cp.A_overorder = int.Parse(dt.Rows[0]["A_overorder"].ToString());
            cp.A_conclusion = int.Parse(dt.Rows[0]["A_conclusion"].ToString());
            cp.A_businessName = dt.Rows[0]["A_businessName"].ToString();
            cp.A_businessID = dt.Rows[0]["A_businessID"].ToString();
            cp.A_customerName = dt.Rows[0]["A_customerName"].ToString();
            cp.A_customerID = dt.Rows[0]["A_customerID"].ToString();
            cp.A_documentaryID = dt.Rows[0]["A_documentaryID"].ToString();
            cp.UserID = dt.Rows[0]["UserID"].ToString();
            cp.A_remark1 = dt.Rows[0]["A_remark1"].ToString();
            cp.A_remark2 = dt.Rows[0]["A_remark2"].ToString();
            return cp;
        }

        //修改需求评估
        public static bool Updataprojectassess(CRM_projectassess cp){
        string sql=string.Format(@"UPDATE [dbo].[CRM_projectassess]
   SET [A_durationneed] ={1}
      ,[A_constructneed] = {2}
      ,[A_acceptanceneed] = {3}
      ,[A_defaultmakeup] = {4}
      ,[A_aptitudes] = {5}
      ,[A_fundsbackfront] = {6}
      ,[A_projectscale] = {7}
      ,[A_budget] = {8}
      ,[A_businessmass] = {9}
      ,[A_compete] = {10}
      ,[A_consumerkey] = {11}
      ,[A_kickbackneed] = {12}
      ,[A_consumerFocus] = {13}
      ,[A_competitiveness] = {14}
      ,[A_projectdrag] = {15}
      ,[A_overorder] = {16}
      ,[A_conclusion] ={17}
 WHERE ID='{0}'",cp.ID,cp.A_durationneed,cp.A_constructneed,cp.A_acceptanceneed,cp.A_defaultmakeup,cp.A_aptitudes,cp.A_fundsbackfront,cp.A_projectscale,cp.A_budget,cp.A_businessmass,cp.A_compete,cp.A_consumerkey,cp.A_kickbackneed,cp.A_consumerFocus,cp.A_competitiveness,cp.A_projectdrag,cp.A_overorder,cp.A_conclusion);
        return DbHelperSQL.ExecuteNonQuery(sql,CommandType.Text)>0;
        }





    }
}
