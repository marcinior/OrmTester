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
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NHibernate;Integrated Security=True";

        public NHibernateTestOperations(TestParameters testParameters) : base(testParameters)
        {
            var cfg = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).ShowSql)
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClassMapper>()).BuildConfiguration();
            var exporter = new SchemaUpdate(cfg);
            exporter.Execute(false, true);

            _sessionFactory = cfg.BuildSessionFactory();
            session = _sessionFactory.OpenSession();
        }

        public TimeSpan BulkCreateManyToMany(int records)
        {
            return CreateManyToMany(records);
        }

        private TimeSpan CreateManyToMany(int repetitions)
        {
            var students = NHibernateDataGenerator.GetStudents(repetitions);
            var subjects = NHibernateDataGenerator.GetSubjects(repetitions);

            var watch = new Stopwatch();
            watch.Start();

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

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan BulkCreateOneToMany(int records)
        {
            return CreateOneToMany(records);
        }

        private TimeSpan CreateOneToMany(int repetitions)
        {
            var students = NHibernateDataGenerator.GetStudents(repetitions);
            var @class = NHibernateDataGenerator.GetClass();

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {
                foreach (var student in students)
                {
                    session.Save(student);
                    @class.Student.Add(student);
                }

                session.Save(@class);
                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }


        public TimeSpan BulkCreateOneToOne(int records)
        {
            return CreateOneToOne(records);
        }

        private TimeSpan CreateOneToOne(int repetitions)
        {
            var students = NHibernateDataGenerator.GetStudents(repetitions);
            var indices = NHibernateDataGenerator.GetIndices(repetitions);

            var watch = new Stopwatch();
            watch.Start();

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

            watch.Stop();
            return watch.Elapsed;
        }


        public TimeSpan BulkCreateWithoutRelationship(int records)
        {
            return CreateWithoutRelationship(records);
        }

        private TimeSpan CreateWithoutRelationship(int repetitions)
        {
            var classes = NHibernateDataGenerator.GetClasses(repetitions);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {
                foreach (var @class in classes)
                {
                    session.Save(@class);
                }

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }


        public TimeSpan BulkDeleteManyToMany(int records)
        {
            return DeleteManyToMany(records);
        }

        private TimeSpan DeleteManyToMany(int repetitions)
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

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {
                foreach (var student in students)
                {
                    foreach (var studentSubject in student.StudentSubject)
                    {
                        session.Delete(studentSubject);
                    }
                    student.StudentSubject.Clear();
                }
                subjects.ForEach(subject => subject.StudentSubject.Clear());
                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan BulkDeleteWithoutRelationship(int records)
        {
            return DeleteNoRelationship(records);
        }

        private TimeSpan DeleteNoRelationship(int repetitions)
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

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {
                foreach (var @class in classes)
                {
                    session.Delete(@class);
                }

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan BulkDeleteOneToMany(int records)
        {
            return DeleteOneToMany(records);
        }

        private TimeSpan DeleteOneToMany(int repetitions)
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

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {
                foreach (var student in students)
                {
                    session.Delete(student);
                }
                @class.Student.Clear();

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;

        }

        public TimeSpan BulkDeleteOneToOne(int records)
        {
            return DeleteOneToOne(records);
        }

        private TimeSpan DeleteOneToOne(int repetitions)
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

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {
                foreach (var student in students)
                {
                    session.Delete(student);
                }

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan BulkUpdateManyToMany(int records)
        {
            return UpdateManyToMany(records);
        }

        private TimeSpan UpdateManyToMany(int repetitions)
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

            var watch = new Stopwatch();
            watch.Start();

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

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }


        public TimeSpan BulkUpdateOneToMany(int records)
        {
            return UpdateOneToMany(records);
        }

        private TimeSpan UpdateOneToMany(int repetitions)
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

            var watch = new Stopwatch();
            watch.Start();

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
                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan BulkUpdateOneToOne(int records)
        {
            return UpdateOneToOne(records);
        }

        private TimeSpan UpdateOneToOne(int repetitions)
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

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {
                foreach (var index in indices)
                {
                    index.UpdatedAt = DateTime.Now.AddDays(1);
                }

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }


        public TimeSpan BulkUpdateWithoutRelationship(int records)
        {
            return UpdateWithoutRelationship(records);
        }

        private TimeSpan UpdateWithoutRelationship(int repetitions)
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

            var watch = new Stopwatch();
            watch.Start();

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

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;

        }

        public TimeSpan SingleCreateManyToMany()
        {
            return CreateManyToMany(1);
        }

        public TimeSpan SingleCreateOneToMany()
        {
            return CreateOneToMany(1);
        }

        public TimeSpan SingleCreateOneToOne()
        {
            return CreateOneToOne(1);
        }

        public TimeSpan SingleCreateWithoutRelationship()
        {
            return CreateWithoutRelationship(1);
        }

        public TimeSpan SingleDeleteManyToMany()
        {
            return DeleteManyToMany(1);
        }

        public TimeSpan SingleDeleteOneToMany()
        {
            return DeleteOneToMany(1);
        }

        public TimeSpan SingleDeleteOneToOne()
        {
            return DeleteOneToOne(1);
        }

        public TimeSpan SingleDeleteWithoutRelationship()
        {
            return DeleteNoRelationship(1);
        }

        public TimeSpan SingleUpdateManyToMany()
        {
            return UpdateManyToMany(1);
        }

        public TimeSpan SingleUpdateOneToMany()
        {
            return UpdateOneToMany(1);
        }

        public TimeSpan SingleUpdateOneToOne()
        {
            return UpdateOneToOne(1);
        }

        public TimeSpan SingleUpdateWithoutRelationship()
        {
            return UpdateWithoutRelationship(1);
        }

        public void Dispose()
        {
            session.Dispose();
        }

        public void TruncateDatabase()
        {
            session.Delete("from Class");
            session.Delete("from StudentSubject");
            session.Delete("from Index");
            session.Delete("from Student");
            session.Delete("from Subject");
            session.Flush();
        }
    }
}
