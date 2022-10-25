using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Web.DynamicData;
using System.Runtime.Remoting.Messaging;
using University_Registration_System_Current_.Data_Access_Layer;

namespace UniversitySystemRegistration.Data_Access_Layer
{

    public interface IDatabaseManipulation
    {
        void Open();
        void Close();
        bool SetInfo(string query,List<SqlParameter> parameters);//insert
        DataTable GetInfo(string query, List<SqlParameter> parameters);//select

    }
    public class DatabaseManipulation:IDatabaseManipulation
    {
        public DatabaseManipulation() { }

        SqlConnection cnn = null;

        public void Open()
        {
            try
            {
                string connetionString;
                connetionString = "Data Source=L-PW02X091;Initial Catalog=UniversityRegsitrationSystem;Integrated Security=True";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
            }
            catch
            {
                cnn = null;
            }
        }

        public void Close()
        {
            try
            {
                cnn.Close();
            }
            catch
            {
            }
        }

        public DataTable GetInfo(string query, List<SqlParameter> parameters)//select
        {
            DataTable tableData = new DataTable();
            if (cnn != null)
            {
                using (cnn)
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(query, cnn);
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

                       
                        Close();
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine("EException.Message: {0}", ex.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed: DbConnection is null.");
            }

            return tableData;
        }


        public bool SetInfo(string query, List<SqlParameter> parameters)//insert
        {
            Open();

            var result=false;

            SqlCommand sqlcom = new SqlCommand(query, cnn);
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
            catch (Exception ex)
            {
                Console.WriteLine("EException.Message: {0}", ex.Message);
            }
            Close();

            return result;
        }

        
    }
}