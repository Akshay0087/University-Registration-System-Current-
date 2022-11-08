using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UniversitySystemRegistration.Models;
using UniversitySystemRegistration.Repository;

namespace RepositoryLibrary.DataAccessLayer
{
    public class AdminDAL:IAdminDAL
    {
        private readonly IDatabaseConnection databaseManipulation;

        public AdminDAL(IDatabaseConnection DatabaseManipulation)
        {
            this.databaseManipulation = DatabaseManipulation;

        }
        public bool SetStudentStatus()
        {

            var resultClear = databaseManipulation.SetInfo(SqlQueries.clearStatusField, null);
            var dataTableTopStudent = databaseManipulation.GetInfo(SqlQueries.selectTop15StudentQuery, null) ;
            if (dataTableTopStudent.Rows.Count > 0)
            {
                foreach (DataRow row in dataTableTopStudent.Rows)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@char", "Approved"));
                    parameters.Add(new SqlParameter("@studentId", Convert.ToInt32(row["StudentId"])));
                    var resultTop = databaseManipulation.SetInfo(SqlQueries.insertStatus, parameters);

                }
            }
            var dataTableRejectedStudent = databaseManipulation.GetInfo(SqlQueries.selectRejectStudentQuery, null);
            if (dataTableRejectedStudent.Rows.Count > 0)
            {
                foreach (DataRow row in dataTableRejectedStudent.Rows)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@char", "Rejected"));
                    parameters.Add(new SqlParameter("@studentId", Convert.ToInt32(row["StudentId"])));
                    var resultRejected = databaseManipulation.SetInfo(SqlQueries.insertStatus, parameters);

                }
            }
            var dataTablePendingStudent = databaseManipulation.GetInfo(SqlQueries.selectPendingStudentQuery, null);
            if (dataTablePendingStudent.Rows.Count > 0)
            {
                foreach (DataRow row in dataTablePendingStudent.Rows)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@char", "Waiting"));
                    parameters.Add(new SqlParameter("@studentId", Convert.ToInt32(row["StudentId"])));
                    var resultWaiting = databaseManipulation.SetInfo(SqlQueries.insertStatus, parameters);
                }
            }
 
            return true;
        }

        public Tuple<List<Student>,List<Student>,List<Student>> GetStudentStatusFromDB()
        {
            List<Student> approvedStudent=new List<Student>();
            List<Student> waitingStudent = new List<Student>();
            List<Student> rejectedStudent = new List<Student>();

            

            List<SqlParameter> parametersApproved = new List<SqlParameter>();
            parametersApproved.Add(new SqlParameter("@char", "Approved"));
            var resultTop = databaseManipulation.GetInfo(SqlQueries.getStudentFromDB, parametersApproved);

            List<SqlParameter> parametersWaiting = new List<SqlParameter>();
            parametersWaiting.Add(new SqlParameter("@char", "Waiting"));
            var resultWaiting = databaseManipulation.GetInfo(SqlQueries.getStudentFromDB, parametersWaiting);

            List<SqlParameter> parametersRejected = new List<SqlParameter>();
            parametersRejected.Add(new SqlParameter("@char", "Rejected"));
            var resultRejected = databaseManipulation.GetInfo(SqlQueries.getStudentFromDB, parametersRejected);

            foreach (DataRow row in resultTop.Rows)
            {
                
                Student stud = new Student();
                stud.StudentId= Convert.ToInt32(row["StudentId"]);
                stud.TotalSubjectPoints = Convert.ToInt32(row["GradePoint"]);
                stud.StudentFullName = string.Concat(row["FirstName"].ToString(), " ", row["LastName"].ToString());
                approvedStudent.Add(stud);
            }
            foreach (DataRow row in resultWaiting.Rows)
            {

                Student stud = new Student();
                stud.StudentId = Convert.ToInt32(row["StudentId"]);
                stud.TotalSubjectPoints = Convert.ToInt32(row["GradePoint"]);
                stud.StudentFullName = string.Concat(row["FirstName"].ToString(), " ", row["LastName"].ToString());
                waitingStudent.Add(stud);
            }
            foreach (DataRow row in resultRejected.Rows)
            {

                Student stud = new Student();
                stud.StudentId = Convert.ToInt32(row["StudentId"]);
                stud.TotalSubjectPoints = Convert.ToInt32(row["GradePoint"]);
                stud.StudentFullName = string.Concat(row["FirstName"].ToString()," ",row["LastName"].ToString());
                rejectedStudent.Add(stud);
            }

            var tuple = new Tuple<List<Student>, List<Student>, List<Student>>(approvedStudent,waitingStudent,rejectedStudent);
            return tuple;
        }
    }
}
