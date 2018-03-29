using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace DAL
{
    public class DbHelperSQL
    {
        //private static SqlCommand cmd = null;
        //private static SqlDataReader sdr = null;
        public static string connectionString = ConfigurationManager.AppSettings["SqlConnectionString"];
        //public static SqlConnection conn = null;
        //private static DataSet st = null;
        private static SqlConnection GetConn(SqlConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            else if (conn.State == ConnectionState.Broken)
            {
                conn.Close();
                conn.Open();
            }
            return conn;
        }

        /// <summary>  
        ///  执行不带参数的增删改SQL语句或存储过程  
        /// </summary>  
        /// <param name="cmdText">增删改SQL语句或存储过程</param>  
        /// <param name="ct">命令类型</param>  
        /// <returns></returns>  
        public static int ExecuteNonQuery(string cmdText, CommandType ct)
        {
            int res;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(cmdText, GetConn(conn));
                    cmd.CommandType = ct;
                    res = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return res;
        }

        /// <summary>  
        ///  执行带参数的增删改SQL语句或存储过程  
        /// </summary>  
        /// <param name="cmdText">增删改SQL语句或存储过程</param>  
        /// <param name="ct">命令类型</param>  
        /// <returns>int值</returns>  
        public static int ExecuteNonQuery(string cmdText, SqlParameter[] paras, CommandType ct)
        {
            int res;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(cmdText, GetConn(conn));
                    cmd.CommandType = ct;
                    cmd.Parameters.AddRange(paras);
                    res = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return res;
        }

        /// <summary>  
        ///  执行查询SQL语句或存储过程  
        /// </summary>  
        /// <param name="cmdText">查询SQL语句或存储过程</param>  
        /// <param name="ct">命令类型</param>  
        /// <returns>Table值</returns>  
        //public static DataTable ExecuteQuery(string cmdText, CommandType ct)
        //{
        //    try
        //    {
        //        conn = new SqlConnection(connectionString);
        //        DataTable dt = new DataTable();
        //        cmd = new SqlCommand(cmdText, GetConn());
        //        cmd.CommandType = ct;
        //        using (sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        //        {
        //            dt.Load(sdr);
        //        }
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //dataset写法
        public static DataTable ExecuteQuery(string cmdText, CommandType ct)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmdText, GetConn(conn));
                    da.SelectCommand.CommandType = ct;
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }




        //dataset写法
        public static DataTable ExecuteQuery(string cmdText, SqlParameter[] paras, CommandType ct)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmdText, GetConn(conn));
                    da.SelectCommand.CommandType = ct;
                    da.SelectCommand.Parameters.AddRange(paras);
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }






        /// <summary>  
        ///  执行带参数的查询SQL语句或存储过程  
        /// </summary>  
        /// <param name="cmdText">查询SQL语句或存储过程</param>  
        /// <param name="paras">参数集合</param>  
        /// <param name="ct">命令类型</param>  
        /// <returns>Table值</returns>  
        //public static DataTable ExecuteQuery(string cmdText, SqlParameter[] paras, CommandType ct)
        //{
        //    try
        //    {
        //        conn = new SqlConnection(connectionString);
        //        DataTable dt = new DataTable();
        //        cmd = new SqlCommand(cmdText, GetConn());
        //        cmd.CommandType = ct;
        //        cmd.Parameters.AddRange(paras);
        //        using (sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        //        {
        //            dt.Load(sdr);
        //        }
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}


        /// <summary>  
        /// 执行带参数的Scalar查询  
        /// </summary>  
        /// <param name="cmdText">查询SQL语句或存储过程</param>  
        /// <param name="paras">参数集合</param>  
        /// <param name="ct">命令类型</param>  
        /// <returns>一个int型值</returns>  
        public static int ExecuteCheck(string cmdText, SqlParameter[] paras, CommandType ct)
        {
            int result;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(cmdText, GetConn(conn));
                    cmd.CommandType = ct;
                    cmd.Parameters.AddRange(paras);
                    if (cmd.ExecuteScalar() == null)
                    {
                        result = -1;
                    }
                    else
                    {
                        result = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    return result;
                }
                catch (Exception)
                {

                    return -1;
                }
            }
        }
    }
}
