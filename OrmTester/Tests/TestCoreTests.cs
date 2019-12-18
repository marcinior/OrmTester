using NUnit.Framework;
using OrmTesterLib.TestCore;
using TestParameters = OrmTesterLib.TestCore.TestParameters;

namespace Tests
{
    class TestCoreTests
    {
        [Test]
        public void TestParametersBulder_CreateBuilder()
        {
            TestParametersBuilder testParametersBuilder = new TestParametersBuilder();
            testParametersBuilder
                .TestBulkCreateManyToMany(15)
                .TestSingleCreateOneToOne()
                .TestBulkDeleteOneToOne();

            TestParameters testParameters = testParametersBuilder.GetTestParameters();

            Assert.AreEqual(true, testParameters.BulkCreateManyToMany.Item1);
            Assert.AreEqual(15, testParameters.BulkCreateManyToMany.Item2);
            Assert.AreEqual(true, testParameters.SingleCreateOneToOne);
            Assert.AreEqual(true, testParameters.BulkDeleteOneToOne.Item1);
            Assert.AreEqual(10, testParameters.BulkDeleteOneToOne.Item2);
        }
    }
}
