using RepositoryLibrary.DataAccessLayer;
using System;
using System.Linq;
using UniversitySystemRegistration.Models;

namespace ServiceLibrary.Services
{
    public class StudentGradeSubjectValidation : IStudentGradeSubjectValidation
    {
        private readonly IStudentDAL studentDAL;

        public StudentGradeSubjectValidation(IStudentDAL _studentDAL)
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
                if (!(user.student.Subjects[count].SubjectName == null))
                {
                    status = subjectList.Any(s => s.Contains(user.student.Subjects[count].SubjectName));

                }
                count++;
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
                if (!(user.student.Subjects[count].SubjectGrade.Equals('\0')))
                {
                    status = gradeList.Any(s => s.Contains(user.student.Subjects[count].SubjectGrade));
                }
                count++;
            }
            return status;

        }

    }
}