using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmTesterLib.NHibernate.entity
{
    class SubjectMapper :ClassMap<Subject>
    {
        public SubjectMapper()
        {
            Id(subject => subject.SubjectId);
            Map(subject => subject.ClassesYear).Not.Nullable();
            Map(subject => subject.Ects).Not.Nullable();
            Map(subject => subject.ExamType).Not.Nullable();
            Map(subject => subject.SubjectName).Not.Nullable();
            Map(subject => subject.CreatedAt).Not.Nullable();
            Map(subject => subject.UpdatedAt).Not.Nullable();
            HasMany(subject => subject.StudentSubject);
        }
    }
}
