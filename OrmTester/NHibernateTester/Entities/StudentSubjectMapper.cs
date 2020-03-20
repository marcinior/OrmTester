using FluentNHibernate.Mapping;

namespace NHibernateTester.Entities
{
    class StudentSubjectMapper : ClassMap<StudentSubject>
    {
        public StudentSubjectMapper()
        {
            Id(studentSubject => studentSubject.Id);
            References(studentSubject => studentSubject.StudentId).Not.Nullable().Index("IX_StudentSubject_StudentId");
            References(studentSubject => studentSubject.SubjectId).Not.Nullable().Index("IX_StudentSubject_SubjectId");
        }
    }
}
