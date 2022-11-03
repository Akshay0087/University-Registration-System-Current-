using UniversitySystemRegistration.Models;

namespace ServiceLibrary.Services
{
    public interface IStudentInterfaceFormValidation
    {


        bool subjectValidation(User user);


        bool gradeValidation(User user);
           



        }
    }
