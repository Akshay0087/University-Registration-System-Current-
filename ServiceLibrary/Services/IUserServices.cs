using System.Data;
using UniversitySystemRegistration.Models;

namespace UniversitySystemRegistration.Services
{
    public interface IUserServices
    {
        bool UserLogin(User userData);
        bool insertUserData(User user);
        User GetUserData(User user);
        string ConvertDataTableToHTML(DataTable dt);
        bool UserCheck(User user);

    }
}