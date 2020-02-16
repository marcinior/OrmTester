using FluentNHibernate.Mapping;

namespace NHibernateTester.Entities
{
    class SubjectMapper : ClassMap<Subject>
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
            HasMany(subject => subject.StudentSubject).Inverse();
        }
    }
}
