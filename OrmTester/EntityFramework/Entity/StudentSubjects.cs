using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Entity
{
    public class StudentSubjects
    {
        [Key]
        public int StudentSubjectId { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }
    }
}
