using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernateTester.Entities;
using NHibernateTester.Enums;
using OrmTesterLib.Interfaces;
using OrmTesterLib.TestCore;
using System;
using System.Diagnostics;

namespace NHibernateTester
{
    public class NHibernateTestOperations : BaseTester, ITestOperations, IDisposable
    {
        private ISessionFactory _sessionFactory;
        private ISession session;

        public NHibernateTestOperations(TestParametersBuilder testParameters) : base(testParameters)
        {
            var cfg = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NHibernate;Integrated Security=True").ShowSql)
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClassMapper>()).BuildConfiguration();
            var exporter = new SchemaUpdate(cfg);
            exporter.Execute(false, true);

            _sessionFactory = cfg.BuildSessionFactory();
            session = _sessionFactory.OpenSession();
            session.Delete("from StudentSubject");
            session.Delete("from Student");
            session.Delete("from Class");
            session.Delete("from Index");
            session.Delete("from Subject");            
            session.Flush();
        }

        public TimeSpan BulkCreateManyToMany()
        {
            return CreateManyToMany(3);
        }

        private TimeSpan CreateManyToMany(int repetitions = 1)
        {
            var students = NHibernateDataGenerator.GetStudents(repetitions);
            var subjects = NHibernateDataGenerator.GetSubjects(repetitions);
            using (var transaction = session.BeginTransaction())
            {

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

                        subject.StudentSubject.Add(studentSubject);
                        session.Save(studentSubject);
                    }
                }

