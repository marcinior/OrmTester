using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.Entity
{
    public class Index
    {
        [Key]
        public int IndexId { get; set; }

        [Required]
        [StringLength(7)]
        public string IndexNumber { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public Student Student { get; set; }
    }
}
