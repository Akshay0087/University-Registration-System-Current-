using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UniversitySystemRegistration.Repository
{
    public class DatabaseConnection:IDatabaseConnection
    {
        public DatabaseConnection() { }
        SqlConnection DbConnection = null;
        public DataTable GetInfo(string query, List<SqlParameter> parameters)//select
        {
            DataTable tableData = new DataTable();
            if (DbConnection != null)
            {
                using (DbConnection)
                {
                    try
                    {
                        OpenDbConnection();
                        SqlCommand cmd = new SqlCommand(query, DbConnection);
                        // create data adapter
                        cmd.CommandType = CommandType.Text;
                        if (parameters != null)
                        {
                            parameters.ForEach(parameter => {
                                cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                            });
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tableData);
                        }

                        CloseDbConnection();
                    }

                    catch (Exception error)
                    {
                        throw error;
                    }
                }
            }
            else
            {
               //
            }

            return tableData;
        }


        public bool SetInfo(string query, List<SqlParameter> parameters)//insert
        {
            OpenDbConnection();

            var result=false;

            SqlCommand sqlcom = new SqlCommand(query, DbConnection);
            sqlcom.CommandType = CommandType.Text;
            if (parameters != null)
            {
                parameters.ForEach(parameter => {
                    sqlcom.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                });
            }
            try
            {
                int rowAffected=sqlcom.ExecuteNonQuery();
                result =rowAffected>0?  true : false;

            }
            catch (Exception error)
            {
                throw error;
            }
            CloseDbConnection();

            return result;
        }

        public void OpenDbConnection()
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                DbConnection = new SqlConnection(strcon);
                DbConnection.Open();
           
            }
            catch (Exception error)
            {
                DbConnection = null;
                throw error;
            }
        }

        public void CloseDbConnection()
        {
            try
            {
                DbConnection.Close();
                DbConnection.Dispose();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}