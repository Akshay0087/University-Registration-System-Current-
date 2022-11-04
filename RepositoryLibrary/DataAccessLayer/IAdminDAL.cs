using System.Collections.Generic;
using System;
using UniversitySystemRegistration.Models;

namespace RepositoryLibrary.DataAccessLayer
{
    public interface IAdminDAL
    {
        bool SetStudentStatus();
        Tuple<List<Student>, List<Student>, List<Student>>  GetStudentStatusFromDB();
    }
}
