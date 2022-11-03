using System.Runtime.Remoting.Messaging;
using UniversitySystemRegistration.Models;

namespace RepositoryLibrary.DataAccessLayer
{
    public class SqlQueries
    {

        public
        const string studentInfoQuery = "SELECT GuardianId,StudentStatus FROM Student where StudentId=@userId";

        public
        const string studentGuardianInfo = "SELECT FirstName,LastName FROM Guardian where GuardianId=@guardianId";

        public
        const string studentSubjectInfo = "SELECT su.SubjectName,su.SubjectId,sr.Grade FROM SubjectResult sr inner join Subject su on sr.SubjectId=su.SubjectId where sr.StudentId=@studentID";

        public
       const string selectSubjectId = "Select SubjectId from Subject where SubjectName=@subjectName";

        public
        const string insertSubject = "INSERT INTO SubjectResult(SubjectId,StudentId,Grade)values(@subjectId,@studentId,@grade)";

        public
        const string insertUserIdIntoStudent = "INSERT INTO Student(StudentId)values(@studentId)";

        public
        const string insertGuardian = "INSERT INTO Guardian(FirstName,LastName)values(@firstName,@lastName)";

        public const string getGuardianId = "Select GuardianId from Guardian where FirstName=@firstName AND LastName=@lastName";

        public
        const string loginCheckQuery = "SELECT PasswordHash FROM Users where EmailAddress=@emailAddress";
        public
        const string infoCheckQuery = "SELECT u.UserId FROM Users u inner join UsersInfo ui on u.UserId=ui.UserId where u.EmailAddress=@emailAddress OR ui.Phone=@phoneNum OR ui.NID=@nid";
        public
        const string insertIdInStudent = "Update Student set GuardianId=@guardianId where StudentId=@studentId";

        public const string getGuardianIdFromStudentId="SELECT GuardianId from Student where StudentId = @studentId";

        public const string getStudentSubjects = "Select Grade,SubjectId,SubjectName from SubjectResult sr inner join Subject s on sr.SubjectId=s.SubjectId";

        public
        const string getSubjectList = "SELECT SubjectName from Subject";
        public
        const string getGradeList = "SELECT Grade from GradeInfo";

        public const string insertIntoUser = "insert into Users (Emailaddress,Passwordhash,role)values" +
           "(@emailaddress,@passwordhash,@role)";
        public const string getUserDataQuery = "SELECT ui.NID,u.UserId,ui.FirstName,ui.LastName,ui.DateOfBirth,ui.UserAddress,ui.Phone,u.Role from Users u inner join UsersInfo ui on u.UserId=ui.UserId where u.EmailAddress=@emailAddress";
        public const string selectIndentity = "select UserId from Users where EmailAddress=@emailAddress";
        public const string insertIntoUsersInfo = "insert into UsersInfo (Nid,FirstName,LastName,UserAddress,Phone,DateOfBirth,UserId) values (@nid,@firstname,@lastname,@address,@phone,@dateOfBirth,@userId)";
    }
}
