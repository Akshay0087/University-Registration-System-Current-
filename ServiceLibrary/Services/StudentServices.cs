using RepositoryLibrary.DataAccessLayer;
using ServiceLibrary.Services;
using System;
using System.Collections.Generic;
using UniversitySystemRegistration.Models;

namespace UniversitySystemRegistration.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IStudentDAL _studentDAL;
        private readonly IStudentGuardianSubjectValidation _studentValidationBL;
        public StudentServices(IStudentDAL studentDAL, IStudentGuardianSubjectValidation studentValidationBL)
        {
            _studentDAL = studentDAL;
            _studentValidationBL = studentValidationBL;
          
        }
        public int SubjectTotalScoreCalculation(Student student)
        {
            int totalScore = 0;
            Dictionary<char, int> gradePoints = null;
            foreach (GradeScore scoreEnum in Enum.GetValues(typeof(GradeScore)))
            {
                gradePoints.Add(Convert.ToChar(scoreEnum), (int)scoreEnum);
            }

            for (int j = 0; j < student.Subjects.Count; j++)
            {
                int dicVal = 0;
                char scoreChar = student.Subjects[j].SubjectGrade;
                gradePoints.TryGetValue(scoreChar, out dicVal);
                totalScore += dicVal;
            }
            student.TotalScoreOfSubjects = totalScore;
            return totalScore;
        }


        public List<string> GetListOfData(string query)
        {
            return _studentDAL.StudentGradeSubjectList(query);
        }

        public bool SaveStudentSubject(User user)
        {
            return _studentDAL.SetStudentSubject(user);
            /*
            if ((_studentValidationBL.subjectValidation(user) && _studentValidationBL.gradeValidation(user)))
            {
                return _studentDAL.SetStudentSubject(user);
            }
            else
            {
                return false;
            }*/        
        }

        public bool SaveStudentGuardian(User user)
        {
                return _studentDAL.SetStudentGuardian(user); 
        }
        public Tuple<bool,User> GetStudentDataFromDb(User user)
        {
            return _studentDAL.GetStudentData(user);
        }

        public Student StudentInfoForDetailScreen(User user)
        {
            Student stud = new Student();
            stud = user.student;
            return stud;
        }

        
    }
}
