using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Helpers;
using University_Registration_System_Current_.Data_Access_Layer;

namespace University_Registration_System_Current_.Business_Logic
{
    public class CommonHelper
    {
        /*
        IDatabaseManipulation databaseManipulation;

        public bool GenerateID()
        {
            bool answer = false;// get password of user from email from DAL

            try
            {
                string query = "SELECT * FROM User,Guardian,Student,Subject,SubjectResult where ";
                var result = databaseManipulation.GetInfo(query);
                answer = result.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("EException.Message: {0}", ex.Message);
            }
            return answer;
        
        }

        */
    }
}