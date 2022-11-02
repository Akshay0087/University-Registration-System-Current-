using System.Collections.Generic;

namespace UniversitySystemRegistration.Models
{
    public class Student
    {
        public Student() { 
            Subjects = new List<Subject>(); 
        }
        public Guardian StudentGuardianInfo;
        public List<Subject> Subjects;
        public int TotalScoreOfSubjects;
        public int StudentId;
        public char StudentStatus;
    }
}
