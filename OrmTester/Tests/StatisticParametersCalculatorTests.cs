using FluentAssertions;
using NUnit.Framework;
using OrmTesterLib.Enums;
using OrmTesterLib.StatisticParametersCalculator;
using OrmTesterLib.TestCore;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Tests
{
    /*ExecutonTimes represents sample operation execution time and are base to test statistic parameters calculation corectness*/
    class StatisticParametersCalculatorTests
    {
        private List<TestResult> integerTestResults;
        private List<TestResult> doubleTestResults;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            integerTestResults = new List<TestResult>
            {
                new TestResult()
                {
                    OperationType = OperationType.Create,
                    IsBulkTest = false,
                    RelationshipType = RelationshipType.OneToOne,
                    ExecutionTime = TimeSpan.FromMilliseconds(2)
                },
                 new TestResult()
                {
                    OperationType = OperationType.Create,
                    IsBulkTest = false,
                    RelationshipType = RelationshipType.OneToOne,
                    ExecutionTime = TimeSpan.FromMilliseconds(3)
                },
                  new TestResult()
                {
                    OperationType = OperationType.Create,
                    IsBulkTest = false,
                    RelationshipType = RelationshipType.OneToOne,
                    ExecutionTime = TimeSpan.FromMilliseconds(5)
                }
            };

            doubleTestResults = new List<TestResult>
            {
                new TestResult()
                {
                    OperationType = OperationType.Update,
                    IsBulkTest = true,
                    RelationshipType = RelationshipType.OneToMany,
                    ExecutionTime = TimeSpan.FromMilliseconds(2).AddMicroseconds(7000)
                },
                 new TestResult()
                {
                    OperationType = OperationType.Update,
                    IsBulkTest = true,
                    RelationshipType = RelationshipType.OneToMany,
                    ExecutionTime = TimeSpan.FromMilliseconds(3).AddMicroseconds(2500)
                },
                  new TestResult()
                {
                    OperationType = OperationType.Update,
                    IsBulkTest = true,
                    RelationshipType = RelationshipType.OneToMany,
                    ExecutionTime = TimeSpan.FromMilliseconds(5).AddMicroseconds(1700)
                }
            };
        }

        [Test]
        public void CalculateStatisticParameters_AverageTest_IntegerResults()
        {
            StatisticParametersCalculator calculator = new StatisticParametersCalculator(CultureInfo.CurrentCulture);
            double actual = TestHelper.InvokePrivateMethod(calculator, "CalculateAverage", new object[] { integerTestResults });
            double expected = 3.33;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalculateStatisticParameters_AverageTest_DoubleResults()
        {
            StatisticParametersCalculator calculator = new StatisticParametersCalculator(CultureInfo.CurrentCulture);
            double actual = TestHelper.InvokePrivateMethod(calculator, "CalculateAverage", new object[] { doubleTestResults });
            double expected = 3.71;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(4, 3, 1)]
        [TestCase(4, 5, 1)]
        [TestCase(2.32, 2.32, 0)]
        [TestCase(11.32, 11.39, 0.07)]
        public void CalculateStatisticParameters_DifferenceTest(double average1, double average2, double expected)
        {
            StatisticParametersCalculator calculator = new StatisticParametersCalculator(CultureInfo.CurrentCulture);
            double actual = TestHelper.InvokePrivateMethod(calculator, "CalculateDifference", new object[] { average1, average2 });
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalculateStatisticParameters_StandardDeviationTest_IntegerResults()
        {
            StatisticParametersCalculator calculator = new StatisticParametersCalculator(CultureInfo.CurrentCulture);
            double actual = TestHelper.InvokePrivateMethod(calculator, "CalculateStandardDeviation", new object[] { integerTestResults, 3.33 });
            double expected = 1.25;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalculateStatisticParameters_StandardDeviationTest_DoubleResults()
        {
            StatisticParametersCalculator calculator = new StatisticParametersCalculator(CultureInfo.CurrentCulture);
            double actual = TestHelper.InvokePrivateMethod(calculator, "CalculateStandardDeviation", new object[] { doubleTestResults, 3.71 });
            double expected = 1.06;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalculateStatisticParameters_CoefficentOfVariationt_SingleResults()
        {
            StatisticParametersCalculator calculator = new StatisticParametersCalculator(CultureInfo.CurrentCulture);
            double actual = TestHelper.InvokePrivateMethod(calculator, "CalculateCoefficentOfVariation", new object[] { 1.25, 3.33 });
            double expected = 37.54;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalculateStatisticParameters_CoefficentOfVariationt_DoubleResults()
        {
            StatisticParametersCalculator calculator = new StatisticParametersCalculator(CultureInfo.CurrentCulture);
            double actual = TestHelper.InvokePrivateMethod(calculator, "CalculateCoefficentOfVariation", new object[] { 1.06, 3.71 });
            double expected = 28.57;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalculateStatisticParameters_CalculateStatisticsTest()
        {
            StatisticParametersCalculator calculator = new StatisticParametersCalculator(CultureInfo.CurrentCulture);
            var efTestResults = new List<TestResult>(integerTestResults);
            efTestResults.Add(
                new TestResult()
                {
                    OperationType = OperationType.Update,
                    IsBulkTest = true,
                    RelationshipType = RelationshipType.OneToMany,
                    ExecutionTime = TimeSpan.FromMilliseconds(2).AddMicroseconds(7000),
                    NumberOfRecords = 11
                });

            var nHibernateTestResults = new List<TestResult>()
            {
                new TestResult()
                {
                    OperationType = OperationType.Update,
                    IsBulkTest = true,
                    RelationshipType = RelationshipType.OneToMany,
                    ExecutionTime = TimeSpan.FromMilliseconds(2).AddMicroseconds(1100),
                    NumberOfRecords = 11
                },
                new TestResult()
                {
                    OperationType = OperationType.Create,
                    IsBulkTest = false,
                    RelationshipType = RelationshipType.OneToOne,
                    ExecutionTime = TimeSpan.FromMilliseconds(4)
                },
                new TestResult()
                {
                    OperationType = OperationType.Create,
                    IsBulkTest = false,
                    RelationshipType = RelationshipType.OneToOne,
                    ExecutionTime = TimeSpan.FromMilliseconds(4)
                },
                new TestResult()
                {
                    OperationType = OperationType.Create,
                    IsBulkTest = false,
                    RelationshipType = RelationshipType.OneToOne,
                    ExecutionTime = TimeSpan.FromMilliseconds(3)
                }
            };

            List<StatisticParameter> results = calculator.CalculateStatisticParameters(efTestResults, nHibernateTestResults);
            results.Should().ContainSingle(sp => sp.EfAverage == 3.33 &&
                                            sp.EfStandardDeviation == 1.25 &&
                                            sp.EfCoefficentOfVariation == 37.54 &&
                                            sp.NHibernateAverage == 3.67 &&
                                            sp.NHibernateStandardDeviation == 0.47 &&
                                            sp.NHibernateCoefficentOfVariation == 12.81 &&
                                            sp.Difference == 0.34 &&
                                            sp.TestName == "Single Create 1:1")
                .And.ContainSingle(sp => sp.EfAverage == 2.7 &&
                                            sp.EfStandardDeviation == 0 &&
                                            sp.EfCoefficentOfVariation == 0 &&
                                            sp.EfExecutionTimePerRecord == 0.25 &&
                                            sp.NHibernateAverage == 2.11 &&
                                            sp.NHibernateStandardDeviation == 0 &&
                                            sp.NHibernateCoefficentOfVariation == 0 &&
                                            sp.Difference == 0.59 &&
                                            sp.NHibernateExecutionTimePerRecord == 0.19 &&
                                            sp.TestName == "Bulk Update 1:N");
        }
    }
}
