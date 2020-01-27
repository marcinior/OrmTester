using System;

namespace OrmTesterLib.nHibernate.entity
{
    class Subject
    {
        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public short Ects { get; set; }

        public short ClassesYear { get; set; }


    }
}
