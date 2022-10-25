namespace UniversitySystemRegistration.Models.Entity
{
    public class Subject
    {
        public string subjectName { get; set; }
        public int subjectId { get;private set; }
        public char gradeSubject { get; set; } //A,B,C
    }
}
