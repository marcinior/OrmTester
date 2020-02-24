using System;

namespace NHibernateTester.Entities
{
    public class Index
    {
        public virtual int IndexId { get; set; }

        public virtual string IndexNumber { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime UpdatedAt { get; set; }

        public virtual Student Student { get; set; }
    }
}