                var watch = new Stopwatch();
                watch.Start();
                transaction.Commit();
                watch.Stop();
                return watch.Elapsed;
            }

        }

        public TimeSpan BulkCreateOneToMany()
        {
            return CreateOneToMany(500);
        }

        private TimeSpan CreateOneToMany(int repetitions = 1)
        {
            var students = NHibernateDataGenerator.GetStudents(repetitions);
            var @class = NHibernateDataGenerator.GetClass();

            using (var transaction = session.BeginTransaction())
            {

                foreach (var student in students)
                {
                    session.Save(student);
                    @class.Student.Add(student);
                }

                session.Save(@class);
                var watch = new Stopwatch();
                watch.Start();
                transaction.Commit();
                watch.Stop();
                return watch.Elapsed;
            }
        }


        public TimeSpan BulkCreateOneToOne()
        {
            return CreateOneToOne(500);
        }

        private TimeSpan CreateOneToOne(int repetitions = 1)
        {
            var students = NHibernateDataGenerator.GetStudents(repetitions);
            var indices = NHibernateDataGenerator.GetIndices(repetitions);

            using (var transaction = session.BeginTransaction())
            {
                for (int i = 0; i < students.Count; i++)
                {
                    var index = indices[i];
                    var student = students[i];
                    student.IndexId = index;
                    session.Save(index);
                    session.Save(student);
                }

                var watch = new Stopwatch();
                watch.Start();
                transaction.Commit();
                watch.Stop();
                return watch.Elapsed;
            }
        }


        public TimeSpan BulkCreateWithoutRelationship()
        {
            return CreateWithoutRelationship(500);
        }

        private TimeSpan CreateWithoutRelationship(int repetitions = 1)
        {
            var classes = NHibernateDataGenerator.GetClasses(repetitions);

            using (var transaction = session.BeginTransaction())
            {
                foreach (var @class in classes)
                {
                    session.Save(@class);
                }

                Stopwatch watch = new Stopwatch();
                watch.Start();
                transaction.Commit();
                watch.Stop();
                return watch.Elapsed;
            }
        }


        public TimeSpan BulkDeleteManyToMany()
        {
            return DeleteManyToMany(3);
        }

        private TimeSpan DeleteManyToMany(int repetitions = 1)
        {
            var students = NHibernateDataGenerator.GetStudents(repetitions);
            var subjects = NHibernateDataGenerator.GetSubjects(repetitions);

            using (var transaction = session.BeginTransaction())
            {

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
                        student.StudentSubject.Add(studentSubject);
                        subject.StudentSubject.Add(studentSubject);
                        session.Save(studentSubject);
                    }
                }
                transaction.Commit();
            }

            using (var transaction = session.BeginTransaction())
            {
                foreach (var student in students)
                {
                    foreach (var studentSubject in student.StudentSubject)
                    {
                        session.Delete(studentSubject);
                    }
                }
                var watch = new Stopwatch();
                watch.Start();
                transaction.Commit();
                watch.Stop();
                return watch.Elapsed;
            }

        }

        public TimeSpan BulkDeleteWithoutRelationship()
        {
            return DeleteNoRelationship(500);
        }

        private TimeSpan DeleteNoRelationship(int repetitions = 1)
        {
            var classes = NHibernateDataGenerator.GetClasses(repetitions);

            using (var transaction = session.BeginTransaction())
            {
                foreach (var @class in classes)
                {
                    session.Save(@class);
                }

                transaction.Commit();
            }

            using (var transaction = session.BeginTransaction())
            {
                foreach (var @class in classes)
                {
                    session.Delete(@class);
                }

                var watch = new Stopwatch();
                watch.Start();
                transaction.Commit();
                watch.Stop();
                return watch.Elapsed;
            }

        }

        public TimeSpan BulkDeleteOneToMany()
        {
            return DeleteOneToMany(500);
        }

        private TimeSpan DeleteOneToMany(int repetitions = 1)
        {
            var students = NHibernateDataGenerator.GetStudents(repetitions);
            var @class = NHibernateDataGenerator.GetClass();

            using (var transaction = session.BeginTransaction())
            {

                foreach (var student in students)
                {
                    session.Save(student);
                    @class.Student.Add(student);
                    session.Save(@class);
                }

                transaction.Commit();
            }

            using (var transaction = session.BeginTransaction())
            {
                foreach (var student in students)
                {
                    session.Delete(student);
                }
                var watch = new Stopwatch();
                watch.Start();
                transaction.Commit();
                watch.Stop();
                return watch.Elapsed;
            }

        }

        public TimeSpan BulkDeleteOneToOne()
        {
            return DeleteOneToOne(500);
        }

        private TimeSpan DeleteOneToOne(int repetitions = 1)
        {
            var students = NHibernateDataGenerator.GetStudents(repetitions);
            var indices = NHibernateDataGenerator.GetIndices(repetitions);

            using (var transaction = session.BeginTransaction())
            {
                for (int i = 0; i < students.Count; i++)
                {
                    var index = indices[i];
                    var student = students[i];
                    student.IndexId = index;
                    session.Save(index);
                    session.Save(student);
                }

                transaction.Commit();
            }

            using (var transaction = session.BeginTransaction())
            {
                foreach (var student in students)
                {
                    session.Delete(student);
                }

                var watch = new Stopwatch();
                watch.Start();
                transaction.Commit();
                watch.Stop();
                return watch.Elapsed;
            }

        }

        public TimeSpan BulkUpdateManyToMany()
        {
            return UpdateManyToMany(3);
        }

        private TimeSpan UpdateManyToMany(int repetitions = 1)
        {
            var students = NHibernateDataGenerator.GetStudents(repetitions);
            var subjects = NHibernateDataGenerator.GetSubjects(repetitions);

            using (var transaction = session.BeginTransaction())
            {

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
                        student.StudentSubject.Add(studentSubject);
                        subject.StudentSubject.Add(studentSubject);
                        session.Save(studentSubject);
                    }
                }
                transaction.Commit();
            }

            using (var transaction = session.BeginTransaction())
            {

                foreach (var student in students)
                {
                    student.FirstName = "Name";
                    student.LastName = "Surname";
                    student.BirthDate = DateTime.Now.AddYears(-25);
                    student.UpdatedAt = DateTime.Now.AddDays(1);
                    session.Update(student);
                }
                foreach (var subject in subjects)
                {
                    subject.ClassesYear = 1;
                    subject.Ects = 8;
                    subject.ExamType = ExamType.PROJECT;
                    subject.SubjectName = "New Subject";
                    subject.UpdatedAt = DateTime.Now.AddDays(1);
                }
                var watch = new Stopwatch();
                watch.Start();
                transaction.Commit();
                watch.Stop();
                return watch.Elapsed;
            }
        }


        public TimeSpan BulkUpdateOneToMany()
        {
            return UpdateOneToMany(500);
        }

        private TimeSpan UpdateOneToMany(int repetitions = 1)
        {
            var students = NHibernateDataGenerator.GetStudents(repetitions);
            var @class = NHibernateDataGenerator.GetClass();

            using (var transaction = session.BeginTransaction())
            {

                foreach (var student in students)
                {
                    session.Save(student);
                    @class.Student.Add(student);
                    session.Save(@class);
                }

                transaction.Commit();
            }

            using (var transaction = session.BeginTransaction())
            {
                foreach (var student in students)
                {
                    student.FirstName = "Name";
                    student.LastName = "Surname";
                    student.BirthDate = DateTime.Now.AddYears(-25);
                    student.UpdatedAt = DateTime.Now.AddDays(1);
                    session.Update(student);
                }
                var watch = new Stopwatch();
                watch.Start();
                transaction.Commit();
                watch.Stop();
                return watch.Elapsed;
            }
        }

        public TimeSpan BulkUpdateOneToOne()
        {
            return UpdateOneToOne(500);
        }

        private TimeSpan UpdateOneToOne(int repetitions = 1)
        {
            var students = NHibernateDataGenerator.GetStudents(repetitions);
            var indices = NHibernateDataGenerator.GetIndices(repetitions);

            using (var transaction = session.BeginTransaction())
            {
                for (int i = 0; i < students.Count; i++)
                {
                    var index = indices[i];
                    var student = students[i];
                    student.IndexId = index;
                    session.Save(index);
                    session.Save(student);
                }

                transaction.Commit();
            }

            using (var transaction = session.BeginTransaction())
            {
                foreach (var index in indices)
                {
                    index.UpdatedAt = DateTime.Now.AddDays(1);
                }

                var watch = new Stopwatch();
                watch.Start();
                transaction.Commit();
                watch.Stop();
                return watch.Elapsed;
            }
        }


        public TimeSpan BulkUpdateWithoutRelationship()
        {
            return UpdateWithoutRelationship(500);
        }

        private TimeSpan UpdateWithoutRelationship(int repetitions = 1)
        {
            var classes = NHibernateDataGenerator.GetClasses(repetitions);

            using (var transaction = session.BeginTransaction())
            {
                foreach (var @class in classes)
                {
                    session.Save(@class);
                }

                transaction.Commit();
            }

            using (var transaction = session.BeginTransaction())
            {
                foreach (var @class in classes)
                {
                    @class.DegreeCourse = "Elektrotechnika";
                    @class.UpdatedAt = DateTime.Now.AddDays(1);
                    @class.Year = 6;
                    @class.GroupNumber = 11;
                    session.Update(@class);
                }

                var watch = new Stopwatch();
                watch.Start();
                transaction.Commit();
                watch.Stop();
                return watch.Elapsed;
            }

        }

        public TimeSpan SingleCreateManyToMany()
        {
            return CreateManyToMany();
        }

        public TimeSpan SingleCreateOneToMany()
        {
            return CreateOneToMany();
        }

        public TimeSpan SingleCreateOneToOne()
        {
            return CreateOneToOne();
        }

        public TimeSpan SingleCreateWithoutRelationship()
        {
            return CreateWithoutRelationship();
        }

        public TimeSpan SingleDeleteManyToMany()
        {
            return DeleteManyToMany();
        }

        public TimeSpan SingleDeleteOneToMany()
        {
            return DeleteOneToMany();
        }

        public TimeSpan SingleDeleteOneToOne()
        {
            return DeleteOneToOne();
        }

        public TimeSpan SingleDeleteWithoutRelationship()
        {
            return DeleteNoRelationship();
        }

        public TimeSpan SingleUpdateManyToMany()
        {
            return UpdateManyToMany();
        }

        public TimeSpan SingleUpdateOneToMany()
        {
            return UpdateOneToMany();
        }

        public TimeSpan SingleUpdateOneToOne()
        {
            return UpdateOneToOne();
        }

        public TimeSpan SingleUpdateWithoutRelationship()
        {
            return UpdateWithoutRelationship();
        }

        public void Dispose()
        {
            session.Dispose();
        }
    }
}
