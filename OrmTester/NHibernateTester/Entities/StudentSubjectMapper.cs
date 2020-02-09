using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateTester.Entities
{
    class StudentSubjectMapper : ClassMap<StudentSubject>
    {
        public StudentSubjectMapper()
        {
            Id(studentSubject => studentSubject.Id);
            References(studentSubject => studentSubject.StudentId).Not.Nullable();
            References(studentSubject => studentSubject.SubjectId).Not.Nullable();
        }
    }
}
