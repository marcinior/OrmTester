using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Entity
{
    public class Subject
    {
        [Key]
        [Required]
        public int SubjectId { get; set; }

        [Required]
        [MaxLength(45)]
        public string SubjectName { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public byte ECTS { get; set; }

        [Required]
        public byte ClassYear { get; set; }

        [Required]
        public ExamType ExamType { get; set; }

        public IList<StudentSubject> StudentSubjects { get; set; }
    }
}
