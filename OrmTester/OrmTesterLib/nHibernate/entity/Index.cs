using System;

namespace OrmTesterLib.nHibernate.entity
{
    class Index
    {
        public virtual int IndexId { get; set; }

        public virtual string IndexNumber { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime UpdatedAt { get; set; }
    }
}
