using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernateTester.Entities;
using OrmTesterLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateTester
{
    class NHibernateTestOperations : ITestOperations
    {
        private ISessionFactory _sessionFactory;

        public NHibernateTestOperations()
        {
            var cfg = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NHibernate;Integrated Security=True").ShowSql)
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Entities.ClassMapper>()).BuildConfiguration();
            var exporter = new SchemaExport(cfg);
            exporter.Execute(true, true, false);

            _sessionFactory = cfg.BuildSessionFactory();
        }

        public TimeSpan BulkCreateManyToMany()
        {
            var students = NHibernateDataGenerator.GetStudents(500);
            var subjects = NHibernateDataGenerator.GetSubjects(500);
            var watch = new Stopwatch();
            using (var session = _sessionFactory.OpenSession()) 
            {
                using (var transaction = session.BeginTransaction())
                {                                       
                    watch.Start();
                    foreach (var student in students)
                    {
                        session.Save(student);
                        foreach (var subject in subjects)
                        {
                            session.Save(subject);
                            var studentSubject = new StudentSubject
                            {
                                StudentId = student,
                                SubjectId = subject
                            };
                            session.Save(studentSubject);
                        }
                    }
                    transaction.Commit();
                    watch.Stop();
                }
            }
            return TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds);
        }

        public TimeSpan BulkCreateOneToMany()
        {
            throw new NotImplementedException();
        }

        public TimeSpan BulkCreateOneToOne()
        {
            throw new NotImplementedException();
        }

        public TimeSpan BulkCreateWithoutRelationship()
        {
            throw new NotImplementedException();
        }

        public TimeSpan BulkDeleteManyToMany()
        {
            throw new NotImplementedException();
        }

        public TimeSpan BulkDeleteNoRelationship()
        {
            throw new NotImplementedException();
        }

        public TimeSpan BulkDeleteOneToMany()
        {
            throw new NotImplementedException();
        }

        public TimeSpan BulkDeleteOneToOne()
        {
            throw new NotImplementedException();
        }

        public TimeSpan BulkUpdateManyToMany()
        {
            throw new NotImplementedException();
        }

        public TimeSpan BulkUpdateOneToMany()
        {
            throw new NotImplementedException();
        }

        public TimeSpan BulkUpdateOneToOne()
        {
            throw new NotImplementedException();
        }

        public TimeSpan BulkUpdateWithoutRelationship()
        {
            throw new NotImplementedException();
        }

        public TimeSpan SingleCreateManyToMany()
        {
            throw new NotImplementedException();
        }

        public TimeSpan SingleCreateOneToMany()
        {
            throw new NotImplementedException();
        }

        public TimeSpan SingleCreateOneToOne()
        {
            throw new NotImplementedException();
        }

        public TimeSpan SingleCreateWithoutRelationship()
        {
            throw new NotImplementedException();
        }

        public TimeSpan SingleDeleteManyToMany()
        {
            throw new NotImplementedException();
        }

        public TimeSpan SingleDeleteOneToMany()
        {
            throw new NotImplementedException();
        }

        public TimeSpan SingleDeleteOneToOne()
        {
            throw new NotImplementedException();
        }

        public TimeSpan SingleDeleteWithoutRelationship()
        {
            throw new NotImplementedException();
        }

        public TimeSpan SingleUpdateManyToMany()
        {
            throw new NotImplementedException();
        }

        public TimeSpan SingleUpdateOneToMany()
        {
            throw new NotImplementedException();
        }

        public TimeSpan SingleUpdateOneToOne()
        {
            throw new NotImplementedException();
        }

        public TimeSpan SingleUpdateWithoutRelationship()
        {
            throw new NotImplementedException();
        }
    }
}
