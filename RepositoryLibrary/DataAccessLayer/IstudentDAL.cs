using System;
using System.Collections.Generic;
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

        List<String> StudentGradeList(string query);

    }
}