using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using UniversitySystemRegistration.Models;
using UniversitySystemRegistration.Repository;
using System.Data;
using System.Security.Cryptography;

namespace RepositoryLibrary.DataAccessLayer
{
    public class AdminDAL:IAdminDAL
    {
        private readonly IDatabaseConnection databaseManipulation;

        public AdminDAL(IDatabaseConnection DatabaseManipulation)
        {
            this.databaseManipulation = DatabaseManipulation;

        }


        string selectTop15StudentQuery =
        "Select top(15) StudentId,sum(GradePoint) As GradePoint from(Select SubjectResult.StudentId as StudentId, GradeInfo.GradePoint as GradePoint From SubjectResult inner join Subject on SubjectResult.SubjectId= Subject.SubjectId inner join GradeInfo on GradeInfo.Grade= SubjectResult.Grade) finalResult group by finalResult.StudentId having sum(GradePoint)>9 order by sum(GradePoint) desc, StudentId asc";
        string selectRejectStudentQuery =
        "Select StudentId,sum(GradePoint) As GradePoint from(Select SubjectResult.StudentId as StudentId, GradeInfo.GradePoint as GradePoint From SubjectResult inner join Subject on SubjectResult.SubjectId= Subject.SubjectId inner join GradeInfo on GradeInfo.Grade= SubjectResult.Grade) finalResult group by finalResult.StudentId having sum(GradePoint)<10";
        string selectPendingStudentQuery = "Select StudentId from Student where StudentStatus='P'";
        string insertStatus = "Update Student SET StudentStatus=@char where StudentId=@studentId";
        string clearStatusField = "Update Student SET StudentStatus='P'";
        public bool SetStudentStatus()
        {

            var resultClear = databaseManipulation.SetInfo(clearStatusField, null);
            var dataTableTopStudent = databaseManipulation.GetInfo(selectTop15StudentQuery, null) ;
            if (dataTableTopStudent.Rows.Count > 0)
            {
                foreach (DataRow row in dataTableTopStudent.Rows)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@char", "A"));
                    parameters.Add(new SqlParameter("@studentId", Convert.ToInt32(row["StudentId"])));
                    var resultTop = databaseManipulation.SetInfo(insertStatus, parameters);

                }
            }
            var dataTableRejectedStudent = databaseManipulation.GetInfo(selectRejectStudentQuery, null);
            if (dataTableRejectedStudent.Rows.Count > 0)
            {
                foreach (DataRow row in dataTableRejectedStudent.Rows)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@char", "R"));
                    parameters.Add(new SqlParameter("@studentId", Convert.ToInt32(row["StudentId"])));
                    var resultRejected = databaseManipulation.SetInfo(insertStatus, parameters);

                }
            }
            var dataTablePendingStudent = databaseManipulation.GetInfo(selectPendingStudentQuery, null);
            if (dataTablePendingStudent.Rows.Count > 0)
            {
                foreach (DataRow row in dataTablePendingStudent.Rows)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@char", "W"));
                    parameters.Add(new SqlParameter("@studentId", Convert.ToInt32(row["StudentId"])));
                    var resultWaiting = databaseManipulation.SetInfo(insertStatus, parameters);
                }
            }
 
            return true;
        }

        public Tuple<List<Student>,List<Student>,List<Student>> GetStudentStatusFromDB()
        {
            List<Student> approvedStudent=new List<Student>();
            List<Student> waitingStudent = new List<Student>();
            List<Student> rejectedStudent = new List<Student>();

            string getStudentFromDB = "select UsersInfo.FirstName, UsersInfo.LastName, StudentId,sum(GradePoint) As GradePoint  from (Select SubjectResult.StudentId as StudentId,GradeInfo.GradePoint as GradePoint  From SubjectResult  inner join Subject  on SubjectResult.SubjectId=Subject.SubjectId inner join GradeInfo on GradeInfo.Grade=SubjectResult.Grade inner join Student on Student.StudentId=SubjectResult.StudentId where Student.StudentStatus=@char) finalResult inner Join UsersInfo on UsersInfo.UserId = finalResult.StudentId group by finalResult.StudentId, UsersInfo.FirstName, UsersInfo.LastName";

            List<SqlParameter> parametersApproved = new List<SqlParameter>();
            parametersApproved.Add(new SqlParameter("@char", "A"));
            var resultTop = databaseManipulation.GetInfo(getStudentFromDB, parametersApproved);

            List<SqlParameter> parametersWaiting = new List<SqlParameter>();
            parametersWaiting.Add(new SqlParameter("@char", "W"));
            var resultWaiting = databaseManipulation.GetInfo(getStudentFromDB, parametersWaiting);

            List<SqlParameter> parametersRejected = new List<SqlParameter>();
            parametersRejected.Add(new SqlParameter("@char", "R"));
            var resultRejected = databaseManipulation.GetInfo(getStudentFromDB, parametersRejected);

            foreach (DataRow row in resultTop.Rows)
            {
                
                Student stud = new Student();
                stud.StudentId= Convert.ToInt32(row["StudentId"]);
                stud.TotalSubjectPoints = Convert.ToInt32(row["GradePoint"]);
                stud.StudentFullName = string.Concat(row["FirstName"].ToString(), row["LastName"].ToString());
                approvedStudent.Add(stud);
            }
            foreach (DataRow row in resultWaiting.Rows)
            {

                Student stud = new Student();
                stud.StudentId = Convert.ToInt32(row["StudentId"]);
                stud.TotalSubjectPoints = Convert.ToInt32(row["GradePoint"]);
                stud.StudentFullName = string.Concat(row["FirstName"].ToString(), row["LastName"].ToString());
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
