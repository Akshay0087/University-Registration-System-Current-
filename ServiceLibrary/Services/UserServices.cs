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
        private readonly IUserDAL _userDAL;

        /*public const string insertintodb = "insert into users (emailaddress,nid,firstname,lastname,useraddress,phone,dob,passwordhash,role)values" +
            "(@emailaddress,@nid,@firstname,@lastname,@address,@phone,@dob,@passwordhash,@role)";*/

        public UserServices(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }
        public bool UserLogin(User userData)
        {
            return _userDAL.LoginCheck(userData);
        }
        public bool insertUserData(User user)
        {
            
            return _userDAL.insertUserDataInDb(user);
        }
        public User GetUserData(User user)
        {
            
            return _userDAL.GetUserDataInDb(user);
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