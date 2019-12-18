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
            testParameters.BulkUpdateOneToMany = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestBulkUpdayeOneToMany(int testRepetitions = 10)
        {
            testParameters.BulkUpdateOneToOne = (true, testRepetitions).ToTuple();
            return this;
        }

        public ITestParametersBuilder TestSingleCreateManyToMany()
        {
            testParameters.SingleCreateManyToMany = true;
            return this;
        }

        public ITestParametersBuilder TestSingleCreateNoRelationship()
        {
            testParameters.SingleCreateNoRelationship = true;
            return this;
        }

        public ITestParametersBuilder TestSingleCreateOneToMany()
        {
            testParameters.SingleCreateOneToMany = true;
            return this;
        }

        public ITestParametersBuilder TestSingleCreateOneToOne()
        {
            testParameters.SingleCreateOneToOne = true;
            return this;
        }

        public ITestParametersBuilder TestSingleDeleteManyToMany()
        {
            testParameters.SingleDeleteManyToMany = true;
            return this;
        }

        public ITestParametersBuilder TestSingleDeleteNoRelationship()
        {
            testParameters.SingleDeleteNoRelationship = true;
            return this;
        }

        public ITestParametersBuilder TestSingleDeleteOneToMany()
        {
            testParameters.SingleDeleteOneToMany = true;
            return this;
        }

        public ITestParametersBuilder TestSingleDeleteOneToOne()
        {
            testParameters.SingleDeleteOneToOne = true;
            return this;
        }

        public ITestParametersBuilder TestSingleUpdateManyToMany()
        {
            testParameters.SingleUpdateManyToMany = true;
            return this;
        }

        public ITestParametersBuilder TestSingleUpdateNoRelationship()
        {
            testParameters.SingleUpdateNoRelationship = true;
            return this;
        }

        public ITestParametersBuilder TestSingleUpdateOneToMany()
        {
            testParameters.SingleUpdateOneToMany = true;
            return this;
        }

        public ITestParametersBuilder TestSingleUpdateOneToOne()
        {
            testParameters.SingleUpdateOneToOne = true;
            return this;
        }
    }
}
