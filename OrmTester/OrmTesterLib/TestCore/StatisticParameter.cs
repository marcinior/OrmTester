using OrmTesterLib.Enums;

namespace OrmTesterLib.TestCore
{
    public class StatisticParameter
    {
        public double EfAverage { get; set; }

        public double NHibernateAverage { get; set; }

        public double Difference { get; set; }

        public double EfStandardDeviation { get; set; }

        public double NHiberanteStandardDeviation { get; set; }

        public OperationType OperationType { get; set; }

        public RelationshipType RelationshipType { get; set; }

        public bool IsBulkTest { get; set; }
    }
}
