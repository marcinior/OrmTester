using OrmTesterLib.Enums;
using System;

namespace OrmTesterLib.StatisticParametersCalculator
{
    [Serializable]
    public class StatisticParameter
    {
        public string TestName { get; set; }

        public double EfAverage { get; set; }

        public double NHibernateAverage { get; set; }

        public double Difference { get; set; }

        public double EfStandardDeviation { get; set; }

        public double NHibernateStandardDeviation { get; set; }

        public double EfCoefficentOfVariation { get; set; }

        public double NHibernateCoefficentOfVariation { get; set; }

        public bool IsBulk { get; set; }

        public RelationshipType RelationshipType { get; set; }

        public OperationType OperationType { get; set; }

        public double ExecutionTimePerRecord { get; set; }
    }
}
