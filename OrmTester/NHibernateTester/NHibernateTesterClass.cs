using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using OrmTesterLib.TestCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateTester
{
    public class NHibernateTesterClass : BaseTester
    {
        public NHibernateTesterClass(TestParametersBuilder testParameters):base(testParameters)
        {
            
        }

        //TODO implement test execution using ITestOperations, generate test data using TestDataFactory
    }
}
