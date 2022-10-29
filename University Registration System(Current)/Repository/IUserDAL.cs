using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UniversitySystemRegistration.Models;

namespace UniversitySystemRegistration.Repository
{
    public interface IUserDAL
    {
        bool LoginCheck(User userData);
        bool UserInfoCheck(User userData);
    }
}