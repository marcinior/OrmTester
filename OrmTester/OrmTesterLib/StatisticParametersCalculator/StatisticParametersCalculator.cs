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
        private const string MissingTestResultsError = "MissingTestResultsError";
        private const string DifferentCountOfGroupsError = "DifferentCountOfGroupsError";
        private readonly CultureInfo cultureInfo;

        public StatisticParametersCalculator(CultureInfo cultureInfo)
        {
            resourceManager = new ResourceManager(typeof(Resources.Resources));
            this.cultureInfo = cultureInfo;
        }

        public List<StatisticParameter> CalculateStatisticParameters(List<TestResult> efTestResults, List<TestResult> nHibernateResults)
        {
            if (efTestResults == null || nHibernateResults == null || efTestResults.Count == 0 || nHibernateResults.Count == 0)
                throw new ArgumentNullException(resourceManager.GetString(MissingTestResultsError, cultureInfo));

            var efGroupedResults = efTestResults.GetGroupedTestResults(cultureInfo);
            var nHiberanateGroupedResults = nHibernateResults.GetGroupedTestResults(cultureInfo);

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

                if (statParam.IsBulk)
                {
                    int numberOfRecords = efGroupedResults[i].Item2.First().NumberOfRecords;
                    if (statParam.RelationshipType == RelationshipType.ManyToMany)
                        numberOfRecords *= 2;

                    statParam.EfExecutionTimePerRecord = Math.Round(statParam.EfAverage / numberOfRecords, 2);
                    statParam.NHibernateExecutionTimePerRecord = Math.Round(statParam.NHibernateAverage / numberOfRecords, 2);
                }
                else
                {
                    statParam.EfExecutionTimePerRecord = statParam.EfAverage;
                    statParam.NHibernateExecutionTimePerRecord = statParam.NHibernateAverage;
                }

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
    }
}
