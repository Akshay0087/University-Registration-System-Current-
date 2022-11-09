using UniversitySystemRegistration.Models;

namespace ServiceLibrary.Services
{
    public interface IStudentGradeSubjectValidation
    {
        bool subjectValidation(User user);

        bool gradeValidation(User user);
    }
}