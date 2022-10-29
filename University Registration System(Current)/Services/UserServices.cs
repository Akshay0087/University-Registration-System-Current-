using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UniversitySystemRegistration.Models;
using UniversitySystemRegistration.Repository;
using UniversitySystemRegistration.Services;

namespace UniversitySystemRegistration
{
    public class UserServices : IUserServices
    {
        private readonly IPasswordHashing _passwordHashing;
        private readonly IDatabaseConnection _databaseManipulation;
        private readonly IUserDAL _userDAL;

        public const string getUserDataQuery = "SELECT UserId,FirstName,LastName,DoB,NID,Phone,Role FROM Users where EmailAddress=@emailAddress";
        public const string getSelectedStudentsQuery = "Select FirstName+' '+LastName AS Name,EmailAddress as Email_Address,TotalPoint as Total_Point from Users U " +
            "inner join Student s on U.UserId=s.UserId " +
            "where s.Student_Status='A'";
        public const string insertIntoDB = "INSERT INTO Users (EmailAddress,NID,FirstName,LastName,UserAddress,Phone,DoB,PasswordHash,Role)Values" +
            "(@emailAddress,@nid,@firstname,@lastname,@address,@phone,@dob,@passwordhash,@role)";

        public UserServices(IPasswordHashing passwordHashing, IDatabaseConnection databaseManipulation, IUserDAL userDAL)
        {
            _passwordHashing = passwordHashing;
            _databaseManipulation = databaseManipulation;
            _userDAL = userDAL;
        }
        public bool UserLogin(User userData)
        {
            userData.PasswordHash = _passwordHashing.Hash(userData.PasswordHash);
            return _userDAL.LoginCheck(userData);
        }
        public bool insertUserData(User user)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@emailAddress", user.EmailAddress));
            parameters.Add(new SqlParameter("@address", user.ResidentialAddress));
            parameters.Add(new SqlParameter("@nid", user.NationalIdentityNumber));
            parameters.Add(new SqlParameter("@firstname", user.Firstname));
            parameters.Add(new SqlParameter("@lastname", user.Lastname));
            parameters.Add(new SqlParameter("@phone", user.PhoneNumber));
            parameters.Add(new SqlParameter("@dob", user.DateOfBirth));
            parameters.Add(new SqlParameter("@passwordhash", _passwordHashing.Hash(user.PasswordHash)));
            parameters.Add(new SqlParameter("@role", user.UserRole.ToString()));

            bool status = _databaseManipulation.SetInfo(insertIntoDB, parameters);
            return status;
        }
        public User GetUserData(User user)
        {
            User user1 = new User();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@emailAddress", user.EmailAddress));
            var dataTable = _databaseManipulation.GetInfo(getUserDataQuery, parameters);
            user1.UserId = Convert.ToInt32(dataTable.Rows[0]["UserId"]);
            user1.EmailAddress = user.EmailAddress;
            user1.Firstname = dataTable.Rows[0]["FirstName"].ToString();
            user1.Lastname = dataTable.Rows[0]["LastName"].ToString();
            user1.DateOfBirth = Convert.ToDateTime(dataTable.Rows[0]["DoB"]);
            user1.NationalIdentityNumber = dataTable.Rows[0]["NID"].ToString();
            user1.PhoneNumber = dataTable.Rows[0]["Phone"].ToString();
            Enum.TryParse(dataTable.Rows[0]["Role"].ToString(), out UserRoles roleUser);
            user1.UserRole = roleUser;
            return user1;
        }
        public bool UserCheck(User user)
        {
            return _userDAL.UserInfoCheck(user);
        }

        public string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table>";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }
        public string UserDelete(User user)
        {
            int status = 1; //pass email to DAL to check existant
            if (status == 1)
            {
                //pass arg to DAL to delete user in db
                return "Sucessful Deletion";
            }
            else
            {
                return "Unsucessful Deletion";
            }
        }

        public DataTable GetSelectedStudents()
        {
            DataTable dataTable;
            return dataTable = _databaseManipulation.GetInfo(getSelectedStudentsQuery, null);

        }
    }
}