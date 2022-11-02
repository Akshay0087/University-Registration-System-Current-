using System.Collections.Generic;
using UniversitySystemRegistration.Models;

namespace UniversitySystemRegistration.Services
{
    public interface IStudentServices
    {
        int SubjectTotalScoreCalculation(Student student);
        List<string> GetSubjectAndGradeList(string query);
    }
}