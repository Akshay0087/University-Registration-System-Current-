using System;
using System.Collections.Generic;
using UniversitySystemRegistration.Models.Entity;

namespace UniversitySystemRegistration.Business_Logic
{
    public interface IStudentServices
    {
        int ScoreCalculation(Student student);
    }
    public class StudentServices: IStudentServices
    {
       
        public int ScoreCalculation(Student student)
        {
            int totalValue = 0;
            Dictionary<char, int> dictionary = null;

            foreach (score scoreEnum in Enum.GetValues(typeof(score)))
            {
                dictionary.Add(Convert.ToChar(scoreEnum), (int)scoreEnum);
            }

            for (int j = 0; j < student.subjects.Count; j++)
            {
                int dicVal = 0;
                char scoreChar = student.subjects[j].gradeSubject;
                dictionary.TryGetValue(scoreChar, out dicVal);
                totalValue += dicVal;
            }
            student.TotalScore= totalValue;
            return totalValue;
        }

        //modify student info


    }
}
