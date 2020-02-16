using System;
using System.Collections.Generic;

namespace NHibernateTester.Entities
{
    public class Class
    {
        public virtual int ClassId { get; set; }

        public virtual byte GroupNumber { get; set; }

        public virtual byte Year { get; set; }

        public virtual string DegreeCourse { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime UpdatedAt { get; set; }

        public virtual IList<Student> Student { get; set; }

        public Class()
        {
            Student = new List<Student>();
        }
    }
}
