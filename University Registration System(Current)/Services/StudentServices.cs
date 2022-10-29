using System;
using System.Collections.Generic;
using UniversitySystemRegistration.Models;

namespace UniversitySystemRegistration.Services
{
    public class StudentServices: IStudentServices
    {
        public int SubjectTotalScoreCalculation(Student student)
        {
            int totalScore = 0;
            Dictionary<char, int> gradePoints = null;
            foreach (GradeScore scoreEnum in Enum.GetValues(typeof(GradeScore)))
            {
                gradePoints.Add(Convert.ToChar(scoreEnum), (int)scoreEnum);
            }

            for (int j = 0; j < student.Subjects.Count; j++)
            {
                int dicVal = 0;
                char scoreChar = student.Subjects[j].SubjectGrade;
                gradePoints.TryGetValue(scoreChar, out dicVal);
                totalScore += dicVal;
            }
            student.TotalScoreOfSubjects= totalScore;
            return totalScore;
        }
    }
}
