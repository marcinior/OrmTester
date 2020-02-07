using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmTesterLib.nHibernate.entity
{
    class ClassMapper : ClassMap<Class>
    {
        public ClassMapper()
        {
            Id(classObject => classObject.ClassId);
            Map(classObject => classObject.GroupNumber).Not.Nullable();
            Map(classObject => classObject.DegreeCourse).Length(30).Not.Nullable();
            Map(classObject => classObject.CreatedAt).Not.Nullable();
            Map(classObject => classObject.UpdatedAt).Not.Nullable();
            Map(classObject => classObject.Year).Not.Nullable();
            HasMany(classObject => classObject.Student);
        }
    }
}
