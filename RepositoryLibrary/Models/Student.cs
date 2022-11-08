using System.Collections.Generic;

namespace UniversitySystemRegistration.Models
{
    public class Student
    {
        public Student() { 
            Subjects = new List<Subject>(); 
        }
        public Guardian StudentGuardianInfo { get; set; }
        public List<Subject> Subjects { get; set; }
        public int TotalScoreOfSubjects { get; set; }
        public int StudentId { get; set; }
        public string StudentStatus { get; set; }
        public int TotalSubjectPoints { get; set; }
        public string StudentFullName { get; set; }
    }
}
