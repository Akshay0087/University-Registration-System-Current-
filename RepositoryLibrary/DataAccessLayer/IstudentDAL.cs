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

        bool SetStudentSubject(User user);

        bool SetStudentGuardian(User user);

        List<String> StudentGradeList(string query);

        Tuple<bool, User> GetStudentData(User user);

    }
}