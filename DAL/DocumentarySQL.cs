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
    public class DocumentarySQL
    {
        //通过账号查询出用户信息
        public static CRM_User searchCRMUserInfor(string T_accountnumber)
        {
            string sql = "SearchCrmuserPRO";
            SqlParameter[] parm = new SqlParameter[]{
        new SqlParameter("@T_accountnumber",T_accountnumber)
        };
            return LoginSQL.PubCrmUserSearch(sql,parm);
        }
        //员工修改个人信息
        public static bool UpdatauserInfor(CRM_User cu)
        {
            string sql = string.Format(@"UPDATE [dbo].[CRM_User]
   SET [T_name] = '{1}'
      ,[T_demartment] = '{2}'
      ,[T_duct] ='{3}'
      ,[T_phone] ='{4}'
      ,[T_jobnumber] = '{5}'
      ,[T_Birth] = '{6}'
      ,[T_Sex] = '{7}'
      ,[T_nativeplace] = '{8}'
      ,[T_Entrytime] = '{9}'
      ,[T_Nowhous] = '{10}'
      ,[T_photoUrl] = '{11}'
      ,[T_IDcard] = '{12}'
      ,[T_remark] = '{13}'
      ,[T_remark1] = '{14}'
 WHERE T_accountnumber='{0}'", cu.T_accountnumber, cu.T_name, cu.T_demartment, cu.T_duct, cu.T_phone, cu.T_jobnumber, cu.T_Birth, cu.T_Sex, cu.T_nativeplace, cu.T_Entrytime, cu.T_Nowhous, cu.T_photoUrl, cu.T_IDcard, cu.T_remark, cu.T_remark1);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }

        //显示所有状态为1的用户
        public static List<CRM_User_IDandnName> SearchUserBystatusStatus(string UserID)
        {
            string sql = "SearchUserBystatusStatusPRO";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@UserID",UserID)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            List<CRM_User_IDandnName> list = new List<CRM_User_IDandnName>();
            foreach (DataRow item in dt.Rows)
            {
                CRM_User_IDandnName cuin = new CRM_User_IDandnName();
                cuin.ID = item["ID"].ToString();
                cuin.name = item["T_name"].ToString();
                list.Add(cuin);
            }
            return list;
        }
        //通过用户ID查询出用户的名字
        public static string SearchUserNameByID(string ID)
        {
            string sql = "SearchUserNameByIDPRO";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@ID",ID)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            string T_name = dt.Rows[0]["T_name"].ToString();
            return T_name;
        }


        //在用户修改个人密码中，通过账号查找出密码(对比是否一样)
        public static string SearchIsPwdPRO(string T_accountnumber)
        {

            string sql = "[dbo].[SearchIsPwdPRO]";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@T_accountnumber",T_accountnumber)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            string pwd = dt.Rows[0]["T_pwd"].ToString();
            return pwd;
        }

        //修改密码
        public static bool UpdataPWD(string T_accountnumber, string pwd)
        {
            string sql = string.Format(@"UPDATE [dbo].[CRM_User]
   SET [T_pwd] = '{1}' WHERE T_accountnumber='{0}'", T_accountnumber, pwd);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }


    }
}
