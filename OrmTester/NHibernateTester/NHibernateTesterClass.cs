using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateTester
{
    public class NHibernateTesterClass
    {
        public NHibernateTesterClass()
        {
            var cfg = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NHibernate;Integrated Security=True").ShowSql)
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Entities.ClassMapper>()).BuildConfiguration();
            var exporter = new SchemaExport(cfg);
            exporter.Execute(true, true, false);

            var _sessionFactory = cfg.BuildSessionFactory();
        }
    }
}
