using System;
using System.Security.Cryptography;
using System.Text;

namespace UniversitySystemRegistration.Services
{
    public class PasswordHashing: IPasswordHashing
    {
            public string Hash(string value)
            {
                return BCrypt.Net.BCrypt.HashPassword(value); 
            }
    }
 }

