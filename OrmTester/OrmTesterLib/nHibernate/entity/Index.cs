using System;

namespace OrmTesterLib.nHibernate.entity
{
    class Index
    {
        public int IndexId { get; set; }

        public string IndexNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
