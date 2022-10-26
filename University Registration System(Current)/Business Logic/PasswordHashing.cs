using System;
using System.Security.Cryptography;
using System.Text;

namespace UniversitySystemRegistration.Business_Logic
{

    public interface IPasswordHashing
    {
        string Hash(string value);
    }
    public class PasswordHashing: IPasswordHashing
    {
            public string Hash(string value)
            {
                byte[] bytePassword= Encoding.UTF8.GetBytes(value);
                byte[] computedHash = SHA256.Create().ComputeHash(bytePassword);
                string toStringConversion= Convert.ToBase64String(computedHash);

            return toStringConversion; 
            }

    }
 }

