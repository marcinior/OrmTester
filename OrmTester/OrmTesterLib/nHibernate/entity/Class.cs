using System;

namespace OrmTesterLib.nHibernate.entity
{
    class Class
    {
        public int ClassId { get; set; }
        public short GroupNumber { get; set; }
        public short Year { get; set; }
        public string DegreeCourse { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
