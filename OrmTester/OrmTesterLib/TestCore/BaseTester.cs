using OrmTesterLib.Enums;
using OrmTesterLib.Interfaces;
using System;
using System.Collections.Generic;

namespace OrmTesterLib.TestCore
{
    public abstract class BaseTester
    {
        private readonly TestParameters testParameters;
        private ITestOperations testOperations;

        protected BaseTester(TestParameters testParameters)
        {
            this.testParameters = testParameters ?? throw new ArgumentNullException("TestParameters object is necessary to run tests");
        }

        public List<TestResult> RunTests(ITestOperations testOperations)
        {
            this.testOperations = testOperations;
            List<TestResult> testResults = new List<TestResult>();

            if (testParameters.SingleCreateNoRelationship?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteSingleTests(OperationType.Create, RelationshipType.None, testParameters.SingleCreateNoRelationship.Item2, testOperations.SingleCreateWithoutRelationship));
            }

            if (testParameters.SingleCreateOneToOne?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteSingleTests(OperationType.Create, RelationshipType.OneToOne, testParameters.SingleCreateOneToOne.Item2, testOperations.SingleCreateOneToOne));
            }

            if (testParameters.SingleCreateOneToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteSingleTests(OperationType.Create, RelationshipType.OneToMany, testParameters.SingleCreateOneToMany.Item2, testOperations.SingleCreateOneToMany));
            }

            if (testParameters.SingleCreateManyToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteSingleTests(OperationType.Create, RelationshipType.ManyToMany, testParameters.SingleCreateManyToMany.Item2, testOperations.SingleCreateManyToMany));
            }

            if (testParameters.SingleUpdateNoRelationship?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteSingleTests(OperationType.Update, RelationshipType.None, testParameters.SingleUpdateNoRelationship.Item2, testOperations.SingleUpdateWithoutRelationship));
            }

            if (testParameters.SingleUpdateOneToOne?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteSingleTests(OperationType.Update, RelationshipType.OneToOne, testParameters.SingleUpdateOneToOne.Item2, testOperations.SingleUpdateOneToOne));
            }

            if (testParameters.SingleUpdateOneToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteSingleTests(OperationType.Update, RelationshipType.OneToMany, testParameters.SingleUpdateOneToMany.Item2, testOperations.SingleUpdateOneToMany));
            }

            if (testParameters.SingleUpdateManyToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteSingleTests(OperationType.Update, RelationshipType.ManyToMany, testParameters.SingleUpdateManyToMany.Item2, testOperations.SingleUpdateManyToMany));
            }

            if (testParameters.SingleDeleteNoRelationship?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteSingleTests(OperationType.Delete, RelationshipType.None, testParameters.SingleDeleteNoRelationship.Item2, testOperations.SingleDeleteWithoutRelationship));
            }

            if (testParameters.SingleDeleteOneToOne?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteSingleTests(OperationType.Delete, RelationshipType.OneToOne, testParameters.SingleDeleteOneToOne.Item2, testOperations.SingleDeleteOneToOne));
            }

            if (testParameters.SingleDeleteOneToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteSingleTests(OperationType.Delete, RelationshipType.OneToMany, testParameters.SingleDeleteOneToMany.Item2, testOperations.SingleDeleteOneToMany));
            }

            if (testParameters.SingleDeleteManyToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteSingleTests(OperationType.Delete, RelationshipType.ManyToMany, testParameters.SingleDeleteManyToMany.Item2, testOperations.SingleDeleteManyToMany));
            }

            if (testParameters.BulkCreateNoRelationship?.Item1 == true)
            {
                testResults.Add(
                    ExecuteBulkTests(OperationType.Create, RelationshipType.None, testParameters.BulkCreateNoRelationship.Item2, testOperations.BulkCreateWithoutRelationship));
            }

            if (testParameters.BulkCreateOneToOne?.Item1 == true)
            {
                testResults.Add(
                    ExecuteBulkTests(OperationType.Create, RelationshipType.OneToOne, testParameters.BulkCreateOneToOne.Item2, testOperations.BulkCreateOneToOne));
            }

            if (testParameters.BulkCreateOneToMany?.Item1 == true)
            {
                testResults.Add(
                    ExecuteBulkTests(OperationType.Create, RelationshipType.OneToMany, testParameters.BulkCreateOneToMany.Item2, testOperations.BulkCreateOneToMany));
            }

            if (testParameters.BulkCreateManyToMany?.Item1 == true)
            {
                testResults.Add(
                    ExecuteBulkTests(OperationType.Create, RelationshipType.ManyToMany, testParameters.BulkCreateManyToMany.Item2, testOperations.BulkCreateManyToMany));
            }

            if (testParameters.BulkUpdateNoRelationship?.Item1 == true)
            {
                testResults.Add(
                    ExecuteBulkTests(OperationType.Update, RelationshipType.None, testParameters.BulkUpdateNoRelationship.Item2, testOperations.BulkUpdateWithoutRelationship));
            }

            if (testParameters.BulkUpdateOneToOne?.Item1 == true)
            {
                testResults.Add(
                    ExecuteBulkTests(OperationType.Update, RelationshipType.OneToOne, testParameters.BulkUpdateOneToOne.Item2, testOperations.BulkUpdateOneToOne));
            }

            if (testParameters.BulkUpdateOneToMany?.Item1 == true)
            {
                testResults.Add(
                    ExecuteBulkTests(OperationType.Update, RelationshipType.OneToMany, testParameters.BulkUpdateOneToMany.Item2, testOperations.BulkUpdateOneToMany));
            }

            if (testParameters.BulkUpdateManyToMany?.Item1 == true)
            {
                testResults.Add(
                    ExecuteBulkTests(OperationType.Update, RelationshipType.ManyToMany, testParameters.BulkUpdateManyToMany.Item2, testOperations.BulkUpdateManyToMany));
            }

            if (testParameters.BulkDeleteNoRelationship?.Item1 == true)
            {
                testResults.Add(
                    ExecuteBulkTests(OperationType.Delete, RelationshipType.None, testParameters.BulkDeleteNoRelationship.Item2, testOperations.BulkDeleteWithoutRelationship));
            }

            if (testParameters.BulkDeleteOneToOne?.Item1 == true)
            {
                testResults.Add(
                    ExecuteBulkTests(OperationType.Delete, RelationshipType.OneToOne, testParameters.BulkDeleteOneToOne.Item2, testOperations.BulkDeleteOneToOne));
            }

            if (testParameters.BulkDeleteOneToMany?.Item1 == true)
            {
                testResults.Add(
                    ExecuteBulkTests(OperationType.Delete, RelationshipType.OneToMany, testParameters.BulkDeleteOneToMany.Item2, testOperations.BulkDeleteOneToMany));
            }

            if (testParameters.BulkDeleteManyToMany?.Item1 == true)
            {
                testResults.Add(
                    ExecuteBulkTests(OperationType.Delete, RelationshipType.ManyToMany, testParameters.BulkDeleteManyToMany.Item2, testOperations.BulkDeleteManyToMany));
            }


            this.testOperations.TruncateDatabase();

            return testResults;
        }

        private List<TestResult> ExecuteSingleTests(OperationType operationType,
            RelationshipType relationshipType,
            int repetitions,
            Func<TimeSpan> testMethod)
        {
            List<TestResult> testResults = new List<TestResult>();

            for (int i = 0; i < repetitions; i++)
            {
                TestResult testResult = new TestResult
                {
                    OperationType = operationType,
                    RelationshipType = relationshipType,
                    IsBulkTest = false,
                    ExecutionTime = testMethod.Invoke()
                };

                testResults.Add(testResult);
            }

            return testResults;
        }

        private TestResult ExecuteBulkTests(OperationType operationType,
            RelationshipType relationshipType,
            int numberOfRecords,
            Func<int, TimeSpan> testMethod)
        {

            TestResult testResult = new TestResult
            {
                OperationType = operationType,
                RelationshipType = relationshipType,
                IsBulkTest = true,
                NumberOfRecords = numberOfRecords,
                ExecutionTime = testMethod.Invoke(numberOfRecords)
            };

            return testResult;
        }
    }
}
