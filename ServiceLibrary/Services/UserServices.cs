using ServiceLibrary.Services;
using System;
using System.Collections.Generic;
using UniversitySystemRegistration.Models;
using UniversitySystemRegistration.Repository;
using UniversitySystemRegistration.Services;

namespace UniversitySystemRegistration
{
    public class UserServices : IUserServices
    {
        private readonly IUserDAL _userDAL;
        private readonly IRegisterValidations _registerValidations;

        public UserServices(IUserDAL userDAL, IRegisterValidations registerValidations)
        {
            _userDAL = userDAL;
            _registerValidations = registerValidations;
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

        public Tuple<bool, bool, Dictionary<string, string>> UserCheck(User user)
        {
            if (_userDAL.IsUserUniqueInDB(user))
            {
                return Tuple.Create(true, IsUserDataValid(user).Item1, IsUserDataValid(user).Item2);
            }
            else
            {

                Dictionary<string, string> result = new Dictionary<string, string>();
                return Tuple.Create(false, true, result);

            }

        }

        public Tuple<bool, Dictionary<string, string>> IsUserDataValid(User user)
        {
            var status = true;
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (_registerValidations.AreFieldsEmpty(user))
            {
                status = false;
                result.Add("", "Some fields are empty");
            }
            else
            {
                if (!_registerValidations.IsEmailValid(user))
                {
                    status = false;
                    result.Add("email", "Email not valid");
                }
                if (!_registerValidations.IsFirstNameValid(user))
                {
                    status = false;
                    result.Add("fname", "Firstname not valid");
                }
                if (!_registerValidations.IsLastNameValid(user))
                {
                    status = false;
                    result.Add("lname", "Lastname not valid");
                }
                if (!_registerValidations.IsNationalIdentityNumberValid(user))
                {
                    status = false;
                    result.Add("NID", "National Identity Number not valid");
                }
                if (!_registerValidations.IsAddressValid(user))
                {
                    status = false;
                    result.Add("address", "Address not valid");
                }

                if (!_registerValidations.IsPhoneNumberValid(user))
                {
                    status = false;
                    result.Add("phone", "Phone Number not valid");
                }
            }
            return Tuple.Create(status, result);
        }

    }
}