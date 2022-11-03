using RepositoryLibrary.DataAccessLayer;
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
      

        public bool LoginCheck(User userData)
        {
            bool answer = false; // get password of user from email from DAL
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@emailAddress", userData.EmailAddress));
            try
            {
                var result = databaseManipulation.GetInfo(SqlQueries.loginCheckQuery, parameters);
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
                var result = databaseManipulation.GetInfo(SqlQueries.infoCheckQuery, parameters);
                answer = result.Rows.Count > 0 ? true : false;
            }
            catch (Exception error)
            {
                throw error;
            }
            return answer;
        }

        public bool insertUserDataInDb(User user)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@emailAddress", user.EmailAddress));
            parameters.Add(new SqlParameter("@passwordhash", BCrypt.Net.BCrypt.HashPassword(user.PasswordHash)));
            parameters.Add(new SqlParameter("@role", user.UserRole.ToString()));
            bool status = databaseManipulation.SetInfo(SqlQueries.insertIntoUser, parameters);
            if (status)
            {
                List<SqlParameter> parametersSecond = new List<SqlParameter>();
                parametersSecond.Add(new SqlParameter("@emailAddress", user.EmailAddress));
                var result = databaseManipulation.GetInfo(SqlQueries.selectIndentity, parametersSecond);
                var identity = Convert.ToInt32(result.Rows[0].ItemArray[0]);
                user.UserId = identity;

                List<SqlParameter> parametersThird = new List<SqlParameter>();
                parametersThird.Add(new SqlParameter("@address", user.ResidentialAddress));
                parametersThird.Add(new SqlParameter("@nid", user.NationalIdentityNumber));
                parametersThird.Add(new SqlParameter("@firstname", user.Firstname));
                parametersThird.Add(new SqlParameter("@lastname", user.Lastname));
                parametersThird.Add(new SqlParameter("@phone", user.PhoneNumber));
                parametersThird.Add(new SqlParameter("@dateOfBirth", user.DateOfBirth));
                parametersThird.Add(new SqlParameter("@userId", user.UserId));
                status = databaseManipulation.SetInfo(SqlQueries.insertIntoUsersInfo, parametersThird);
            }
            return status;
        }

        public User GetUserDataInDb(User user)
        {
            User user1 = new User();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@emailAddress", user.EmailAddress));
            var dataTable = databaseManipulation.GetInfo(SqlQueries.getUserDataQuery, parameters);
            user1.UserId = Convert.ToInt32(dataTable.Rows[0]["UserId"]);
            user1.EmailAddress = user.EmailAddress;
            user1.Firstname = dataTable.Rows[0]["FirstName"].ToString();
            user1.Lastname = dataTable.Rows[0]["LastName"].ToString();
            user1.ResidentialAddress = dataTable.Rows[0]["UserAddress"].ToString();
            user1.DateOfBirth = Convert.ToDateTime(dataTable.Rows[0]["DateOfBirth"]);
            user1.NationalIdentityNumber = dataTable.Rows[0]["NID"].ToString();
            user1.PhoneNumber = dataTable.Rows[0]["Phone"].ToString();
            Enum.TryParse(dataTable.Rows[0]["Role"].ToString(), out UserRoles roleUser);
            user1.UserRole = roleUser;
            return user1;
        }


    }
}