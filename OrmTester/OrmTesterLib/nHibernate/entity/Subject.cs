using System;
using System.Collections.Generic;

namespace OrmTesterLib.nHibernate.entity
{
    class Subject
    {
        public virtual int SubjectId { get; set; }

        public virtual string SubjectName { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime UpdatedAt { get; set; }

        public virtual byte Ects { get; set; }

        public virtual byte ClassesYear { get; set; }

        public virtual ExamType ExamType { get; set; }

        public virtual List<StudentSubject> StudentSubject { get; set; }
    }
}
