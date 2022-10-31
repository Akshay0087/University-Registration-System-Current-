using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UniversitySystemRegistration.Models;

namespace UniversitySystemRegistration.Repository
{
    public class UserDAL : IUserDAL
    {
        private readonly IDatabaseConnection databaseManipulation;
        public UserDAL(IDatabaseConnection DatabaseManipulation)
        {
            this.databaseManipulation = DatabaseManipulation;
        }
        private
        const string loginCheckQuery = "SELECT PasswordHash FROM Users where EmailAddress=@emailAddress";
        private
        const string infoCheckQuery = "SELECT u.UserId FROM Users u inner join UsersInfo ui on u.UserId=ui.UserId where u.EmailAddress=@emailAddress OR ui.Phone=@phoneNum OR ui.NID=@nid";

        public bool LoginCheck(User userData)
        {
            bool answer = false; // get password of user from email from DAL
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@emailAddress", userData.EmailAddress));
            try
            {
                var result = databaseManipulation.GetInfo(loginCheckQuery, parameters);
                string dbpassword = (result.Rows[0].ItemArray[0]).ToString();
                answer = dbpassword.Equals(userData.PasswordHash) ? true : false;
                answer = BCrypt.Net.BCrypt.Verify(userData.PasswordHash,dbpassword);
            }
            catch (Exception error)
            {
                throw error;
            }
            return answer;
        }

        public bool UserInfoCheck(User user)
        {
            bool answer = false;
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@emailAddress", user.EmailAddress));
            parameters.Add(new SqlParameter("@phoneNum", user.PhoneNumber));
            parameters.Add(new SqlParameter("@nid", user.NationalIdentityNumber));
            try
            {
                var result = databaseManipulation.GetInfo(infoCheckQuery, parameters);
                answer = result.Rows.Count > 0 ? true : false;
            }
            catch (Exception error)
            {
                throw error;
            }
            return answer;
        }
    }
}