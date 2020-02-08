using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmTesterLib.NHibernate.entity
{
    class IndexMapper : ClassMap<Index>
    {
        public IndexMapper()
        {
            Id(index => index.IndexId);
            Map(index => index.IndexNumber).Not.Nullable().Length(7);
            Map(index => index.CreatedAt).Not.Nullable();
            Map(index => index.UpdatedAt).Not.Nullable();
        }
    }
}
