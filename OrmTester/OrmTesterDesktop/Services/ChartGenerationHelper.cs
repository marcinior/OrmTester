﻿using LiveCharts;
using LiveCharts.Wpf;
using OrmTesterDesktop.Properties;
using OrmTesterDesktop.Views;
using OrmTesterLib.Enums;
using OrmTesterLib.StatisticParametersCalculator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrmTesterDesktop.Services
{
    class ChartGenerationHelper
    {
        public List<StatisticParameter> StatisticParameters { get; set; }

        public readonly Dictionary<RelationshipType, string> LabelsForRelationship = new Dictionary<RelationshipType, string>
        {
            {RelationshipType.ManyToMany, Resources.ManyToMany },
            {RelationshipType.OneToMany, Resources.OneToMany },
            {RelationshipType.OneToOne, Resources.OneToOne },
            {RelationshipType.None, Resources.None }
        };

        private string allTestLabel = Resources.AllTests;

        public void GenerateAverageBarChart(OperationType type)
        {
            var createParameters = GetParamsByOperation(type);

            var fullAverage = GetAverageForFrameworks(createParameters);

            var nHibernateResults = new ChartValues<double>();
            var efResults = new ChartValues<double>();
            var labels = new List<string>();
            labels.Add(allTestLabel);
            nHibernateResults.Add(fullAverage.Item1);
            efResults.Add(fullAverage.Item2);

            foreach (var relation in Enum.GetValues(typeof(RelationshipType)).Cast<RelationshipType>())
            {
                var averageForRelationship = GetAverageForRelationship(createParameters, relation);
                if (averageForRelationship != null)
                {
                    nHibernateResults.Add(averageForRelationship.Item1);
                    efResults.Add(averageForRelationship.Item2);
                    if (LabelsForRelationship.TryGetValue(relation, out var label))
                    {
                        labels.Add(label);
                    }
                }
            }

            var nHibernateSeriesCollection = new ColumnSeries
            {
                Title = "nHibernate",
                Values = nHibernateResults
            };

            var efSeriesCollection = new ColumnSeries
            {
                Title = "Entity Framework",
                Values = efResults
            };


            var chartView = new ChartView
            {
                Labels = labels.ToArray()
            };
            chartView.SeriesCollection.Add(nHibernateSeriesCollection);
            chartView.SeriesCollection.Add(efSeriesCollection);
            chartView.ShowDialog();
        }

        public void GenerateStandardDeviationBarChart(OperationType type)
        {
            var updateParameters = GetParamsByOperation(type);

            var fullAverage = GetAverageStandardDeviationForFrameworks(updateParameters);

            var nHibernateResults = new ChartValues<double>();
            var efResults = new ChartValues<double>();
            var labels = new List<string>();

            labels.Add(allTestLabel);

            nHibernateResults.Add(fullAverage.Item1);
            efResults.Add(fullAverage.Item2);

            foreach (var relation in Enum.GetValues(typeof(RelationshipType)).Cast<RelationshipType>())
            {
                var averageForRelationship = GetAverageStandardDeviationForRelationship(updateParameters, relation);
                if (averageForRelationship != null)
                {
                    nHibernateResults.Add(averageForRelationship.Item1);
                    efResults.Add(averageForRelationship.Item2);
                    if (LabelsForRelationship.TryGetValue(relation, out var label))
                    {
                        labels.Add(label);
                    }
                }
            }

            var nHibernateSeriesCollection = new ColumnSeries
            {
                Title = "nHibernate",
                Values = nHibernateResults
            };

            var efSeriesCollection = new ColumnSeries
            {
                Title = "Entity Framework",
                Values = efResults
            };


            var chartView = new ChartView
            {
                Labels = labels.ToArray()
            };
            chartView.SeriesCollection.Add(nHibernateSeriesCollection);
            chartView.SeriesCollection.Add(efSeriesCollection);
            chartView.ShowDialog();
        }

        public void GenerateCoefficentOfVariationBarChart(OperationType type)
        {
            var updateParameters = GetParamsByOperation(type);

            var fullAverage = GetAverageCoefficientOfVariationForFrameworks(updateParameters);

            var nHibernateResults = new ChartValues<double>();
            var efResults = new ChartValues<double>();
            var labels = new List<string>();

            labels.Add(allTestLabel);

            nHibernateResults.Add(fullAverage.Item1);
            efResults.Add(fullAverage.Item2);

            foreach (var relation in Enum.GetValues(typeof(RelationshipType)).Cast<RelationshipType>())
            {
                var averageForRelationship = GetAverageCoefficientOfVariationForRelationship(updateParameters, relation);
                if (averageForRelationship != null)
                {
                    nHibernateResults.Add(averageForRelationship.Item1);
                    efResults.Add(averageForRelationship.Item2);
                    if (LabelsForRelationship.TryGetValue(relation, out var label))
                    {
                        labels.Add(label);
                    }
                }
            }

            var nHibernateSeriesCollection = new ColumnSeries
            {
                Title = "nHibernate",
                Values = nHibernateResults
            };

            var efSeriesCollection = new ColumnSeries
            {
                Title = "Entity Framework",
                Values = efResults
            };


            var chartView = new ChartView
            {
                Labels = labels.ToArray()
            };
            chartView.SeriesCollection.Add(nHibernateSeriesCollection);
            chartView.SeriesCollection.Add(efSeriesCollection);
            chartView.ShowDialog();
        }

        private IEnumerable<StatisticParameter> GetParamsByOperation(OperationType operationType)
        {
            return this.StatisticParameters.Where(param => param.OperationType == operationType);
        }

        private Tuple<double, double> GetAverageForRelationship(IEnumerable<StatisticParameter> statisticParameters, RelationshipType relationshipType)
        {
            return this.GetAverageForFrameworks(statisticParameters.Where(param => param.RelationshipType == relationshipType));
        }

        private Tuple<double, double> GetAverageForFrameworks(IEnumerable<StatisticParameter> statisticParameters)
        {
            if (statisticParameters.Any())
            {
                var nHibernateCreateAverage = statisticParameters.Average(createParameter => createParameter.NHibernateAverage);
                var efCreateAverage = statisticParameters.Average(createParameter => createParameter.EfAverage);
                return new Tuple<double, double>(nHibernateCreateAverage, efCreateAverage);
            }
            else
            {
                return null;
            }
        }

        private Tuple<double, double> GetAverageStandardDeviationForRelationship(IEnumerable<StatisticParameter> statisticParameters, RelationshipType relationshipType)
        {
            return this.GetAverageStandardDeviationForFrameworks(statisticParameters.Where(param => param.RelationshipType == relationshipType));
        }

        private Tuple<double, double> GetAverageStandardDeviationForFrameworks(IEnumerable<StatisticParameter> statisticParameters)
        {
            if (statisticParameters.Any())
            {
                var nHibernateCreateAverage = statisticParameters.Average(createParameter => createParameter.NHibernateStandardDeviation);
                var efCreateAverage = statisticParameters.Average(createParameter => createParameter.EfStandardDeviation);
                return new Tuple<double, double>(nHibernateCreateAverage, efCreateAverage);
            }
            else
            {
                return null;
            }
        }

        private Tuple<double, double> GetAverageCoefficientOfVariationForRelationship(IEnumerable<StatisticParameter> statisticParameters, RelationshipType relationshipType)
        {
            return this.GetAverageCoefficientOfVariationForFrameworks(statisticParameters.Where(param => param.RelationshipType == relationshipType));
        }

        private Tuple<double, double> GetAverageCoefficientOfVariationForFrameworks(IEnumerable<StatisticParameter> statisticParameters)
        {
            if (statisticParameters.Any())
            {
                var nHibernateCreateAverage = statisticParameters.Average(createParameter => createParameter.NHibernateCoefficentOfVariation);
                var efCreateAverage = statisticParameters.Average(createParameter => createParameter.EfCoefficentOfVariation);
                return new Tuple<double, double>(nHibernateCreateAverage, efCreateAverage);
            }
            else
            {
                return null;
            }
        }
    }
}
