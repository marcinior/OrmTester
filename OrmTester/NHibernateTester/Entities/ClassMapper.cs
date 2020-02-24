using FluentNHibernate.Mapping;

namespace NHibernateTester.Entities
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
            HasMany(classObject => classObject.Student).Cascade.Delete();
        }

    }
}
