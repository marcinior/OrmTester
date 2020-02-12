namespace OrmTesterLib.Interfaces
{
    public interface ITestParametersBuilder
    {
        ITestParametersBuilder TestSingleCreateNoRelationship(int testRepetitions = 10);

        ITestParametersBuilder TestSingleUpdateNoRelationship(int testRepetitions = 10);

        ITestParametersBuilder TestSingleDeleteNoRelationship(int testRepetitions = 10);

        ITestParametersBuilder TestBulkCreateNoRelationship(int testRepetitions = 10);

        ITestParametersBuilder TestBulkUpdateNoRelationship(int testRepetitions = 10);

        ITestParametersBuilder TestBulkDeleteNoRelationship(int testRepetitions = 10);

        ITestParametersBuilder TestSingleCreateOneToOne(int testRepetitions = 10);

        ITestParametersBuilder TestSingleUpdateOneToOne(int testRepetitions = 10);

        ITestParametersBuilder TestSingleDeleteOneToOne(int testRepetitions = 10);

        ITestParametersBuilder TestBulkCreateOneToOne(int testRepetitions = 10);

        ITestParametersBuilder TestBulkUpdateOneToOne(int testRepetitions = 10);

        ITestParametersBuilder TestBulkDeleteOneToOne(int testRepetitions = 10);

        ITestParametersBuilder TestSingleCreateOneToMany(int testRepetitions = 10);

        ITestParametersBuilder TestSingleUpdateOneToMany(int testRepetitions = 10);

        ITestParametersBuilder TestSingleDeleteOneToMany(int testRepetitions = 10);

        ITestParametersBuilder TestBulkCreateOneToMany(int testRepetitions = 10);

        ITestParametersBuilder TestBulkUpdateOneToMany(int testRepetitions = 10);

        ITestParametersBuilder TestBulkDeleteOneToMany(int testRepetitions = 10);

        ITestParametersBuilder TestSingleCreateManyToMany(int testRepetitions = 10);

        ITestParametersBuilder TestSingleUpdateManyToMany(int testRepetitions = 10);

        ITestParametersBuilder TestSingleDeleteManyToMany(int testRepetitions = 10);

        ITestParametersBuilder TestBulkCreateManyToMany(int testRepetitions = 10);

        ITestParametersBuilder TestBulkUpdateManyToMany(int testRepetitions = 10);

        ITestParametersBuilder TestBulkDeleteManyToMany(int testRepetitions = 10);
    }
}
