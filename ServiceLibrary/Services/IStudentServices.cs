using System;
using System.Collections.Generic;
using UniversitySystemRegistration.Models;

namespace UniversitySystemRegistration.Services
{
    public interface IStudentServices
    {
        int SubjectTotalScoreCalculation(Student student);
        List<string> GetListOfData(string query);

        bool SaveStudentSubject(User user);


        bool SaveStudentGuardian(User user);

        Tuple<bool, User> GetStudentDataFromDb(User user);

        Student StudentInfoForDetailScreen(User user);


    }
}