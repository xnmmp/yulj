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
    public class TokenTableSQL
    {
        //添加数据（在用户登录的时候）
        public static bool AddToken(string name, string GUID, string loginTime, string Remark, string Role)
        {
            string sql = string.Format(@"INSERT INTO [dbo].[TokenTable]
           ([UserName]
           ,[GUID]
           ,[loginTime]
           ,[Remark]
           ,[Role])
     VALUES
           ('{0}'
           ,'{1}'
           ,'{2}'
           ,'{3}'
           ,'{4}')", name, GUID, loginTime, Remark, Role);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }

        //修改Token（在用户登录的时候）
        public static bool UpdataToken(string name, string GUID, string loginTime, string Role, string Remark)
        {
            string sql = string.Format(@"UPDATE [dbo].[TokenTable]
   SET [GUID] ='{1}'
      ,[loginTime] = '{2}'
      ,[Role]='{3}'
      ,[Remark] ='{4}' WHERE UserName='{0}'", name, GUID, loginTime, Role, Remark);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }



        //查询
        public static TokenTable SelByname(string name)
        {
            string sql = "store_Token";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@user",name)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            TokenTable tu = null;
            tu = new TokenTable();
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                tu.UserName = dt.Rows[0]["UserName"].ToString();
                tu.GUID = dt.Rows[0]["GUID"].ToString();
                tu.loginTime = dt.Rows[0]["loginTime"].ToString();
                tu.Role = dt.Rows[0]["Role"].ToString();
                return tu;
            }
        }
    }
}
