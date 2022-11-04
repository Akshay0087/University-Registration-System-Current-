using RepositoryLibrary.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
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

        public bool SetStudentSubject(User user)
        {
            var result = false;
            for(var i=0; i < user.student.Subjects.Count; i++) {
                if (!(user.student.Subjects[i].SubjectName==null)) {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@subjectName", user.student.Subjects[i].SubjectName));
                    var dataTable = databaseManipulation.GetInfo(SqlQueries.selectSubjectId, parameters);
                    user.student.Subjects[i].SubjectId = Convert.ToInt32(dataTable.Rows[0]["SubjectId"]);
                }
                
                
            }
            for (var j = 0; j < user.student.Subjects.Count; j++)
            {

                if (!(user.student.Subjects[j].SubjectName == null))
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@subjectId", user.student.Subjects[j].SubjectId));
                    parameters.Add(new SqlParameter("@studentId", user.student.StudentId));
                    parameters.Add(new SqlParameter("@grade", user.student.Subjects[j].SubjectGrade));
                    result = databaseManipulation.SetInfo(SqlQueries.insertSubject, parameters);
                    
                }

            }
            return result;
        }

        public bool SetStudentGuardian(User user)
        {
            var result = false;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@firstName", user.student.StudentGuardianInfo.FirstName));
            parameters.Add(new SqlParameter("@lastName", user.student.StudentGuardianInfo.LastName));

            var tuple = IsGuardianExist(parameters);
            List<SqlParameter> parametersSecond = new List<SqlParameter>();
            if (tuple.Item1)
            {
                user.student.StudentGuardianInfo.GuardianId = tuple.Item2;
            }
            else
            {
                var resultSet = databaseManipulation.SetInfo(SqlQueries.insertGuardian, parameters);
                var dataTableResult = databaseManipulation.GetInfo(SqlQueries.getGuardianId,parameters);
                user.student.StudentGuardianInfo.GuardianId = Convert.ToInt32(dataTableResult.Rows[0]["GuardianId"]);
            }
            parametersSecond.Add(new SqlParameter("@guardianId", user.student.StudentGuardianInfo.GuardianId));
            parametersSecond.Add(new SqlParameter("@studentId", user.student.StudentId));
            result = databaseManipulation.SetInfo(SqlQueries.insertIdInStudent, parametersSecond);

            return result;
        }

        public Tuple<bool, int> IsGuardianExist(List<SqlParameter> parameter)
        {
            bool status = false;
            int guardianId=0;
            var dataTable = databaseManipulation.GetInfo(SqlQueries.getGuardianId, parameter);
            if (dataTable.Rows.Count > 0)
            {
                status = true;
                guardianId = Convert.ToInt32(dataTable.Rows[0]["GuardianId"]);
              
            }
            var tuple = new Tuple<bool, int>(status,guardianId);
            return tuple;

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

        public Tuple<bool,User> GetStudentData(User user)
        {
            var status = false;
            var query = "Select FirstName,LastName from Guardian where GuardianId=@guardianId";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@studentId", user.UserId));
            var dataTableResult = databaseManipulation.GetInfo(SqlQueries.getGuardianIdFromStudentId, parameters);
            if (dataTableResult.Rows.Count > 0)
            {
                status = true;
                Student stud = new Student();
                
                Guardian guardian = new Guardian();
                guardian.GuardianId = Convert.ToInt32(dataTableResult.Rows[0]["GuardianId"]);
                stud.StudentGuardianInfo = guardian;
                stud.StudentId = user.UserId;
                if (!((dataTableResult.Rows[0]["StudentStatus"].ToString())=="" )){
                    stud.StudentStatus = Convert.ToChar(dataTableResult.Rows[0]["StudentStatus"]);
                }
                user.student = stud;
                List<SqlParameter> parametersSecond = new List<SqlParameter>();
                parametersSecond.Add(new SqlParameter("@studentId", user.UserId));
                var dataTableResultSecond = databaseManipulation.GetInfo(SqlQueries.getStudentSubjects, parametersSecond);
                foreach (DataRow row in dataTableResultSecond.Rows)
                {
                    user.student.Subjects.Add(new Subject(row["SubjectName"].ToString(), Convert.ToInt32(row["SubjectId"]), Convert.ToChar(row["Grade"])));
                }
                List<SqlParameter> parameterThird = new List<SqlParameter>();
                parameterThird.Add(new SqlParameter("@guardianId", user.student.StudentGuardianInfo.GuardianId));
                var dataTableResultThird = databaseManipulation.GetInfo(query, parameterThird);
                user.student.StudentGuardianInfo.FirstName=dataTableResultThird.Rows[0]["FirstName"].ToString();
                user.student.StudentGuardianInfo.LastName = dataTableResultThird.Rows[0]["LastName"].ToString();
            }
            var tuple = new Tuple<bool, User>(status, user);

            return tuple;
           
        }

       


    }

}