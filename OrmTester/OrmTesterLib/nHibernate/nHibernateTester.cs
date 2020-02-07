using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;

namespace OrmTesterLib.nHibernate
{
    public class NHibernateTester
    {       
        public NHibernateTester()
        {
            var cfg = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Krzysztof\Desktop\OrmTester\OrmTester\OrmTesterLib\nHibernate.mdf;Integrated Security=True;").ShowSql)
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<entity.ClassMapper>()).BuildConfiguration();
            var exporter = new SchemaExport(cfg);
            exporter.Execute(true, true, false);

            var _sessionFactory = cfg.BuildSessionFactory();
        }
    }
}
