using System.Collections.Generic;

namespace UniversitySystemRegistration.Models
{
    public class Student
    {
        public Guardian StudentGuardienInfo=null;
        public List<Subject> Subjects;
        public int TotalScoreOfSubjects;
        public int StudentId;
        public char StudentStatus;
    }
}
