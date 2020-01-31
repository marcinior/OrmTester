using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Entity
{
    public class Class
    {
        [Key]
        [Required]
        public int ClassId { get; set; }

        [Required]
        public byte GroupNumber { get; set; }

        [Required]
        public byte Year { get; set; }

        [Required]
        [MaxLength(30)]
        public string DegreeCourse { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public IList<Student> Students { get; set; }
    }
}
