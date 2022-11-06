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
        bool IsPhoneNumberValid(User user);
        bool IsAddressValid(User user);
        bool IsNationalIdentityNumberValid(User user);
        bool IsLastNameValid(User user);
        bool IsFirstNameValid(User user);
        bool IsEmailValid(User user);
        Tuple<bool, Dictionary<string, string>> IsUserDataValid(User user);
    }
}