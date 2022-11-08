using System;
using System.Text.RegularExpressions;
using UniversitySystemRegistration.Models;

namespace ServiceLibrary.Services
{
    public class RegisterValidation : IRegisterValidations
    {

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

            if (!((user.PhoneNumber.Length.Equals(8) || user.PhoneNumber.Length.Equals(7)) && Regex.IsMatch(user.PhoneNumber, phoneNumberPattern)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool AreFieldsEmpty(User user)
        {
            if (String.IsNullOrEmpty(user.EmailAddress)|| String.IsNullOrEmpty(user.Firstname)|| String.IsNullOrEmpty(user.Lastname)|| String.IsNullOrEmpty(user.DateOfBirth.ToString())|| String.IsNullOrEmpty(user.NationalIdentityNumber)|| String.IsNullOrEmpty(user.PhoneNumber)|| String.IsNullOrEmpty(user.ResidentialAddress))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}