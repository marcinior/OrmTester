using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFramework.Entity
{
    public class Student
    {
        [Key]
        [Required]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(45)]
        public string LastName { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [StringLength(11)]         
        public string PESEL { get; set; }

        public int IndexForeignKey { get; set; }

        public Index Index { get; set; }

        public int ClassId { get; set; }

        public Class Class { get; set; }

        public IList<StudentSubject> StudentSubjects { get; set; }
    }
}
