using RepositoryLibrary.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UniversitySystemRegistration.Models;
using UniversitySystemRegistration.Services;

namespace ServiceLibrary.Services
{
    public class StudentInterfaceFormValidation: IStudentInterfaceFormValidation
    {

        private readonly IStudentServices _studentServices;
        public StudentInterfaceFormValidation(IStudentServices studentServices)
        {

            _studentServices = studentServices;
        }

        public bool subjectValidation(User user)
        {
            bool status = true;
            var subjectList = _studentServices.GetListOfData(SqlQueries.getSubjectList);
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
            var subjectList = _studentServices.GetListOfData(SqlQueries.getGradeList);
            int count = 0;

            while (status && count < user.student.Subjects.Count)
            {
                status = subjectList.Any(s => s.Equals(user.student.Subjects[count].SubjectGrade));
            }
            return status;

        }



    }
}
