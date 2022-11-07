using RepositoryLibrary.DataAccessLayer;
using System.Collections.Generic;
using System;
using System.Linq;
using UniversitySystemRegistration.Models;
using UniversitySystemRegistration.Services;

namespace ServiceLibrary.Services
{
    public class StudentGuardianSubjectValidation : IStudentGuardianSubjectValidation
    {
        private readonly IStudentDAL studentDAL;

        public StudentGuardianSubjectValidation(IStudentDAL _studentDAL)
        {
            studentDAL = _studentDAL;
        }

        public bool subjectValidation(User user)
        {
            

            bool status = true;
            var subjectList = studentDAL.StudentGradeSubjectList(SqlQueries.getSubjectList);
            int count = 0;

            while (status && count < user.student.Subjects.Count)
            {
                status = subjectList.Any(s => s == user.student.Subjects[count].SubjectName);
            }
            return status;

        }

        public bool gradeValidation(User user)
        {
            bool status = true;
            var gradeList = studentDAL.StudentGradeSubjectList(SqlQueries.getGradeList);
            int count = 0;

            while (status && count < user.student.Subjects.Count)
            {
                status = gradeList.Any(s => s.Equals(user.student.Subjects[count].SubjectGrade));
            }
            return status;

        }



    }
}
