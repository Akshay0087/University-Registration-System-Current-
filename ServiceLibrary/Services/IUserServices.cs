using System;
using System.Collections.Generic;
using System.Data;
using UniversitySystemRegistration.Models;

namespace UniversitySystemRegistration.Services
{
    public interface IUserServices
    {
        bool UserLogin(User userData);
        bool insertUserData(User user);
        User GetUserData(User user);
        Tuple<bool, bool, Dictionary<string, string>> UserCheck(User user);
    
    }
}