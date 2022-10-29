using UniversitySystemRegistration.Models;

namespace UniversitySystemRegistration.Services
{
    public interface IStudentServices
    {
        int SubjectTotalScoreCalculation(Student student);
    }
}