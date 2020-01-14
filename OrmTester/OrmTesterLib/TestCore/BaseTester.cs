using OrmTesterLib.Enums;
using OrmTesterLib.Interfaces;
using System;
using System.Collections.Generic;

namespace OrmTesterLib.TestCore
{
    public class BaseTester
    {
        private readonly TestParametersBuilder testParametersBuilder;
        private readonly TestParameters testParameters;

        protected BaseTester(TestParametersBuilder testParametersBuilder)
        {
            this.testParametersBuilder = testParametersBuilder ?? throw new ArgumentNullException("TestParametersBuilder is necessary to run tests");
            testParameters = this.testParametersBuilder.GetTestParameters();
        }

        public List<TestResult> RunTests(ITestOperations testOperations)
        {
            List<TestResult> testResults = new List<TestResult>();

            if (testParameters.SingleCreateNoRelationship?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Create, RelationshipType.None, testParameters.SingleCreateNoRelationship.Item2, testOperations.SingleCreateWithoutRelationship));
            }

            if (testParameters.SingleCreateOneToOne?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Create, RelationshipType.OneToOne, testParameters.SingleCreateOneToOne.Item2, testOperations.SingleCreateOneToOne));
            }

            if (testParameters.SingleCreateOneToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Create, RelationshipType.OneToMany, testParameters.SingleCreateOneToMany.Item2, testOperations.SingleCreateOneToMany));
            }

            if (testParameters.SingleCreateManyToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Create, RelationshipType.ManyToMany, testParameters.SingleCreateManyToMany.Item2, testOperations.SingleCreateManyToMany));
            }

            if (testParameters.SingleUpdateNoRelationship?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Update, RelationshipType.None, testParameters.SingleUpdateNoRelationship.Item2, testOperations.SingleUpdateWithoutRelationship));
            }

            if (testParameters.SingleUpdateOneToOne?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Update, RelationshipType.OneToOne, testParameters.SingleUpdateOneToOne.Item2, testOperations.SingleUpdateOneToOne));
            }

            if (testParameters.SingleUpdateOneToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Update, RelationshipType.OneToMany, testParameters.SingleUpdateOneToMany.Item2, testOperations.SingleUpdateOneToMany));
            }

            if (testParameters.SingleUpdateManyToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Update, RelationshipType.ManyToMany, testParameters.SingleUpdateManyToMany.Item2, testOperations.SingleUpdateManyToMany));
            }

            if (testParameters.SingleDeleteNoRelationship?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Delete, RelationshipType.None, testParameters.SingleDeleteNoRelationship.Item2, testOperations.SingleDeleteWithoutRelationship));
            }

            if (testParameters.SingleDeleteOneToOne?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Delete, RelationshipType.OneToOne, testParameters.SingleDeleteOneToOne.Item2, testOperations.SingleDeleteOneToOne));
            }

            if (testParameters.SingleDeleteOneToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Delete, RelationshipType.OneToMany, testParameters.SingleDeleteOneToMany.Item2, testOperations.SingleDeleteOneToMany));
            }

            if (testParameters.SingleDeleteManyToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Delete, RelationshipType.ManyToMany, testParameters.SingleDeleteManyToMany.Item2, testOperations.BulkDeleteManyToMany));
            }

            if (testParameters.BulkCreateNoRelationship?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Create, RelationshipType.None, testParameters.BulkCreateNoRelationship.Item2, testOperations.BulkCreateWithoutRelationship, true));
            }

            if (testParameters.BulkCreateOneToOne?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Create, RelationshipType.OneToOne, testParameters.BulkCreateOneToOne.Item2, testOperations.BulkCreateOneToOne, true));
            }

            if (testParameters.BulkCreateOneToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Create, RelationshipType.OneToMany, testParameters.BulkCreateOneToMany.Item2, testOperations.BulkCreateOneToMany, true));
            }

            if (testParameters.BulkCreateManyToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Create, RelationshipType.ManyToMany, testParameters.BulkCreateManyToMany.Item2, testOperations.BulkCreateManyToMany, true));
            }

            if (testParameters.BulkUpdateNoRelationship?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Update, RelationshipType.None, testParameters.BulkUpdateNoRelationship.Item2, testOperations.BulkUpdateWithoutRelationship, true));
            }

            if (testParameters.BulkUpdateOneToOne?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Update, RelationshipType.OneToOne, testParameters.BulkUpdateOneToOne.Item2, testOperations.BulkUpdateOneToOne, true));
            }

            if (testParameters.BulkUpdateOneToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Update, RelationshipType.OneToMany, testParameters.BulkUpdateOneToMany.Item2, testOperations.BulkUpdateOneToMany, true));
            }

            if (testParameters.BulkUpdateManyToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Update, RelationshipType.ManyToMany, testParameters.BulkUpdateManyToMany.Item2, testOperations.BulkUpdateManyToMany, true));
            }

            if (testParameters.BulkDeleteNoRelationship?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Delete, RelationshipType.None, testParameters.BulkDeleteNoRelationship.Item2, testOperations.BulkDeleteNoRelationship, true));
            }

            if (testParameters.BulkDeleteOneToOne?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Delete, RelationshipType.OneToOne, testParameters.BulkDeleteOneToOne.Item2, testOperations.BulkDeleteOneToOne, true));
            }

            if (testParameters.BulkDeleteOneToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Delete, RelationshipType.OneToMany, testParameters.BulkDeleteOneToMany.Item2, testOperations.BulkDeleteOneToMany, true));
            }

            if (testParameters.BulkDeleteManyToMany?.Item1 == true)
            {
                testResults.AddRange(
                    ExecuteMultipleTests(OperationType.Delete, RelationshipType.ManyToMany, testParameters.BulkDeleteManyToMany.Item2, testOperations.BulkDeleteManyToMany, true));
            }

            return testResults;
        }

        private List<TestResult> ExecuteMultipleTests(OperationType operationType, RelationshipType relationshipType, int repetitions, Func<TimeSpan> testMethod, bool isBulkTest = false)
        {
            List<TestResult> testResults = new List<TestResult>();

            for (int i = 0; i < repetitions; i++)
            {
                TestResult testResult = new TestResult
                {
                    OperationType = operationType,
                    RelationshipType = relationshipType,
                    IsBulkTest = isBulkTest,
                    ExecutionTime = testMethod.Invoke()
                };

                testResults.Add(testResult);
            }

            return testResults;
        }
    }
}
