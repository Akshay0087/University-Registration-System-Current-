namespace RepositoryLibrary.DataAccessLayer
{
    public class SqlQueries
    {
        public const string studentInfoQuery =
            "SELECT GuardianId,StudentStatus FROM Student where StudentId=@userId";

        public const string studentGuardianInfo =
            "SELECT FirstName,LastName FROM Guardian where GuardianId=@guardianId";

        public const string studentSubjectInfo =
            "SELECT su.SubjectName,su.SubjectId,sr.Grade FROM SubjectResult sr inner join Subject su on sr.SubjectId=su.SubjectId where sr.StudentId=@studentID";

        public const string selectSubjectId =
            "Select SubjectId from Subject where SubjectName=@subjectName";

        public const string insertSubject =
            "INSERT INTO SubjectResult(SubjectId,StudentId,Grade)values(@subjectId,@studentId,@grade)";

        public const string insertUserIdIntoStudent =
            "INSERT INTO Student(StudentId)values(@studentId)";

        public const string insertGuardian =
            "INSERT INTO Guardian(FirstName,LastName)values(@firstName,@lastName)";

        public const string getGuardianId =
            "Select GuardianId from Guardian where FirstName=@firstName AND LastName=@lastName";

        public const string loginCheckQuery =
            "SELECT PasswordHash FROM Users where EmailAddress=@emailAddress";
        public const string infoCheckQuery =
            "SELECT u.UserId FROM Users u inner join UsersInfo ui on u.UserId=ui.UserId where u.EmailAddress=@emailAddress OR ui.Phone=@phoneNum OR ui.NID=@nid";
        public const string insertIdInStudent =
            "insert into Student(StudentId,GuardianId)values(@studentId,@guardianId)";

        public const string getGuardianIdFromStudentId =
            "SELECT GuardianId,StudentStatus from Student where StudentId = @studentId";

        public const string getStudentSubjects =
            "Select sr.Grade,sr.SubjectId,s.SubjectName from SubjectResult sr inner join Subject s on sr.SubjectId=s.SubjectId where StudentId=@studentId";

        public const string getSubjectList = "SELECT SubjectName from Subject";
        public const string getGradeList = "SELECT Grade from GradeInfo";

        public const string insertIntoUser =
            "insert into Users (Emailaddress,Passwordhash,role)values"
            + "(@emailaddress,@passwordhash,@role)";
        public const string getUserDataQuery =
            "SELECT ui.NID,u.UserId,ui.FirstName,ui.LastName,ui.DateOfBirth,ui.UserAddress,ui.Phone,u.Role from Users u inner join UsersInfo ui on u.UserId=ui.UserId where u.EmailAddress=@emailAddress";
        public const string selectIndentity =
            "select UserId from Users where EmailAddress=@emailAddress";
        public const string insertIntoUsersInfo =
            "insert into UsersInfo (Nid,FirstName,LastName,UserAddress,Phone,DateOfBirth,UserId) values (@nid,@firstname,@lastname,@address,@phone,@dateOfBirth,@userId)";

        public const string selectTop15StudentQuery =
        "Select top(15) StudentId,sum(GradePoint) As GradePoint from(Select SubjectResult.StudentId as StudentId, GradeInfo.GradePoint as GradePoint From SubjectResult inner join Subject on SubjectResult.SubjectId= Subject.SubjectId inner join GradeInfo on GradeInfo.Grade= SubjectResult.Grade) finalResult group by finalResult.StudentId having sum(GradePoint)>9 order by sum(GradePoint) desc, StudentId asc";
        public const string selectRejectStudentQuery =
        "Select StudentId,sum(GradePoint) As GradePoint from(Select SubjectResult.StudentId as StudentId, GradeInfo.GradePoint as GradePoint From SubjectResult inner join Subject on SubjectResult.SubjectId= Subject.SubjectId inner join GradeInfo on GradeInfo.Grade= SubjectResult.Grade) finalResult group by finalResult.StudentId having sum(GradePoint)<10";
        public const string selectPendingStudentQuery = "Select StudentId from Student where StudentStatus='P'";
        public const string insertStatus = "Update Student SET StudentStatus=@char where StudentId=@studentId";
        public const string clearStatusField = "Update Student SET StudentStatus='P'";
        public const string getStudentFromDB = "select UsersInfo.FirstName, UsersInfo.LastName, StudentId,sum(GradePoint) As GradePoint  from (Select SubjectResult.StudentId as StudentId,GradeInfo.GradePoint as GradePoint  From SubjectResult  inner join Subject  on SubjectResult.SubjectId=Subject.SubjectId inner join GradeInfo on GradeInfo.Grade=SubjectResult.Grade inner join Student on Student.StudentId=SubjectResult.StudentId where Student.StudentStatus=@char) finalResult inner Join UsersInfo on UsersInfo.UserId = finalResult.StudentId group by finalResult.StudentId, UsersInfo.FirstName, UsersInfo.LastName";
    }
}
