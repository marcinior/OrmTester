using System;
using System.Collections.Generic;

namespace OrmTesterLib.NHibernate.entity
{
    class Class
    {
        public virtual int ClassId { get; set; }

        public virtual byte GroupNumber { get; set; }

        public virtual byte Year { get; set; }

        public virtual string DegreeCourse { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime UpdatedAt { get; set; }

        public virtual List<Student> Student { get; set; }
    }
}
