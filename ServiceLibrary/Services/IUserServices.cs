using System.Data;
using UniversitySystemRegistration.Models;

namespace UniversitySystemRegistration.Services
{
    public interface IUserServices
    {
        bool UserLogin(User userData);
        bool insertUserData(User user);
        string UserDelete(User user);
        User GetUserData(User user);
        DataTable GetSelectedStudents();
        string ConvertDataTableToHTML(DataTable dt);
        bool UserCheck(User user);

    }
}