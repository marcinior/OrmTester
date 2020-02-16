using FluentNHibernate.Mapping;

namespace NHibernateTester.Entities
{
    class StudentMapper : ClassMap<Student>
    {
        public StudentMapper()
        {
            Id(student => student.StudentId);
            Map(student => student.BirthDate).Not.Nullable();
            References(student => student.ClassId);
            Map(student => student.FirstName).Length(20).Not.Nullable();
            Map(student => student.Gender).Not.Nullable();
            References(student => student.IndexId);
            Map(student => student.LastName).Length(45).Not.Nullable();
            Map(student => student.Pesel).Length(11).Nullable();
            Map(student => student.CreatedAt).Not.Nullable();
            Map(student => student.UpdatedAt).Not.Nullable();
            HasMany(student => student.StudentSubject).Inverse();
        }
    }
}
