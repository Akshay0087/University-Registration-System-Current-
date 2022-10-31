namespace UniversitySystemRegistration.Models
{
    public class Subject
    {

        public Subject(string SubjectName, int SubjectId, char SubjectGrade)
        {
            this.SubjectName = SubjectName;
            this.SubjectId = SubjectId;
            this.SubjectGrade = SubjectGrade;
        }

        public string SubjectName { get; set; }
        public int SubjectId { get; set; }
        public char SubjectGrade { get; set; } 
    }
}
