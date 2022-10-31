using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UniversitySystemRegistration.Models;
using UniversitySystemRegistration.Repository;

namespace RepositoryLibrary.DataAccessLayer
{
    public class StudentDAL
    {
        private readonly IDatabaseConnection databaseManipulation;
        public StudentDAL(IDatabaseConnection DatabaseManipulation)
        {
            this.databaseManipulation = DatabaseManipulation;
        }

        private
        const string studentInfoQuery = "SELECT GuardianId,StudentStatus FROM Student where StudentId=@userId";

        private
        const string studentGuardianInfo = "SELECT FirstName,LastName FROM Guardian where GuardianId=@guardianId";

        private
        const string studentSubjectInfo = "SELECT su.SubjectName,su.SubjectId,sr.Grade FROM SubjectResult sr inner join Subject su on sr.SubjectId=su.SubjectId where sr.StudentId=@studentID";

        private
        const string insertSubject = "INSERT INTO SubjectResult(SubjectId,StudentId,Grade)values(@subjectId,@studentId,@grade)";

        private
        const string updateSubject = "UPDATE SubjectResult SET SubjectId=@subjectId,StudentId=@studentId,Grade=@grade where SubjectResultId=@subjectResultId";

        private
        const string insertUserIdIntoStudent = "INSERT INTO Student(StudentId)values(@studentId)";

        private
        const string insertGuardian = "INSERT INTO Guardian(FirstName,LastName)values(@firstName,@lastName)";

        private
        const string updateGuardian = "UPDATE Guardian SET FirstName=@firstName,LastName=@LastName where Guardian=@guardian";
        private
        const string selectIdentity = "select @@IDENTITY";
        private
        const string insertIdInStudent = "INSERT INTO Student(GuardianId)values(@guardianId) where StudentId=@studentId";

        public bool SetUserID(User user)
        {
            var result = false;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@subjectId", user.UserId));
            return result = databaseManipulation.SetInfo(insertUserIdIntoStudent, parameters);
        }
        public User GetStudentIDInfo(User user)
        {

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@userId", user.UserId));
            var dataTable = databaseManipulation.GetInfo(studentInfoQuery, parameters);
            user.student.StudentGuardienInfo.GuardianId = Convert.ToInt32(dataTable.Rows[0]["GuardianId"]);
            user.student.StudentStatus = Convert.ToChar(dataTable.Rows[0]["StudentStatus"]);
            return user;
        }

        public User GetStudentGuardianInfo(User user)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@guardianId", user.student.StudentGuardienInfo.GuardianId));
            var dataTable = databaseManipulation.GetInfo(studentGuardianInfo, parameters);
            user.student.StudentGuardienInfo.FirstName = dataTable.Rows[0]["FirstName"].ToString();
            user.student.StudentGuardienInfo.LastName = dataTable.Rows[0]["LastName"].ToString();
            return user;
        }

        public User GetStudentSubject(User user)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@studentId", user.student.StudentId));
            var dataTable = databaseManipulation.GetInfo(studentSubjectInfo, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                user.student.Subjects.Add(new Subject(row["SubjectName"].ToString(), Convert.ToInt32(row["SubjectId"]), Convert.ToChar(row["Grade"])));
            }
            return user;
        }

        public bool SetUpdateStudentSubject(User user, DbOperation dbOperation)
        {
            var result = false;
            foreach (var subject in user.student.Subjects)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@subjectId", subject.SubjectId));
                parameters.Add(new SqlParameter("@studentId", user.student.StudentId));
                parameters.Add(new SqlParameter("@grade", subject.SubjectGrade));
                if (dbOperation.Equals(DbOperation.update))
                {
                    parameters.Add(new SqlParameter("@subjectResultId", subject.SubjectGrade));
                    result = databaseManipulation.SetInfo(updateSubject, parameters);
                }
                else
                {
                    result = databaseManipulation.SetInfo(insertSubject, parameters);
                }
            }
            return result;
        }

        public bool SetUpdateStudentGuardian(User user, DbOperation dbOperation)
        {
            var result = false;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@firstName", user.student.StudentGuardienInfo.FirstName));
            parameters.Add(new SqlParameter("@lastName", user.student.StudentGuardienInfo.LastName));
            if (dbOperation.Equals(DbOperation.update))
            {
                parameters.Add(new SqlParameter("@guardianId", user.student.StudentGuardienInfo.GuardianId));
                result = databaseManipulation.SetInfo(updateGuardian, parameters);
            }
            else
            {
                result = databaseManipulation.SetInfo(insertGuardian, parameters);
                var dataTable = databaseManipulation.GetInfo(selectIdentity, null);
                if (result)
                {
                    int identity = Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
                    List<SqlParameter> parametersSecond = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@guardianId", user.student.StudentGuardienInfo.GuardianId));
                    parameters.Add(new SqlParameter("@studentId", user.student.StudentId));
                    result = databaseManipulation.SetInfo(insertIdInStudent, parameters);
                }
            }

            return result;
        }

    }

}