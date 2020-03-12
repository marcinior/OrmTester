using System;

namespace OrmTesterLib.Interfaces
{
    public interface ITestOperations
    {
        TimeSpan SingleCreateWithoutRelationship();

        TimeSpan SingleCreateOneToOne();

        TimeSpan SingleCreateOneToMany();

        TimeSpan SingleCreateManyToMany();

        TimeSpan BulkCreateWithoutRelationship(int numberOfRecords);

        TimeSpan BulkCreateOneToOne(int numberOfRecords);

        TimeSpan BulkCreateOneToMany(int numberOfRecords);

        TimeSpan BulkCreateManyToMany(int numberOfRecords);

        TimeSpan SingleUpdateWithoutRelationship();

        TimeSpan SingleUpdateOneToOne();

        TimeSpan SingleUpdateOneToMany();

        TimeSpan SingleUpdateManyToMany();

        TimeSpan BulkUpdateWithoutRelationship(int numberOfRecords);

        TimeSpan BulkUpdateOneToOne(int numberOfRecords);

        TimeSpan BulkUpdateOneToMany(int numberOfRecords);

        TimeSpan BulkUpdateManyToMany(int numberOfRecords);

        TimeSpan SingleDeleteWithoutRelationship();

        TimeSpan SingleDeleteOneToOne();

        TimeSpan SingleDeleteOneToMany();

        TimeSpan SingleDeleteManyToMany();

        TimeSpan BulkDeleteWithoutRelationship(int numberOfRecords);

        TimeSpan BulkDeleteOneToOne(int numberOfRecords);

        TimeSpan BulkDeleteOneToMany(int numberOfRecords);

        TimeSpan BulkDeleteManyToMany(int numberOfRecords);

        void TruncateDatabase();
    }
}
