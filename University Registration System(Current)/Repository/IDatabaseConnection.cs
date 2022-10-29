using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace UniversitySystemRegistration.Repository
{
    public interface IDatabaseConnection
    {
        void OpenDbConnection();
        void CloseDbConnection();
        bool SetInfo(string query, List<SqlParameter> parameters);
        DataTable GetInfo(string query, List<SqlParameter> parameters);

    }
}