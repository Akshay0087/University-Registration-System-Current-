using RepositoryLibrary.DataAccessLayer;
using System;
using System.Collections.Generic;
using UniversitySystemRegistration.Models;

namespace ServiceLibrary.Services
{
    public class AdminServices : IAdminServices
    {

        private readonly IAdminDAL _adminDAL;
        public AdminServices(IAdminDAL adminDAL)
        {

            _adminDAL = adminDAL;
        }

        public bool SetStudentRegistrationStatus()
        {
            return _adminDAL.SetStudentStatus();
        }

        public Tuple<List<Student>, List<Student>, List<Student>> GetListOfStudentStatus()
        {
            return _adminDAL.GetStudentStatusFromDB();
        }
    }
}