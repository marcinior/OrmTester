using OrmTesterLib.Enums;
using OrmTesterLib.TestCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace OrmTesterLib.StatisticParametersCalculator
{
    public class StatisticParametersCalculator
    {
        private ResourceManager resourceManager;
        private CultureInfo cultureInfo;
        private const string IsBulkTestProperty = "IsBulkTest";
        private const string OperationTypeProperty = "OperationType";
        private const string RelationshipTypeProperty = "RelationshipType";
        private const string MissingTestResultsError = "MissingTestResultsError";
        private const string DifferentCountOfGroupsError = "DifferentCountOfGroupsError";

        public StatisticParametersCalculator()
        {
            resourceManager = new ResourceManager(typeof(Resources.Resources));
        }

        public List<StatisticParameter> CalculateStatisticParameters(
            List<TestResult> efTestResults,
            List<TestResult> nHibernateResults,
            CultureInfo cultureInfo = null)
        {
            if (efTestResults == null || nHibernateResults == null || efTestResults.Count == 0 || nHibernateResults.Count == 0)
                throw new ArgumentNullException(resourceManager.GetString(MissingTestResultsError, cultureInfo));

            this.cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;
            var efGroupedResults = GetGroupedTestResults(efTestResults);
            var nHiberanateGroupedResults = GetGroupedTestResults(nHibernateResults);

            if (efGroupedResults.Count != nHiberanateGroupedResults.Count)
                throw new ArgumentException(resourceManager.GetString(DifferentCountOfGroupsError, cultureInfo));

            List<StatisticParameter> statisticParameters = new List<StatisticParameter>();
            for(int i = 0; i < efGroupedResults.Count; i++)
            {
                StatisticParameter statParam = new StatisticParameter
                {
                    TestName = efGroupedResults[i].Item1,
                    EfAverage = CalculateAverage(efGroupedResults[i].Item2),
                    NHibernateAverage = CalculateAverage(nHiberanateGroupedResults[i].Item2),
                    RelationshipType = efGroupedResults[i].Item2.FirstOrDefault().RelationshipType,
                    OperationType = efGroupedResults[i].Item2.FirstOrDefault().OperationType,
                    IsBulk = efGroupedResults[i].Item2.FirstOrDefault().IsBulkTest
                };

                statParam.Difference = CalculateDifference(statParam.EfAverage, statParam.NHibernateAverage);
                statParam.EfStandardDeviation = CalculateStandardDeviation(efGroupedResults[i].Item2, statParam.EfAverage);
                statParam.NHibernateStandardDeviation = CalculateStandardDeviation(nHiberanateGroupedResults[i].Item2, statParam.NHibernateAverage);
                statParam.EfCoefficentOfVariation = CalculateCoefficentOfVariation(statParam.EfStandardDeviation, statParam.EfAverage);
                statParam.NHibernateCoefficentOfVariation = CalculateCoefficentOfVariation(statParam.NHibernateStandardDeviation, statParam.NHibernateAverage);

                statisticParameters.Add(statParam);
            }

            return statisticParameters;
        }

        private double CalculateDifference(double firstAverage, double secondAverage)
        {
            return Math.Round(Math.Abs(firstAverage - secondAverage), 2);
        }

        private double CalculateAverage(List<TestResult> testResults)
        {
            return Math.Round(testResults.Average(tr => tr.ExecutionTime.TotalMilliseconds), 2);
        }

        //odchylenie standardowe z populacji
        private double CalculateStandardDeviation(List<TestResult> testResults, double average)
        {
            double sumOfSquaresOfDifferences = testResults.Select(tr => Math.Pow(tr.ExecutionTime.TotalMilliseconds - average, 2)).Sum();
            return Math.Round(Math.Sqrt(sumOfSquaresOfDifferences / testResults.Count), 2);
        }

        private double CalculateCoefficentOfVariation(double standardDeviation, double average)
        {
            return Math.Round(standardDeviation / average * 100, 2);
        }

        private List<Tuple<string, List<TestResult>>> GetGroupedTestResults(List<TestResult> testResults)
        {
            return testResults
                .GroupBy(tr => new
                {
                    tr.IsBulkTest,
                    tr.OperationType,
                    tr.RelationshipType
                })
                .Select(group => new Tuple<string, List<TestResult>>(GetTestName(group.Key), group.ToList()))
                .OrderBy(p => p.Item1)
                .ToList();
        }

        private string GetTestName(object key)
        {
            string testName = "";
            Type type = key.GetType();
            var isBulkTest = (bool)type.GetProperty(IsBulkTestProperty).GetValue(key);
            var operationType = (OperationType)type.GetProperty(OperationTypeProperty).GetValue(key);
            var relationshipType = (RelationshipType)type.GetProperty(RelationshipTypeProperty).GetValue(key);

            testName = isBulkTest ? resourceManager.GetString("Bulk", cultureInfo) : resourceManager.GetString("Single", cultureInfo);
            testName += " " + resourceManager.GetString(operationType.ToString(), cultureInfo) + " ";
            testName += resourceManager.GetString(relationshipType.ToString(), cultureInfo);

            return testName;
        }
    }
}
