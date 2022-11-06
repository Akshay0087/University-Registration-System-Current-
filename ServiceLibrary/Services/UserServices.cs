using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UniversitySystemRegistration.Models;
using UniversitySystemRegistration.Repository;
using UniversitySystemRegistration.Services;

namespace UniversitySystemRegistration
{
    public class UserServices : IUserServices
    {
        private readonly IUserDAL _userDAL;


        public UserServices(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }
        public bool UserLogin(User userData)
        {
            return _userDAL.LoginCheck(userData);
        }
       public bool insertUserData(User user)
        {

            return _userDAL.insertUserDataInDb(user);
        }
        public User GetUserData(User user)
        {

            return _userDAL.GetUserDataInDb(user);
        }

        public Tuple<bool,bool,Dictionary<string,string>> UserCheck(User user)
        {
            if (_userDAL.IsUserUniqueInDB(user))
            {
                return Tuple.Create(true, IsUserDataValid(user).Item1, IsUserDataValid(user).Item2);
            }
            else {

                Dictionary<string, string> result = new Dictionary<string, string>();
                return Tuple.Create(false, true, result);

                
            }
            
        }

        public Tuple<bool, Dictionary<string, string>> IsUserDataValid(User user)
        {
            var status = true;
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (!IsEmailValid(user))
            {
                status = false;
                result.Add("email", "Email not valid");
            }
            if (!IsFirstNameValid(user))
            {
                status = false;
                result.Add("fname", "Firstname not valid");
            }
            if (!IsLastNameValid(user))
            {
                status = false;
                result.Add("lname", "Lastname not valid");
            }
            if (!IsNationalIdentityNumberValid(user))
            {
                status = false;
                result.Add("NID", "National Identity Number not valid");
            }
            if (!IsAddressValid(user))
            {
                status = false;
                result.Add("address", "Address not valid");
            }

            if (!IsPhoneNumberValid(user))
            {
                status = false;
                result.Add("phone", "Phone Number not valid");
            }
            return Tuple.Create(status, result);
        }

        public bool IsEmailValid(User user)
        {
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (Regex.IsMatch(user.EmailAddress, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsFirstNameValid(User user)
        {
            if (user.Firstname.Length < 2 || user.Firstname.Length > 50)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsLastNameValid(User user)
        {
            if (user.Lastname.Length < 2 || user.Lastname.Length > 50)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsNationalIdentityNumberValid(User user)
        {
            var minLength = 9;
            var maxLength = 16;
            var nidRegexCharacterPattern = @"^([a-zA-Z0-9]*)$";
            if (user.NationalIdentityNumber.Length > minLength || user.NationalIdentityNumber.Length < maxLength)
            {
                if (Regex.IsMatch(user.NationalIdentityNumber, nidRegexCharacterPattern, RegexOptions.IgnoreCase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


        }

        public bool IsAddressValid(User user)
        {
            if (user.ResidentialAddress.Length < 2 || user.ResidentialAddress.Length > 100)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsPhoneNumberValid(User user)
        {
            var phoneNumberPattern = @"^([0-9]*)$";
            
            if (!(user.PhoneNumber.Length.Equals(8) && Regex.IsMatch(user.PhoneNumber, phoneNumberPattern)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}