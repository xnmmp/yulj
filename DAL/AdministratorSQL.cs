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
    public class AdministratorSQL
    {
        //管理员添加子账号
        public static bool Addson(CRM_User cu)
        {
            string sql = string.Format(@"INSERT INTO [dbo].[CRM_User]
           ([ID]
           ,[T_name]
           ,[T_demartment]
           ,[T_duct]
           ,[T_phone]
           ,[T_jobnumber]
           ,[T_accountnumber]
           ,[T_pwd]
           ,[T_status]
           ,[T_UserID]
           ,[T_UserRole]
           ,[T_role]
           ,[T_Usekind]
           ,[T_remark]
           ,[T_remark1]
           ,[T_Birth]
           ,[T_Sex]
           ,[T_nativeplace]
           ,[T_Entrytime]
           ,[T_Nowhous]
           ,[T_photoUrl]
           ,[T_IDcard])
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
           ,'{9}'
           ,{10}
           ,{11}
           ,{12}
           ,'{13}'
           ,'{14}'
           ,'{15}'
           ,'{16}'
           ,'{17}'
           ,'{18}'
           ,'{19}'
           ,'{20}'
           ,'{21}')", cu.ID, cu.T_name, cu.T_demartment, cu.T_duct, cu.T_phone, cu.T_jobnumber, cu.T_accountnumber, cu.T_pwd, cu.T_status, cu.T_UserID, cu.T_UserRole, cu.T_role, cu.T_Usekind, cu.T_remark, cu.T_remark1, cu.T_Birth, cu.T_Sex, cu.T_nativeplace, cu.T_Entrytime, cu.T_Nowhous, cu.T_photoUrl, cu.T_IDcard);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }
        //注册的时候查询账号不能重复名称
        public static int Isrepetition(string T_accountnumber)
        {
            int dt = 0;
            string sql = "IsrepetitionPRO";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@T_accountnumber",T_accountnumber)
            };
            dt = DbHelperSQL.ExecuteCheck(sql, parm, CommandType.StoredProcedure);
            if (dt<= 0)
            {
                string sql1 = "IsrepetitionsonPRO";
                SqlParameter[] parm1 = new SqlParameter[] { 
            new SqlParameter("@T_accountnumber",T_accountnumber)
            };
                dt = DbHelperSQL.ExecuteCheck(sql1, parm1, CommandType.StoredProcedure);
            }
            if (dt <= 0)
            {
                return 1;   //-------------没有重名的可以注册
            }
            else
            {
                return 2; //-------------有同名不能注册
            }

        }
        //管理员查看自己的子账号
        public static List<CRM_User> SearchUserByUserid(int page, int rows, string tblName, string strWhere, string strGetFields, string fldName, int doCount, int OrderType)
        {
            DataTable dt = CRM_businessSQL.paging(rows, page, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                List<CRM_User> list = new List<CRM_User>();
                foreach (DataRow item in dt.Rows)
                {
                    CRM_User cu = new CRM_User();
                    cu.ID = item["ID"].ToString();
                    cu.T_name = item["T_name"].ToString();
                    cu.T_accountnumber = item["T_accountnumber"].ToString();
                    cu.T_UserID = item["T_UserID"].ToString();
                    cu.T_role = int.Parse(item["T_role"].ToString());
                    //需填写信息
                    cu.T_demartment = item["T_demartment"].ToString();
                    cu.T_duct = item["T_duct"].ToString();
                    cu.T_phone = item["T_phone"].ToString();
                    cu.T_jobnumber = item["T_jobnumber"].ToString();
                    cu.T_Birth = item["T_Birth"].ToString();
                    cu.T_nativeplace = item["T_nativeplace"].ToString();
                    cu.T_Entrytime = item["T_Entrytime"].ToString();
                    cu.T_Nowhous = item["T_Nowhous"].ToString();
                    cu.T_IDcard = item["T_IDcard"].ToString();
                    cu.T_photoUrl = item["T_photoUrl"].ToString();
                    cu.T_remark = item["T_remark"].ToString();
                    cu.T_remark1 = item["T_remark1"].ToString();
                    list.Add(cu);
                }
                return list;
            }
        }
        //管理员修改子账号信息
        public static bool UpdataSoninfor(CRM_User cu)
        {
            string sql = string.Format(@"UPDATE [dbo].[CRM_User]
   SET [T_name] ='{1}'
      ,[T_demartment] = '{2}'
      ,[T_duct] = '{3}'
      ,[T_phone] = '{4}'
      ,[T_jobnumber] = '{5}'
      ,[T_pwd] = '{6}'
      ,[T_Birth] = '{7}'
      ,[T_Sex] = '{8}'
      ,[T_nativeplace] = '{9}'
      ,[T_Entrytime] = '{10}'
      ,[T_Nowhous] = '{11}'
      ,[T_photoUrl] = '{12}'
      ,[T_IDcard] = '{13}'
      ,[T_role] = {14}
      ,[T_remark] = '{15}'
      ,[T_remark1] = '{16}'
 WHERE ID='{0}'", cu.ID, cu.T_name, cu.T_demartment, cu.T_duct, cu.T_phone, cu.T_jobnumber,cu.T_pwd, cu.T_Birth, cu.T_Sex, cu.T_nativeplace, cu.T_Entrytime, cu.T_Nowhous, cu.T_photoUrl, cu.T_IDcard, cu.T_role, cu.T_remark, cu.T_remark1);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;

        }

    }
}
