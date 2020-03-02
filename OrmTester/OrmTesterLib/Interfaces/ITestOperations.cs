using System;

namespace OrmTesterLib.Interfaces
{
    public interface ITestOperations
    {
        TimeSpan SingleCreateWithoutRelationship();

        TimeSpan SingleCreateOneToOne();

        TimeSpan SingleCreateOneToMany();

        TimeSpan SingleCreateManyToMany();

        TimeSpan BulkCreateWithoutRelationship();

        TimeSpan BulkCreateOneToOne();

        TimeSpan BulkCreateOneToMany();

        TimeSpan BulkCreateManyToMany();

        TimeSpan SingleUpdateWithoutRelationship();

        TimeSpan SingleUpdateOneToOne();

        TimeSpan SingleUpdateOneToMany();

        TimeSpan SingleUpdateManyToMany();

        TimeSpan BulkUpdateWithoutRelationship();

        TimeSpan BulkUpdateOneToOne();

        TimeSpan BulkUpdateOneToMany();

        TimeSpan BulkUpdateManyToMany();

        TimeSpan SingleDeleteWithoutRelationship();

        TimeSpan SingleDeleteOneToOne();

        TimeSpan SingleDeleteOneToMany();

        TimeSpan SingleDeleteManyToMany();

        TimeSpan BulkDeleteWithoutRelationship();

        TimeSpan BulkDeleteOneToOne();

        TimeSpan BulkDeleteOneToMany();

        TimeSpan BulkDeleteManyToMany();

        void TruncateDatabase();
    }
}
