using UniversitySystemRegistration.Models;

namespace ServiceLibrary.Services
{
    public interface IRegisterValidations
    {
        bool IsAddressValid(User user);
        bool IsEmailValid(User user);
        bool IsFirstNameValid(User user);
        bool IsLastNameValid(User user);
        bool IsNationalIdentityNumberValid(User user);
        bool IsPhoneNumberValid(User user);
    }
}
