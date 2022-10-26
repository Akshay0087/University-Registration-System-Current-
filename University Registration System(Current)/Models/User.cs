using System;
using University_Registration_System_Current_;

namespace UniversitySystemRegistration.Models
{
    public class User
    {
        public Student student =null;
        public string nid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address { get; set; }
        public string phoneNum { get; set; }
        public DateTime dob { get; set; }
        public string emailAddress { get; set; }
        public string passwordHash { set; get; }
        public Role role;

        public int userId;

    }
}
