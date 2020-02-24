using EntityFramework;
using NHibernateTester;
using OrmTesterLib.StatisticParametersCalculator;
using OrmTesterLib.TestCore;
using System;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            TestParametersBuilder testParametersBuilder = new TestParametersBuilder();
            testParametersBuilder
                .TestSingleCreateNoRelationship(5)
                .TestSingleUpdateNoRelationship(5)
                .TestSingleDeleteNoRelationship(5)
                .TestBulkCreateNoRelationship(2)
                .TestBulkUpdateNoRelationship(2)
                .TestBulkDeleteNoRelationship(2)
                .TestSingleCreateOneToOne()
                .TestSingleUpdateOneToOne()
                .TestSingleDeleteOneToOne()
                .TestBulkCreateOneToOne(1)
                .TestBulkUpdateOneToOne(1)
                .TestBulkDeleteOneToOne(1)
                .TestSingleCreateOneToMany()
                .TestSingleUpdateOneToMany()
                .TestSingleDeleteOneToMany()
                .TestBulkCreateOneToMany(1)
                .TestBulkUpdateOneToMany(1)
                .TestBulkDeleteOneToMany(1)
                .TestSingleCreateManyToMany()
                .TestSingleUpdateManyToMany()
                .TestSingleDeleteManyToMany()
                .TestBulkCreateManyToMany(1)
                .TestBulkUpdateManyToMany(1)
                .TestBulkDeleteManyToMany(1);

            NHibernateTester.NHibernateTestOperations entityFrameworkTester = new NHibernateTestOperations(testParametersBuilder);
            var results = entityFrameworkTester.RunTests(entityFrameworkTester);
            results.ForEach(r => Console.WriteLine($"Operation: {r.OperationType} IsBulk: {r.IsBulkTest} Realationship: {r.RelationshipType} Execution Time: {r.ExecutionTime.TotalMilliseconds}"));

            StatisticParametersCalculator stat = new StatisticParametersCalculator();
            stat.CalculateStatisticParameters(results, results, null /*new System.Globalization.CultureInfo("pl-PL")*/);
            Console.ReadKey();
        }
    }
}
