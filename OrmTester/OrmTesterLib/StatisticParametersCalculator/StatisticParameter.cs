namespace OrmTesterLib.StatisticParametersCalculator
{
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
    }
}
