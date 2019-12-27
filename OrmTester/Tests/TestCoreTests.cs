using FluentAssertions;
using Moq;
using NUnit.Framework;
using OrmTesterLib.Enums;
using OrmTesterLib.Interfaces;
using OrmTesterLib.TestCore;
using System;
using TestParameters = OrmTesterLib.TestCore.TestParameters;

namespace Tests
{
    class TestCoreTests
    {
        [Test]
        public void TestParametersBuilder_CreateBuilder()
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

        [Test]
        public void BaseTesterTest()
        {
            Mock<ITestOperations> sampleOrmImplementation = new Mock<ITestOperations>();
            sampleOrmImplementation
                .Setup(o => o.SingleCreateWithoutRelationship())
                .Returns(new TimeSpan(0, 0, 0, 0, 120));

            sampleOrmImplementation
                .Setup(o => o.BulkUpdateOneToOne())
                .Returns(new TimeSpan(0, 0, 0, 0, 150));

            TestParametersBuilder testParametersBuilder = new TestParametersBuilder();
            testParametersBuilder
                .TestSingleCreateNoRelationship(2)
                .TestBulkUpdateOneToOne(3);

            BaseTester baseTester = new BaseTester(testParametersBuilder);

            var results = baseTester.RunTests(sampleOrmImplementation.Object);

            results.Should().HaveCount(5)
                .And.Contain(tr => tr.IsBulkTest == false && tr.OperationType == OperationType.Create && tr.RelationshipType == RelationshipType.None && tr.ExecutionTime.TotalMilliseconds == 120)
                .And.Contain(tr => tr.IsBulkTest == true && tr.OperationType == OperationType.Update && tr.RelationshipType == RelationshipType.OneToOne && tr.ExecutionTime.TotalMilliseconds == 150);
        }
    }
}
