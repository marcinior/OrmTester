namespace OrmTesterLib.TestCore
{
    public interface ITestParametersBuilder
    {
        ITestParametersBuilder TestSingleCreateNoRelationship();

        ITestParametersBuilder TestSingleUpdateNoRelationship();

        ITestParametersBuilder TestSingleDeleteNoRelationship();

        ITestParametersBuilder TestBulkCreateNoRelationship(int testRepetitions = 10);

        ITestParametersBuilder TestBulkUpdateNoRelationship(int testRepetitions = 10);

        ITestParametersBuilder TestBulkDeleteNoRelationship(int testRepetitions = 10);

        ITestParametersBuilder TestSingleCreateOneToOne();

        ITestParametersBuilder TestSingleUpdateOneToOne();

        ITestParametersBuilder TestSingleDeleteOneToOne();

        ITestParametersBuilder TestBulkCreateOneToOne(int testRepetitions = 10);

        ITestParametersBuilder TestBulkUpdateOneToOne(int testRepetitions = 10);

        ITestParametersBuilder TestBulkDeleteOneToOne(int testRepetitions = 10);

        ITestParametersBuilder TestSingleCreateOneToMany();

        ITestParametersBuilder TestSingleUpdateOneToMany();

        ITestParametersBuilder TestSingleDeleteOneToMany();

        ITestParametersBuilder TestBulkCreateOneToMany(int testRepetitions = 10);

        ITestParametersBuilder TestBulkUpdayeOneToMany(int testRepetitions = 10);

        ITestParametersBuilder TestBulkDeleteOneToMany(int testRepetitions = 10);

        ITestParametersBuilder TestSingleCreateManyToMany();

        ITestParametersBuilder TestSingleUpdateManyToMany();

        ITestParametersBuilder TestSingleDeleteManyToMany();

        ITestParametersBuilder TestBulkCreateManyToMany(int testRepetitions = 10);

        ITestParametersBuilder TestBulkUpdateManyToMany(int testRepetitions = 10);

        ITestParametersBuilder TestBulkDeleteManyToMany(int testRepetitions = 10);
    }
}
