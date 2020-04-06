using OrmTesterLib.Interfaces;
using System;

namespace OrmTesterLib.TestCore
{
    public class TestParametersBuilder : ITestParametersBuilder
    {
        private readonly TestParameters testParameters;

        public TestParametersBuilder()
        {
            this.testParameters = new TestParameters();
        }

        public TestParameters Build () => this.testParameters;


        public ITestParametersBuilder TestBulkCreateManyToMany(int numberOfRecords)
        {
            testParameters.BulkCreateManyToMany = (true, numberOfRecords).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkCreateNoRelationship(int numberOfRecords)
        {
            testParameters.BulkCreateNoRelationship = (true, numberOfRecords).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkCreateOneToMany(int numberOfRecords)
        {
            testParameters.BulkCreateOneToMany = (true, numberOfRecords).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkCreateOneToOne(int numberOfRecords)
        {
            testParameters.BulkCreateOneToOne = (true, numberOfRecords).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkDeleteManyToMany(int numberOfRecords)
        {
            testParameters.BulkDeleteManyToMany = (true, numberOfRecords).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkDeleteNoRelationship(int numberOfRecords)
        {
            testParameters.BulkDeleteNoRelationship = (true, numberOfRecords).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkDeleteOneToMany(int numberOfRecords)
        {
            testParameters.BulkDeleteOneToMany = (true, numberOfRecords).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkDeleteOneToOne(int numberOfRecords)
        {
            testParameters.BulkDeleteOneToOne = (true, numberOfRecords).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkUpdateManyToMany(int numberOfRecords)
        {
            testParameters.BulkUpdateManyToMany = (true, numberOfRecords).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkUpdateNoRelationship(int numberOfRecords)
        {
            testParameters.BulkUpdateNoRelationship = (true, numberOfRecords).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkUpdateOneToOne(int numberOfRecords)
        {
            testParameters.BulkUpdateOneToOne = (true, numberOfRecords).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkUpdateOneToMany(int numberOfRecords)
        {
            testParameters.BulkUpdateOneToMany = (true, numberOfRecords).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestSingleCreateManyToMany(int testRepetitions = 10)
        {
            testParameters.SingleCreateManyToMany = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestSingleCreateNoRelationship(int testRepetitions = 10)
        {
            testParameters.SingleCreateNoRelationship = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestSingleCreateOneToMany(int testRepetitions = 10)
        {
            testParameters.SingleCreateOneToMany = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestSingleCreateOneToOne(int testRepetitions = 10)
        {
            testParameters.SingleCreateOneToOne = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestSingleDeleteManyToMany(int testRepetitions = 10)
        {
            testParameters.SingleDeleteManyToMany = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestSingleDeleteNoRelationship(int testRepetitions = 10)
        {
            testParameters.SingleDeleteNoRelationship = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestSingleDeleteOneToMany(int testRepetitions = 10)
        {
            testParameters.SingleDeleteOneToMany = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestSingleDeleteOneToOne(int testRepetitions = 10)
        {
            testParameters.SingleDeleteOneToOne = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestSingleUpdateManyToMany(int testRepetitions = 10)
        {
            testParameters.SingleUpdateManyToMany = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestSingleUpdateNoRelationship(int testRepetitions = 10)
        {
            testParameters.SingleUpdateNoRelationship = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestSingleUpdateOneToMany(int testRepetitions = 10)
        {
            testParameters.SingleUpdateOneToMany = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestSingleUpdateOneToOne(int testRepetitions = 10)
        {
            testParameters.SingleUpdateOneToOne = (true, testRepetitions).ToTuple();
            return this;
        }
    }
}
