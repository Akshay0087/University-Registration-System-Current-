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
        const string insertSubject = "INSERT INTO SubjectResult(SubjectId,StudentId,Grade)values(@subjectId,@studentId,@grade)";

        public
        const string updateSubject = "UPDATE SubjectResult SET SubjectId=@subjectId,StudentId=@studentId,Grade=@grade where SubjectResultId=@subjectResultId";

        public
        const string insertUserIdIntoStudent = "INSERT INTO Student(StudentId)values(@studentId)";

        public
        const string insertGuardian = "INSERT INTO Guardian(FirstName,LastName)values(@firstName,@lastName)";

        public
        const string updateGuardian = "UPDATE Guardian SET FirstName=@firstName,LastName=@LastName where Guardian=@guardian";
        public
        const string selectIdentity = "select @@IDENTITY";
        public
        const string insertIdInStudent = "INSERT INTO Student(GuardianId)values(@guardianId) where StudentId=@studentId";
        public
        const string getSubjectList = "SELECT SubjectName from Subject";

        public const string insertIntoUser = "insert into Users (Emailaddress,Passwordhash,role)values" +
           "(@emailaddress,@passwordhash,@role)";
        public const string getUserDataQuery = "SELECT ui.NID,u.UserId,ui.FirstName,ui.LastName,ui.DateOfBirth,ui.UserAddress,ui.Phone,u.Role from Users u inner join UsersInfo ui on u.UserId=ui.UserId where u.EmailAddress=@emailAddress";
        public const string selectIndentity = "select UserId from Users where EmailAddress=@emailAddress";
        public const string insertIntoUsersInfo = "insert into UsersInfo (Nid,FirstName,LastName,UserAddress,Phone,DateOfBirth,UserId) values (@nid,@firstname,@lastname,@address,@phone,@dateOfBirth,@userId)";
    }
}
