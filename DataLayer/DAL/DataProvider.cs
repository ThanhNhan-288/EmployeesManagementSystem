using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer.DAL
{
    public class DataProvider
    {
        string cnStr;
        SqlConnection cn;
        public SqlConnection Connection
        {
            get { return cn; }
        }

        public DataProvider()
        {
            cnStr = ConfigurationManager.ConnectionStrings["cnStr"].ConnectionString;
            cn = new SqlConnection(cnStr);
        }

        public void Connect()
        {
            if (cn.State != ConnectionState.Open)
                cn.Open();
        }

        public void Disconnect()
        {
            if (cn.State != ConnectionState.Closed)
                cn.Close();
        }

        public DataTable ExecuteQuery(string spName, CommandType type, SqlParameter[] pars = null)
        {
            Connect();
            SqlCommand cmd = new SqlCommand(spName, cn)
            {
                CommandType = type
            };
            if (pars != null)
                cmd.Parameters.AddRange(pars);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Disconnect();
            return dt;
        }

        public int ExecuteNonQuery(string spName, CommandType type, SqlParameter[] pars = null)
        {
            Connect();
            SqlCommand cmd = new SqlCommand(spName, cn)
            {
                CommandType = type
            };
            if (pars != null)
                cmd.Parameters.AddRange(pars);
            int result = cmd.ExecuteNonQuery();
            Disconnect();
            return result;
        }
        public int IExecuteNonQuery(string sql, CommandType type, List<SqlParameter> paras)
        {
            Connect();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandType = type;

                if (paras != null)
                {
                    foreach (SqlParameter para in paras)
                        cmd.Parameters.Add(para);
                }

                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }


        public object ExecuteScalar(string spName, CommandType type, SqlParameter[] pars = null)
        {
            Connect();
            SqlCommand cmd = new SqlCommand(spName, cn)
            {
                CommandType = type
            };
            if (pars != null)
                cmd.Parameters.AddRange(pars);
            object result = cmd.ExecuteScalar();
            Disconnect();
            return result;
        }
        public DataTable ExecuteAdapter(string sql, List<SqlParameter> parameters)
        {
            Connect();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                        cmd.Parameters.Add(param);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }
    }
}
