using EntityFramework;
using NHibernateTester;
using OrmTesterLib.StatisticParametersCalculator;
using OrmTesterLib.TestCore;
using System;
using System.Globalization;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            TestParametersBuilder testParametersBuilder = new TestParametersBuilder();
            testParametersBuilder
                .TestSingleCreateNoRelationship(10)
                .TestSingleUpdateNoRelationship(10)
                .TestSingleDeleteNoRelationship(10)
                .TestBulkCreateNoRelationship(10)
                .TestBulkUpdateNoRelationship(10)
                .TestBulkDeleteNoRelationship(10)
                .TestSingleCreateOneToOne(10)
                .TestSingleUpdateOneToOne(10)
                .TestSingleDeleteOneToOne(10)
                .TestBulkCreateOneToOne(10)
                .TestBulkUpdateOneToOne(10)
                .TestBulkDeleteOneToOne(10)
                .TestSingleCreateOneToMany(10)
                .TestSingleUpdateOneToMany(10)
                .TestSingleDeleteOneToMany(10)
                .TestBulkCreateOneToMany(10)
                .TestBulkUpdateOneToMany(10)
                .TestBulkDeleteOneToMany(10)
                .TestSingleCreateManyToMany(10)
                .TestSingleUpdateManyToMany(10)
                .TestSingleDeleteManyToMany(10)
                .TestBulkCreateManyToMany(10)
                .TestBulkUpdateManyToMany(10)
                .TestBulkDeleteManyToMany(10);

            EntityFrameworkTester entityFrameworkTester = new EntityFrameworkTester(testParametersBuilder);
            var results = entityFrameworkTester.RunTests(entityFrameworkTester);
            results.ForEach(r => Console.WriteLine($"Operation: {r.OperationType} IsBulk: {r.IsBulkTest} Realationship: {r.RelationshipType} Execution Time: {r.ExecutionTime.TotalMilliseconds}"));

            StatisticParametersCalculator stat = new StatisticParametersCalculator(CultureInfo.CurrentCulture);
            stat.CalculateStatisticParameters(results, results);
            Console.ReadKey();
        }
    }
}
