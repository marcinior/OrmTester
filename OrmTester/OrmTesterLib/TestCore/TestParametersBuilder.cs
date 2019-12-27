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

        public TestParameters GetTestParameters() => this.testParameters;


        public ITestParametersBuilder TestBulkCreateManyToMany(int testRepetitions = 10)
        {
            testParameters.BulkCreateManyToMany = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkCreateNoRelationship(int testRepetitions = 10)
        {
            testParameters.BulkCreateNoRelationship = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkCreateOneToMany(int testRepetitions = 10)
        {
            testParameters.BulkCreateOneToMany = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkCreateOneToOne(int testRepetitions = 10)
        {
            testParameters.BulkCreateOneToOne = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkDeleteManyToMany(int testRepetitions = 10)
        {
            testParameters.BulkDeleteManyToMany = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkDeleteNoRelationship(int testRepetitions = 10)
        {
            testParameters.BulkDeleteNoRelationship = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkDeleteOneToMany(int testRepetitions = 10)
        {
            testParameters.BulkDeleteOneToMany = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkDeleteOneToOne(int testRepetitions = 10)
        {
            testParameters.BulkDeleteOneToOne = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkUpdateManyToMany(int testRepetitions = 10)
        {
            testParameters.BulkUpdateManyToMany = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkUpdateNoRelationship(int testRepetitions = 10)
        {
            testParameters.BulkUpdateNoRelationship = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkUpdateOneToOne(int testRepetitions = 10)
        {
            testParameters.BulkUpdateOneToOne = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkUpdayeOneToMany(int testRepetitions = 10)
        {
            testParameters.BulkUpdateOneToMany = (true, testRepetitions).ToTuple();
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
