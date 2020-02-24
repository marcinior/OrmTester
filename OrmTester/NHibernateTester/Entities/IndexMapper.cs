using FluentNHibernate.Mapping;

namespace NHibernateTester.Entities
{
    class IndexMapper : ClassMap<Index>
    {
        public IndexMapper()
        {
            Id(index => index.IndexId);
            Map(index => index.IndexNumber).Not.Nullable().Length(7);
            Map(index => index.CreatedAt).Not.Nullable();
            Map(index => index.UpdatedAt).Not.Nullable();
            References(index => index.Student).Cascade.Delete();
        }
    }
}
