using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using UniversitySystemRegistration.Models;

namespace RepositoryLibrary.DataAccessLayer
{
    public interface IStudentDAL
    {
        bool SetUserID(User user);

        User GetStudentIDInfo(User user);

        User GetStudentGuardianInfo(User user);

        User GetStudentSubject(User user);

        bool SetUpdateStudentSubject(User user, DbOperation dbOperation);

        bool SetUpdateStudentGuardian(User user, DbOperation dbOperation);

        List<String> GetSubjectList();

    }
}