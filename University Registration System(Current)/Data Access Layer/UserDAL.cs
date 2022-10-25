using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_Registration_System_Current_.Business_Logic;
using UniversitySystemRegistration.Data_Access_Layer;
using UniversitySystemRegistration.Models.Entity;

namespace University_Registration_System_Current_.Data_Access_Layer
{
    public interface IUserDAL
    {
        bool LoginCheck(User userData);
        bool UserInfoCheck(User userData);
    }



    public class UserDAL : IUserDAL
    {
        IPasswordHashing PasswordHashing;
        IDatabaseManipulation DatabaseManipulation;
        

        public bool LoginCheck(User userData)
        {
            bool answer = false;// get password of user from email from DAL

            try
            {
                DatabaseManipulation.Open();
                string query = "SELECT Password FROM User where EmailAddress='" + userData.emailAddress + "'";
                var result = DatabaseManipulation.GetInfo(query);
                string dbpassword = (result.Rows[0].ItemArray[0]).ToString();
                answer = dbpassword == userData.passwordHash ? true : false;
                DatabaseManipulation.Close();
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

            try
            {
                DatabaseManipulation.Open();
                string query = "SELECT * FROM User where EmailAddress='" + user.emailAddress + "' OR Phone='" + user.phoneNum + "' OR NID='" + user.nid + "'";
                var result = DatabaseManipulation.GetInfo(query);
                answer = result.Rows.Count > 0 ? true : false;
                DatabaseManipulation.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("EException.Message: {0}", ex.Message);
            }
            return answer;
        }

        public string CheckRole(User userData)
        {
            string role = null;
            try
            {
                string query = "SELECT Role FROM User where EmailAddress='" + userData.emailAddress + "'";
                var result = DatabaseManipulation.GetInfo(query);
                role = (result.Rows[0].ItemArray[0]).ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("EException.Message: {0}", ex.Message);
            }
            return role;
        }





    }
}