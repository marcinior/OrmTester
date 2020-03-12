namespace OrmTesterLib.Interfaces
{
    public interface ITestParametersBuilder
    {

        ITestParametersBuilder TestBulkCreateNoRelationship(int numberOfRecords);

        ITestParametersBuilder TestBulkUpdateNoRelationship(int numberOfRecords);

        ITestParametersBuilder TestBulkDeleteNoRelationship(int numberOfRecords);

        ITestParametersBuilder TestBulkCreateOneToOne(int numberOfRecords);

        ITestParametersBuilder TestBulkUpdateOneToOne(int numberOfRecords);

        ITestParametersBuilder TestBulkDeleteOneToOne(int numberOfRecords);

        ITestParametersBuilder TestBulkCreateOneToMany(int numberOfRecords);

        ITestParametersBuilder TestBulkUpdateOneToMany(int numberOfRecords);

        ITestParametersBuilder TestBulkDeleteOneToMany(int numberOfRecords);

        ITestParametersBuilder TestBulkCreateManyToMany(int numberOfRecords);

        ITestParametersBuilder TestBulkUpdateManyToMany(int numberOfRecords);

        ITestParametersBuilder TestBulkDeleteManyToMany(int numberOfRecords);

        ITestParametersBuilder TestSingleCreateNoRelationship(int testRepetitions);

        ITestParametersBuilder TestSingleUpdateNoRelationship(int testRepetitions);

        ITestParametersBuilder TestSingleDeleteNoRelationship(int testRepetitions);

        ITestParametersBuilder TestSingleCreateOneToOne(int testRepetitions);

        ITestParametersBuilder TestSingleUpdateOneToOne(int testRepetitions);

        ITestParametersBuilder TestSingleDeleteOneToOne(int testRepetitions);

        ITestParametersBuilder TestSingleCreateOneToMany(int testRepetitions);

        ITestParametersBuilder TestSingleUpdateOneToMany(int testRepetitions);

        ITestParametersBuilder TestSingleDeleteOneToMany(int testRepetitions);

        ITestParametersBuilder TestSingleCreateManyToMany(int testRepetitions);

        ITestParametersBuilder TestSingleUpdateManyToMany(int testRepetitions);

        ITestParametersBuilder TestSingleDeleteManyToMany(int testRepetitions);
    }
}
