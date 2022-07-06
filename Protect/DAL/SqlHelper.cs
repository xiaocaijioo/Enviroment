using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
     class SqlHelper
    {
        static string connStr = "Data Source=LAPTOP-63SVR16M;Initial catalog=EnvironmentProtect;Integrated Security=True;Pooling=False";
        #region 添加数据库帮助类
        #region 将sqlconnection写成属性/方法
        public static SqlConnection ConnectDb //属性
        {
            //public static SqlConnection Connection() 方法
            //{
            //    SqlConnection conn = new SqlConnection(connStr);
            //    conn.Open();
            //    return conn;
            //}
            get
            {
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                return conn;
            }
        }

        #endregion

        #region 将ExecuteReader整合成方法
        //public static SqlDataReader ExecuteReader(string sql,SqlConnection conn,SqlParameter[] pars)
        //{
        //    SqlCommand cmd = new SqlCommand(sql, conn);
        //    if(pars!=null)
        //    {
        //        cmd.Parameters.AddRange(pars);
        //    }
        //    SqlDataReader result = cmd.ExecuteReader();

        //    return result;
        //}
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] pars)
        {
            SqlCommand cmd = new SqlCommand(sql, ConnectDb);
            if (pars != null)
            {
                cmd.Parameters.AddRange(pars);
            }
            SqlDataReader result = cmd.ExecuteReader();

            return result;
        }
        public static SqlDataReader ExecuteReader(string sql, CommandType cmdType, params SqlParameter[] pars)
        {
            SqlCommand cmd = new SqlCommand(sql, ConnectDb); //默认情况是Command.Text
            cmd.CommandType = cmdType;    //设置为存储过程
            if (pars != null)
            {
                cmd.Parameters.AddRange(pars);
            }
            SqlDataReader result = cmd.ExecuteReader();

            return result;
        }
        #endregion

        #region ExecuteNonQuery整合为方法
        public static int ExecuteNonQuery(string sql, CommandType cmdType, params SqlParameter[] pars)
        {
            SqlCommand cmd = new SqlCommand(sql, ConnectDb);
            cmd.CommandType = cmdType;
            if (pars != null)
            {
                cmd.Parameters.AddRange(pars);
            }

            int result = cmd.ExecuteNonQuery();
            return result;
        }
        #endregion
        #endregion
    }
}
