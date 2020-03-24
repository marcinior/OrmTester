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
            double expected = 3.333;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalculateStatisticParameters_AverageTest_DoubleResults()
        {
            StatisticParametersCalculator calculator = new StatisticParametersCalculator(CultureInfo.CurrentCulture);
            double actual = TestHelper.InvokePrivateMethod(calculator, "CalculateAverage", new object[] { doubleTestResults });
            double expected = 3.707;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(4, 3, 1)]
        [TestCase(4, 5, 1)]
        [TestCase(2.32, 2.32, 0)]
        [TestCase(11.325, 11.392, 0.067)]
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
            double actual = TestHelper.InvokePrivateMethod(calculator, "CalculateStandardDeviation", new object[] { integerTestResults, 3.334 });
            double expected = 1.25;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalculateStatisticParameters_StandardDeviationTest_DoubleResults()
        {
            StatisticParametersCalculator calculator = new StatisticParametersCalculator(CultureInfo.CurrentCulture);
            double actual = TestHelper.InvokePrivateMethod(calculator, "CalculateStandardDeviation", new object[] { doubleTestResults, 3.707 });
            double expected = 1.06;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalculateStatisticParameters_CoefficentOfVariationt_SingleResults()
        {
            StatisticParametersCalculator calculator = new StatisticParametersCalculator(CultureInfo.CurrentCulture);
            double actual = TestHelper.InvokePrivateMethod(calculator, "CalculateCoefficentOfVariation", new object[] { 1.25, 3.334 });
            double expected = 37.49;
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
                    ExecutionTime = TimeSpan.FromMilliseconds(2).AddMicroseconds(7110),
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
            results.Should().ContainSingle(sp => sp.EfAverage == 3.333 &&
                                            sp.EfStandardDeviation == 1.25 &&
                                            sp.EfCoefficentOfVariation == 37.5 &&
                                            sp.EfExecutionTimePerRecord == 3.333 &&
                                            sp.NHibernateAverage == 3.667 &&
                                            sp.NHibernateStandardDeviation == 0.47 &&
                                            sp.NHibernateCoefficentOfVariation == 12.82 &&
                                            sp.NHibernateExecutionTimePerRecord == 3.667 &&
                                            sp.Difference == 0.334 &&
                                            sp.NumberOfRecords == 1 &&
                                            sp.DifferenceBetweenTimePerRecord == 0.334 && 
                                            sp.TestName == "Single Create 1:1")
                .And.ContainSingle(sp => sp.EfAverage == 2.711 &&
                                            sp.EfStandardDeviation == 0 &&
                                            sp.EfCoefficentOfVariation == 0 &&
                                            sp.EfExecutionTimePerRecord == 0.246 &&
                                            sp.IsBulk == true &&
                                            sp.NHibernateAverage == 2.110 &&
                                            sp.NHibernateStandardDeviation == 0 &&
                                            sp.NHibernateCoefficentOfVariation == 0 &&
                                            sp.Difference == 0.601 &&
                                            sp.NHibernateExecutionTimePerRecord == 0.192 &&
                                            sp.NumberOfRecords == 11 &&
                                            sp.DifferenceBetweenTimePerRecord == 0.054 && 
                                            sp.TestName == "Bulk Update 1:N");
        }
    }
}
