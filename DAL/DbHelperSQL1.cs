﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    public class DbHelperSQL1
    {

        private static SqlCommand cmd = null;
        private static SqlDataReader sdr = null;
        public static string connectionString = ConfigurationManager.AppSettings["SqlConnectionString"];
        private static SqlConnection conn = new SqlConnection(connectionString);
        private static SqlConnection GetConn()
        {
            if (conn.State == ConnectionState.Closed)
            {
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
            try
            {
                cmd = new SqlCommand(cmdText, GetConn());
                cmd.CommandType = ct;
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
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
            try
            {
                cmd = new SqlCommand(cmdText, GetConn());
                cmd.CommandType = ct;
                cmd.Parameters.AddRange(paras);
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
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
        public static DataTable ExecuteQuery(string cmdText, CommandType ct)
        {
            try
            {
                DataTable dt = new DataTable();
                cmd = new SqlCommand(cmdText, GetConn());
                cmd.CommandType = ct;
                using (sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    dt.Load(sdr);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
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
        public static DataTable ExecuteQuery(string cmdText, SqlParameter[] paras, CommandType ct)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand(cmdText, GetConn());
            cmd.CommandType = ct;
            cmd.Parameters.AddRange(paras);
            using (sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                dt.Load(sdr);
            }
            return dt;
        }


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
            using (cmd = new SqlCommand(cmdText, GetConn()))
            {
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

            }
            return result;
        }
    }
}
