using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmTesterLib.nHibernate.entity
{
    class StudentMapper : ClassMap<Student>
    {
        public StudentMapper()
        {
            Id(student => student.StudentId);
            Map(student => student.BirthDate).Not.Nullable();
            References(student => student.ClassId).Not.Nullable();
            Map(student => student.FirstName).Length(20).Not.Nullable();
            Map(student => student.Gender).Not.Nullable();
            References(student => student.IndexId);
            Map(student => student.LastName).Length(45).Not.Nullable();
            Map(student => student.Pesel).Length(11).Nullable();            
            Map(student => student.CreatedAt).Not.Nullable();
            Map(student => student.UpdatedAt).Not.Nullable();
            HasMany(student => student.StudentSubject);
        }
    }
}
