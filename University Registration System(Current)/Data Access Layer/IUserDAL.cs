using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UniversitySystemRegistration.Business_Logic;
using UniversitySystemRegistration.Data_Access_Layer;
using UniversitySystemRegistration.Models;

namespace University_Registration_System_Current_.Data_Access_Layer
{
    public interface IUserDAL
    {
        bool LoginCheck(User userData);
        bool UserInfoCheck(User userData);
    }

    public class UserDAL : IUserDAL
    {
        IPasswordHashing _PasswordHashing;
        IDatabaseManipulation _DatabaseManipulation;

        const string loginCheckQuery = "SELECT PasswordHash FROM Users where EmailAddress=@emailAddress";
        const string infoCheckQuery = "SELECT UserId FROM Users where EmailAddress=@emailAddress OR Phone=@phoneNum OR NID=@nid";

        public UserDAL(IDatabaseManipulation DatabaseManipulation,IPasswordHashing PasswordHashing) {
            _PasswordHashing = PasswordHashing;
            _DatabaseManipulation = DatabaseManipulation;
        }


        public bool LoginCheck(User userData)
        {
            bool answer = false;// get password of user from email from DAL
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@emailAddress", userData.emailAddress));


            try
            {
                _DatabaseManipulation.Open();
                
                var result = _DatabaseManipulation.GetInfo(loginCheckQuery,parameters);
                string dbpassword = (result.Rows[0].ItemArray[0]).ToString();
                answer = dbpassword == userData.passwordHash ? true : false;
                _DatabaseManipulation.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("EException.Message: {0}", ex.Message);
            }
            return answer;
        }


        public bool UserInfoCheck(User user)
        {
            bool answer = false;
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@emailAddress", user.emailAddress));
            parameters.Add(new SqlParameter("@phoneNum", user.phoneNum));
            parameters.Add(new SqlParameter("@nid", user.nid));
            try
            {
                _DatabaseManipulation.Open();
                var result = _DatabaseManipulation.GetInfo(infoCheckQuery,parameters);
                answer = result.Rows.Count > 0 ? true : false;
                _DatabaseManipulation.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("EException.Message: {0}", ex.Message);
            }
            return answer;
        }


        /*public string checkrole(user userdata)
        {
            string role = null;

            list<sqlparameter> parameters = new list<sqlparameter>();

            parameters.add(new sqlparameter("@emailaddress", userdata.emailaddress));
            try
            {
                string query = "select role from user where emailaddress='" + userdata.emailaddress + "'";
                var result = _databasemanipulation.getinfo(query);
                role = (result.rows[0].itemarray[0]).tostring();
            }
            catch (exception ex)
            {
                console.writeline("eexception.message: {0}", ex.message);
            }
            return role;
        }*/

        





    }
}