using System;
namespace UniversitySystemRegistration.Models
{
    public class User
    {
        public int UserId;
        public Student student =null;
        public string NationalIdentityNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string ResidentialAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { set; get; }
        public UserRoles UserRole;
    }
}
