using UniversitySystemRegistration.Models;

namespace ServiceLibrary.Services
{
    public interface IStudentGuardianSubjectValidation
    {
        bool subjectValidation(User user);

        bool gradeValidation(User user);
    }
}
