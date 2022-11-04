using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversitySystemRegistration.Models;

namespace ServiceLibrary.Services
{
    public interface IAdminServices
    {
        bool SetStudentRegistrationStatus();

        Tuple<List<Student>, List<Student>, List<Student>> GetListOfStudentStatus();
        
        }
}
