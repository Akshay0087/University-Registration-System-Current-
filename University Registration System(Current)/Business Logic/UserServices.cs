using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using University_Registration_System_Current_;
using University_Registration_System_Current_.Data_Access_Layer;
using UniversitySystemRegistration.Data_Access_Layer;
using UniversitySystemRegistration.Models;

namespace UniversitySystemRegistration.Business_Logic
{
    public interface IUserServices
    {
        bool UserLogin(User userData);
        bool insertUserData(User user);
        string UserDelete(User user);
        User GetUserData(User user);
        DataTable GetSelectedStudents();
        string ConvertDataTableToHTML(DataTable dt);
        bool UserCheck(User user);

    }
    public class UserServices : IUserServices
    {
        private readonly IPasswordHashing _passwordHashing;
        private readonly IDatabaseManipulation _databaseManipulation;
        private readonly IUserDAL _userDAL;

        public const string getUserDataQuery = "SELECT UserId,FirstName,LastName,DoB,NID,Phone,Role FROM Users where EmailAddress=@emailAddress";

        public const string getSelectedStudentsQuery = "Select FirstName+' '+LastName AS Name,EmailAddress as Email_Address,TotalPoint as Total_Point from Users U " +
            "inner join Student s on U.UserId=s.UserId " +
            "where s.Student_Status='A'";

        public const string insertIntoDB = "INSERT INTO Users (EmailAddress,NID,FirstName,LastName,UserAddress,Phone,DoB,PasswordHash,Role)Values" +
            "(@emailAddress,@nid,@firstname,@lastname,@address,@phone,@dob,@passwordhash,@role)";


        public UserServices(IPasswordHashing passwordHashing, IDatabaseManipulation databaseManipulation, IUserDAL userDAL)
        {
            _passwordHashing = passwordHashing;
            _databaseManipulation = databaseManipulation;
            _userDAL = userDAL;
        }

        public bool UserLogin(User userData)
        {
            userData.passwordHash = _passwordHashing.Hash(userData.passwordHash);
            return _userDAL.LoginCheck(userData);
        }
        public bool insertUserData(User user)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            //(@emailAddress,@nid,@firstname,@lastname,@address,@phone,@dob,@passwordhash,@role)
            parameters.Add(new SqlParameter("@emailAddress", user.emailAddress));
            parameters.Add(new SqlParameter("@address", user.address));
            parameters.Add(new SqlParameter("@nid", user.nid));
            parameters.Add(new SqlParameter("@firstname", user.firstname));
            parameters.Add(new SqlParameter("@lastname", user.lastname));
            parameters.Add(new SqlParameter("@phone", user.phoneNum));
            parameters.Add(new SqlParameter("@dob", user.dob));
            parameters.Add(new SqlParameter("@passwordhash", _passwordHashing.Hash(user.passwordHash)));
            parameters.Add(new SqlParameter("@role", user.role.ToString()));

            bool status = _databaseManipulation.SetInfo(insertIntoDB, parameters);
            return status;
        }
        public User GetUserData(User user)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@emailAddress", user.emailAddress));

            User user1 = new User();
            _databaseManipulation.Open();
            var dataTable = _databaseManipulation.GetInfo(getUserDataQuery, parameters);
            _databaseManipulation.Close();
            user1.userId = Convert.ToInt32(dataTable.Rows[0]["UserId"]);
            user1.emailAddress = user.emailAddress;
            user1.firstname = dataTable.Rows[0]["FirstName"].ToString();
            user1.lastname = dataTable.Rows[0]["LastName"].ToString();
            user1.dob = Convert.ToDateTime(dataTable.Rows[0]["DoB"]);
            user1.nid = dataTable.Rows[0]["NID"].ToString();
            user1.phoneNum = dataTable.Rows[0]["Phone"].ToString();
            Enum.TryParse(dataTable.Rows[0]["Role"].ToString(), out Role roleUser);
            user1.role = roleUser;
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
                //pass arg to DAL to insert user in db
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