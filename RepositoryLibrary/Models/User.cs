using System;
using System.ComponentModel.DataAnnotations;

namespace UniversitySystemRegistration.Models
{
    public class User
    {
        public int UserId;
        public Student student = null;

        [Required]
        [MaxLength(16), MinLength(9)]
        public string NationalIdentityNumber
        {
            get;
            set;
        }
        [Required]
        [MaxLength(50), MinLength(2)]
        public string Firstname
        {
            get;
            set;
        }
        [Required]
        [MaxLength(50), MinLength(2)]
        public string Lastname
        {
            get;
            set;
        }
        [Required]
        [MaxLength(100), MinLength(2)]
        public string ResidentialAddress
        {
            get;
            set;
        }
        [Required]
        [RegularExpression("/^[+ 0-9]*$/")]
        [MaxLength(20), MinLength(4)]
        public string PhoneNumber
        {
            get;
            set;
        }
        [Required]
        public DateTime DateOfBirth
        {
            get;
            set;
        }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Please enter Email ID")]
        [MaxLength(50), MinLength(8)]
        [RegularExpression("/^\\w+([\\.-]?\\w+)*@\\w+([\\.-]?\\w+)*(\\.\\w{2,3})+$/", ErrorMessage = "E-mail is invalid")]
        public string EmailAddress
        {
            get;
            set;
        }
        [Required]
        [MaxLength(25), MinLength(8)]
        public string PasswordHash
        {
            set;
            get;
        }
        public UserRoles UserRole;
    }
}