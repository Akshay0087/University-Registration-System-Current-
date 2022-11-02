using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UniversitySystemRegistration.Models;
using UniversitySystemRegistration.Repository;

namespace RepositoryLibrary.DataAccessLayer
{
    public class StudentDAL:IStudentDAL
    {
        private readonly IDatabaseConnection databaseManipulation;
   
        public StudentDAL(IDatabaseConnection DatabaseManipulation)
        {
            this.databaseManipulation = DatabaseManipulation;
     
        }

        public bool SetUserID(User user)
        {
            var result = false;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@subjectId", user.UserId));
            return result = databaseManipulation.SetInfo(SqlQueries.insertUserIdIntoStudent, parameters);
        }
        public User GetStudentIDInfo(User user)
        {

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@userId", user.UserId));
            var dataTable = databaseManipulation.GetInfo(SqlQueries.studentInfoQuery, parameters);
            user.student.StudentGuardianInfo.GuardianId = Convert.ToInt32(dataTable.Rows[0]["GuardianId"]);
            user.student.StudentStatus = Convert.ToChar(dataTable.Rows[0]["StudentStatus"]);
            return user;
        }

        public User GetStudentGuardianInfo(User user)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@guardianId", user.student.StudentGuardianInfo.GuardianId));
            var dataTable = databaseManipulation.GetInfo(SqlQueries.studentGuardianInfo, parameters);
            user.student.StudentGuardianInfo.FirstName = dataTable.Rows[0]["FirstName"].ToString();
            user.student.StudentGuardianInfo.LastName = dataTable.Rows[0]["LastName"].ToString();
            return user;
        }

        public User GetStudentSubject(User user)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@studentId", user.student.StudentId));
            var dataTable = databaseManipulation.GetInfo(SqlQueries.studentSubjectInfo, parameters);
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
                    result = databaseManipulation.SetInfo(SqlQueries.updateSubject, parameters);
                }
                else
                {
                    result = databaseManipulation.SetInfo(SqlQueries.insertSubject, parameters);
                }
            }
            return result;
        }

        public bool SetUpdateStudentGuardian(User user, DbOperation dbOperation)
        {
            var result = false;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@firstName", user.student.StudentGuardianInfo.FirstName));
            parameters.Add(new SqlParameter("@lastName", user.student.StudentGuardianInfo.LastName));
            if (dbOperation.Equals(DbOperation.update))
            {
                parameters.Add(new SqlParameter("@guardianId", user.student.StudentGuardianInfo.GuardianId));
                result = databaseManipulation.SetInfo(SqlQueries.updateGuardian, parameters);
            }
            else
            {
                result = databaseManipulation.SetInfo(SqlQueries.insertGuardian, parameters);
                var dataTable = databaseManipulation.GetInfo(SqlQueries.selectIdentity, null);
                if (result)
                {
                    int identity = Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
                    List<SqlParameter> parametersSecond = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@guardianId", user.student.StudentGuardianInfo.GuardianId));
                    parameters.Add(new SqlParameter("@studentId", user.student.StudentId));
                    result = databaseManipulation.SetInfo(SqlQueries.insertIdInStudent, parameters);
                }
            }

            return result;
        }

        public List<String> StudentGradeList(string query)
        {
            List<String> list=new List<String>();
            var result = databaseManipulation.GetInfo(query,null);
            foreach (DataRow row in result.Rows)
            {
                list.Add(row[0].ToString());
            }
            return list;
        }

        

    }

}