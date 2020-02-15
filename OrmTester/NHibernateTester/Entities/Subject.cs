using NHibernateTester.Enums;
using System;
using System.Collections.Generic;

namespace NHibernateTester.Entities
{
    public class Subject
    {
        public virtual int SubjectId { get; set; }

        public virtual string SubjectName { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime UpdatedAt { get; set; }

        public virtual byte Ects { get; set; }

        public virtual byte ClassesYear { get; set; }

        public virtual ExamType ExamType { get; set; }

        public virtual IList<StudentSubject> StudentSubject { get; set; }

        public Subject()
        {
            StudentSubject = new List<StudentSubject>();
        }
    }
}