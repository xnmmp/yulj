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
    public class CustomerSQL
    {
        //通过ID查询客户名称
        public static CRM_Customer seachNamerbyID(string ID)
        {
            string sql = "seachCUSbyIDPRO";
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
                CRM_Customer cc = new CRM_Customer();
                cc.C_name = dt.Rows[0]["C_name"].ToString();
                cc.C_address = dt.Rows[0]["C_address"].ToString();
                cc.C_linkname = dt.Rows[0]["C_linkname"].ToString();
                cc.C_linkphone = dt.Rows[0]["C_linkphone"].ToString();
                return cc;
            }
        }
        //通过名称模糊查询
        public static List<CRM_Customer> SearchByname(string C_name, string C_UserID)
        {
            string sql = "SearchBynamePRO";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@C_name",C_name),
            new SqlParameter("@C_UserID",C_UserID)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            List<CRM_Customer> list = new List<CRM_Customer>();
            foreach (DataRow item in dt.Rows)
            {
                CRM_Customer cc = new CRM_Customer();
                cc.ID = item["ID"].ToString();
                cc.C_name = item["C_name"].ToString();
                cc.C_address = item["C_address"].ToString();
                cc.C_linkname = item["C_linkname"].ToString();
                list.Add(cc);
            }
            return list;
        }
        //客户通过ID查询出所有信息
        public static CRM_Customer SearchCustomerAllByID(string ID)
        {
            string sql = "SearchCustomerAllByIDPRO";
            SqlParameter[] parm = new SqlParameter[] { 
            
            new SqlParameter("@ID",ID)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            CRM_Customer cc = new CRM_Customer();
            provinces pro = new provinces();
            cities cit = new cities();
            areass are = new areass();
            cc.ID = dt.Rows[0]["ID"].ToString();
            cc.C_name = dt.Rows[0]["C_name"].ToString();
            cc.C_address = dt.Rows[0]["C_address"].ToString();
            cc.C_linkname = dt.Rows[0]["C_linkname"].ToString();
            cc.C_linkphone = dt.Rows[0]["C_linkphone"].ToString();
            cc.C_kind = int.Parse(dt.Rows[0]["C_kind"].ToString());
            cc.C_cooperation = int.Parse(dt.Rows[0]["C_cooperation"].ToString());
            cc.C_scale = int.Parse(dt.Rows[0]["C_scale"].ToString());
            cc.C_industry = dt.Rows[0]["C_industry"].ToString();
            cc.C_UserID = dt.Rows[0]["C_UserID"].ToString();
            cc.C_remark1 = dt.Rows[0]["C_remark1"].ToString();
            cc.C_remark2 = dt.Rows[0]["C_remark2"].ToString();

            //
            cc.C_province = dt.Rows[0]["C_province"].ToString();
            pro = Service_kindSQL.proname(cc.C_province);
            cc.C_city = dt.Rows[0]["C_city"].ToString();
            cit = Service_kindSQL.citname(cc.C_city);
            cc.C_district = dt.Rows[0]["C_district"].ToString();
            are = Service_kindSQL.arename(cc.C_district);
            var city = new city();
            city.provinces = pro;
            city.cities = cit;
            city.areass = are;
            cc.city = city;
            return cc;
        }



        //添加客户(必须插入管理员ID)
        public static bool Add_Customer(CRM_Customer cc)
        {
            string sql = string.Format(@"INSERT INTO [dbo].[CRM_Customer]
           ([ID]
           ,[C_name]
           ,[C_address]
           ,[C_linkname]
           ,[C_linkphone]
           ,[C_province]
           ,[C_city]
           ,[C_district]
           ,[C_kind]
           ,[C_cooperation]
           ,[C_scale]
           ,[C_industry]
           ,[C_UserID]
           ,[C_remark1]
           ,[C_remark2])
     VALUES
           ('{0}'
           ,'{1}'
           ,'{2}'
           ,'{3}'
           ,'{4}'
           ,'{5}'
           ,'{6}'
           ,'{7}'
           ,{8}
           ,{9}
           ,{10}
           ,'{11}'
           ,'{12}'
           ,'{13}'
           ,'{14}')", cc.ID, cc.C_name, cc.C_address, cc.C_linkname, cc.C_linkphone, cc.C_province, cc.C_city, cc.C_district, cc.C_kind, cc.C_cooperation, cc.C_scale, cc.C_industry, cc.C_UserID, cc.C_remark1, cc.C_remark2);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }
        //修改客户
        public static bool UpdataCustomer(CRM_Customer cc)
        {
            string sql = string.Format(@"UPDATE [dbo].[CRM_Customer]
   SET [C_name] = '{1}'
      ,[C_address] = '{2}'
      ,[C_linkname] = '{3}'
      ,[C_linkphone] = '{4}'
      ,[C_province] = '{5}'
      ,[C_city] = '{6}'
      ,[C_district] ='{7}'
      ,[C_kind] ={8}
      ,[C_cooperation] = {9}
      ,[C_scale] = {10}
      ,[C_industry] = '{11}'
      ,[C_remark1] = '{12}'
      ,[C_remark2] = '{13}'
 WHERE ID='{0}'", cc.ID, cc.C_name, cc.C_address, cc.C_linkname, cc.C_linkphone, cc.C_province, cc.C_city, cc.C_district, cc.C_kind, cc.C_cooperation, cc.C_scale, cc.C_industry, cc.C_remark1, cc.C_remark2);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }





        //查看客户
        public static List<CRM_Customer> loadCustomer(int rows, int page, string tblName, string strWhere, string strGetFields, string fldName, int doCount, int OrderType)
        {
            DataTable dt = CRM_businessSQL.paging(rows, page, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            List<CRM_Customer> list = new List<CRM_Customer>();
            foreach (DataRow item in dt.Rows)
            {
                CRM_Customer cc = new CRM_Customer();
                cc.ID = item["ID"].ToString();
                cc.C_name = item["C_name"].ToString();
                cc.C_linkphone = item["C_linkphone"].ToString();
                cc.C_linkname = item["C_linkname"].ToString();
                cc.C_cooperation = int.Parse(item["C_cooperation"].ToString());
                list.Add(cc);
            }
            return list;
        }

    }
}
