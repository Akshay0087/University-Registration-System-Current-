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

        public const string getUserDataQuery = "SELECT ui.NID,u.UserId,ui.FirstName,ui.LastName,ui.DateOfBirth,ui.UserAddress,ui.Phone,u.Role from Users u inner join UsersInfo ui on u.UserId=ui.UserId where u.EmailAddress=@emailAddress";
   
        public const string insertIntoUser = "insert into Users (Emailaddress,Passwordhash,role)values" +
            "(@emailaddress,@passwordhash,@role)";
        public const string selectIndentity = "select UserId from Users where EmailAddress=@emailAddress";
        public const string insertIntoUsersInfo= "insert into UsersInfo (Nid,FirstName,LastName,UserAddress,Phone,DateOfBirth,UserId) values (@nid,@firstname,@lastname,@address,@phone,@dateOfBirth,@userId)";
        /*public const string insertintodb = "insert into users (emailaddress,nid,firstname,lastname,useraddress,phone,dob,passwordhash,role)values" +
            "(@emailaddress,@nid,@firstname,@lastname,@address,@phone,@dob,@passwordhash,@role)";*/

        public UserServices(IPasswordHashing passwordHashing, IDatabaseConnection databaseManipulation, IUserDAL userDAL)
        {
            _passwordHashing = passwordHashing;
            _databaseManipulation = databaseManipulation;
            _userDAL = userDAL;
        }
        public bool UserLogin(User userData)
        {
            return _userDAL.LoginCheck(userData);
        }
        public bool insertUserData(User user)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@emailAddress", user.EmailAddress));
            parameters.Add(new SqlParameter("@passwordhash", _passwordHashing.Hash(user.PasswordHash)));
            parameters.Add(new SqlParameter("@role", user.UserRole.ToString()));
            bool status = _databaseManipulation.SetInfo(insertIntoUser, parameters);
            if (status)
            {
                List<SqlParameter> parametersSecond = new List<SqlParameter>();
                parametersSecond.Add(new SqlParameter("@emailAddress", user.EmailAddress));
                var result = _databaseManipulation.GetInfo(selectIndentity, parametersSecond);
                var identity=Convert.ToInt32(result.Rows[0].ItemArray[0]);
                user.UserId = identity;

                List<SqlParameter> parametersThird = new List<SqlParameter>();
                parametersThird.Add(new SqlParameter("@address", user.ResidentialAddress));
                parametersThird.Add(new SqlParameter("@nid", user.NationalIdentityNumber));
                parametersThird.Add(new SqlParameter("@firstname", user.Firstname));
                parametersThird.Add(new SqlParameter("@lastname", user.Lastname));
                parametersThird.Add(new SqlParameter("@phone", user.PhoneNumber));
                parametersThird.Add(new SqlParameter("@dateOfBirth", user.DateOfBirth));
                parametersThird.Add(new SqlParameter("@userId", user.UserId));
                status = _databaseManipulation.SetInfo(insertIntoUsersInfo, parametersThird);
            }
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
            user1.ResidentialAddress = dataTable.Rows[0]["UserAddress"].ToString();
            user1.DateOfBirth = Convert.ToDateTime(dataTable.Rows[0]["DateOfBirth"]);
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

    }
}