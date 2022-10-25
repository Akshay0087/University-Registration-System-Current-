using System.Collections.Generic;

namespace UniversitySystemRegistration.Models.Entity
{
    public class Student
    {
        public Guardian guardianStudent=null;
        public List<Subject> subjects;
        public int TotalScore;
        public int studentId;
        public int userId;
    }
}
