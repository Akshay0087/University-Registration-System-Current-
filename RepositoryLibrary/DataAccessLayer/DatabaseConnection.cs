using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UniversitySystemRegistration.Repository
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public DatabaseConnection() { }
        SqlConnection DbConnection = null;
        public DataTable GetInfo(string query, List<SqlParameter> parameters)
        {
            DataTable tableData = new DataTable();
            OpenDbConnection();
            if (DbConnection != null)
            {
                using (DbConnection)
                {
                    try
                    {

                        SqlCommand cmd = new SqlCommand(query, DbConnection);
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

            return tableData;
        }

        public bool SetInfo(string query, List<SqlParameter> parameters) 
        {

            OpenDbConnection();

            var result = false;
            SqlTransaction transaction;
            transaction = DbConnection.BeginTransaction();

            SqlCommand sqlcom = new SqlCommand(query, DbConnection);
            sqlcom.Transaction = transaction;
            sqlcom.CommandType = CommandType.Text;
            if (parameters != null)
            {
                parameters.ForEach(parameter => {
                    sqlcom.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                });
            }
            try
            {

                int rowAffected = sqlcom.ExecuteNonQuery();
                result = rowAffected > 0 ? true : false;
                transaction.Commit();
            }
            catch (Exception error)
            {
                transaction.Rollback();
                throw error;
            }
            finally
            {
                CloseDbConnection();
            }

            return result;
        }

        public void OpenDbConnection()
        {
            try
            {
                string strcon = @ConfigurationManager.AppSettings["ConnectionString"]; ;
                DbConnection = new SqlConnection(strcon);

                if (DbConnection.State == ConnectionState.Open)
                {
                CloseDbConnection(); }
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