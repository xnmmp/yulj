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
    public class LoginSQL
    {
        //公共查询CrmUser
        public static CRM_User PubCrmUserSearch(string sql, SqlParameter[] parm)
        {
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            CRM_User cu = null;
            cu = new CRM_User();
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                cu.ID = dt.Rows[0]["ID"].ToString();
                cu.T_name = dt.Rows[0]["T_name"].ToString();
                cu.T_accountnumber = dt.Rows[0]["T_accountnumber"].ToString();
                cu.T_pwd = dt.Rows[0]["T_pwd"].ToString();
                cu.T_UserID = dt.Rows[0]["T_UserID"].ToString();
                cu.T_UserRole = int.Parse(dt.Rows[0]["T_UserRole"].ToString());
                cu.T_role = int.Parse(dt.Rows[0]["T_role"].ToString());
                cu.T_Usekind = int.Parse(dt.Rows[0]["T_Usekind"].ToString());
                //需填写信息
                cu.T_demartment = dt.Rows[0]["T_demartment"].ToString();
                cu.T_Sex = dt.Rows[0]["T_Sex"].ToString();
                cu.T_duct = dt.Rows[0]["T_duct"].ToString();
                cu.T_phone = dt.Rows[0]["T_phone"].ToString();
                cu.T_jobnumber = dt.Rows[0]["T_jobnumber"].ToString();
                cu.T_Birth = dt.Rows[0]["T_Birth"].ToString();
                cu.T_nativeplace = dt.Rows[0]["T_nativeplace"].ToString();
                cu.T_Entrytime = dt.Rows[0]["T_Entrytime"].ToString();
                cu.T_Nowhous = dt.Rows[0]["T_Nowhous"].ToString();
                cu.T_IDcard = dt.Rows[0]["T_IDcard"].ToString();
                cu.T_photoUrl = dt.Rows[0]["T_photoUrl"].ToString();
                cu.T_remark = dt.Rows[0]["T_remark"].ToString();
                cu.T_remark1 = dt.Rows[0]["T_remark1"].ToString();
                cu.T_status = int.Parse(dt.Rows[0]["T_status"].ToString());
            }
            return cu;
        }

        //查询CRM用户表是否有登录用户
        public static CRM_User SearchCrmuser(string name)
        {
            string sql = "SearchCrmuserPRO";
            SqlParameter[] parm = new SqlParameter[]{
            new SqlParameter("@T_accountnumber",name)
            };
            return PubCrmUserSearch(sql, parm);
        }
        //通过ID查询CRM用户表
        public static CRM_User SearchCrmByID(string ID)
        {
            //string sql = string.Format(@"select * from CRM_User where ID='{0}' and T_status=1", ID);
            string sql = "SearchCrmByIDPRO";
            SqlParameter[] parm = new SqlParameter[]{
            new SqlParameter("@ID",ID)
            };
            return PubCrmUserSearch(sql, parm);
        }


        //查询T_User表是否有该用户
        public static T_User Searchfirstuser(string name)
        {
            //string sql = string.Format(@"select * from T_User where name='{0}' and status=1", name);
            string sql = "SearchfirstuserPRO";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("name",name)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            T_User tu = new T_User();
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                tu.ID = dt.Rows[0]["ID"].ToString();
                tu.name = dt.Rows[0]["name"].ToString();
                tu.PWD = dt.Rows[0]["PWD"].ToString();
                tu.role = int.Parse(dt.Rows[0]["role"].ToString());
                tu.mobile = dt.Rows[0]["mobile"].ToString();
            }
            return tu;
        }

        //将一级用户表的信息赋值到二级用户表中（标记为管理员）
        public static bool AddCRMforfirst(CRM_User cu)
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
           ,[T_remark1])
     VALUES
           ('{0}'
           ,'{1}'
           ,''
           ,''
           ,'{2}'
           ,'1'
           ,'{3}'
           ,'{4}'
           ,1
           ,'{5}'
           ,1
           ,1
           ,'{6}'
           ,''
           ,'')", cu.ID, cu.T_name, cu.T_phone, cu.T_accountnumber, cu.T_pwd, cu.T_UserID, cu.T_Usekind);

            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }


        //在试用表添加一级用户
        public static bool AddCRMtrial(CRM_trial ct)
        {
            string sql = string.Format(@"INSERT INTO [dbo].[CRM_trial]
           ([ID]
           ,[UserID]
           ,[endTime]
           ,[remark1]
           ,[remark2])
     VALUES
           ('{0}'
           ,'{1}'
           ,'{2}'
           ,''
           ,'')", ct.ID, ct.UserID, ct.endTime);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }
        //在试用表里查询到期时间
        public static CRM_trial Searchtrialend(string UserID)
        {
            string sql = "Searchtrialend";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@UserID",UserID)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            CRM_trial ct = new CRM_trial();
            ct.endTime = DateTime.Parse(dt.Rows[0]["endTime"].ToString());
            return ct;
        }
        //付费表里查询到期时间
        public static CRM_Pay searchPayend(string UserID)
        {

            //string sql = @"select endtime from CRM_Pay where UserID='{0}'" + UserID;
            string sql = "searchPayendPRO";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@UserID",UserID)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            CRM_Pay cp = new CRM_Pay();
            cp.endtime = DateTime.Parse(dt.Rows[0]["endtime"].ToString());
            return cp;
        }


        //登录时判断是否有试用
        public static int SearchCRM_Tryon()
        {
            string sql = "select TryTime  from  CRM_Tryon";
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, CommandType.Text);
            if (dt.Rows.Count <= 0)
            {
                return 0;
            }
            int ss = int.Parse(dt.Rows[0]["TryTime"].ToString());
            if (ss == 0)
            {
                return 0;
            }
            else
            {
                return ss;
            }
        }

    }
}
