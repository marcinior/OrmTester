using NHibernateTester.Enums;
using System;
using System.Collections.Generic;

namespace NHibernateTester.Entities
{
    public class Student
    {
        public virtual int StudentId { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime UpdatedAt { get; set; }

        public virtual DateTime BirthDate { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual string Pesel { get; set; }

        public virtual Index IndexId { get; set; }

        public virtual Class ClassId { get; set; }

        public virtual IList<StudentSubject> StudentSubject { get; set; }

        public Student()
        {
            StudentSubject = new List<StudentSubject>();
        }
    }
}