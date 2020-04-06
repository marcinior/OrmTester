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
    public class NHibernateTester : BaseTester, ITestOperations, IDisposable
    {
        private ISessionFactory _sessionFactory;
        private ISession session;
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NHibernate;Integrated Security=True";

        public NHibernateTester(TestParameters testParameters) : base(testParameters)
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
            var students = NHibernateDataGenerator.GetStudents(records);
            var subjects = NHibernateDataGenerator.GetSubjects(records);

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
            var students = NHibernateDataGenerator.GetStudents(records);
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
            var students = NHibernateDataGenerator.GetStudents(records);
            var indices = NHibernateDataGenerator.GetIndices(records);

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
            var classes = NHibernateDataGenerator.GetClasses(records);

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
            var students = NHibernateDataGenerator.GetStudents(records);
            var subjects = NHibernateDataGenerator.GetSubjects(records);

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
            var classes = NHibernateDataGenerator.GetClasses(records);

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
            var students = NHibernateDataGenerator.GetStudents(records);
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
            var students = NHibernateDataGenerator.GetStudents(records);
            var indices = NHibernateDataGenerator.GetIndices(records);

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
            var students = NHibernateDataGenerator.GetStudents(records);
            var subjects = NHibernateDataGenerator.GetSubjects(records);

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
            var students = NHibernateDataGenerator.GetStudents(records);
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
            var students = NHibernateDataGenerator.GetStudents(records);
            var indices = NHibernateDataGenerator.GetIndices(records);

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
            var classes = NHibernateDataGenerator.GetClasses(records);

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
            var student = NHibernateDataGenerator.GetStudent();
            var subject = NHibernateDataGenerator.GetSubject();

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {

                session.Save(student);
                session.Save(subject);
                var studentSubject = new StudentSubject
                {
                    StudentId = student,
                    SubjectId = subject
                };

                subject.StudentSubject.Add(studentSubject);
                session.Save(studentSubject);



                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan SingleCreateOneToMany()
        {
            var student = NHibernateDataGenerator.GetStudent();
            var @class = NHibernateDataGenerator.GetClass();

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {

                session.Save(student);
                @class.Student.Add(student);


                session.Save(@class);
                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan SingleCreateOneToOne()
        {
            var student = NHibernateDataGenerator.GetStudent();
            var index = NHibernateDataGenerator.GetIndex(1);

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {

                student.IndexId = index;
                session.Save(index);
                session.Save(student);

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan SingleCreateWithoutRelationship()
        {
            var @class = NHibernateDataGenerator.GetClass();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {
                session.Save(@class);

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan SingleDeleteManyToMany()
        {
            var student = NHibernateDataGenerator.GetStudent();
            var subject = NHibernateDataGenerator.GetSubject();

            using (var transaction = session.BeginTransaction())
            {

                session.Save(student);

                session.Save(subject);
                var studentSubject = new StudentSubject
                {
                    StudentId = student,
                    SubjectId = subject
                };
                student.StudentSubject.Add(studentSubject);
                subject.StudentSubject.Add(studentSubject);
                session.Save(studentSubject);


                transaction.Commit();
            }

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {
                foreach (var studetSubject in student.StudentSubject)
                {
                    session.Delete(studetSubject);
                }

                student.StudentSubject.Clear();

                subject.StudentSubject.Clear();
                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan SingleDeleteOneToMany()
        {
            var student = NHibernateDataGenerator.GetStudent();
            var @class = NHibernateDataGenerator.GetClass();

            using (var transaction = session.BeginTransaction())
            {

                session.Save(student);
                @class.Student.Add(student);
                session.Save(@class);


                transaction.Commit();
            }

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {

                session.Delete(student);

                @class.Student.Clear();

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan SingleDeleteOneToOne()
        {
            var student = NHibernateDataGenerator.GetStudent();
            var index = NHibernateDataGenerator.GetIndex(1);

            using (var transaction = session.BeginTransaction())
            {
                student.IndexId = index;
                session.Save(index);
                session.Save(student);


                transaction.Commit();
            }

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {

                session.Delete(student);

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan SingleDeleteWithoutRelationship()
        {
            var @class = NHibernateDataGenerator.GetClass();

            using (var transaction = session.BeginTransaction())
            {
                session.Save(@class);


                transaction.Commit();
            }

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {
                session.Delete(@class);

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan SingleUpdateManyToMany()
        {
            var student = NHibernateDataGenerator.GetStudent();
            var subject = NHibernateDataGenerator.GetSubject();

            using (var transaction = session.BeginTransaction())
            {
                session.Save(student);

                session.Save(subject);
                var studentSubject = new StudentSubject
                {
                    StudentId = student,
                    SubjectId = subject
                };
                student.StudentSubject.Add(studentSubject);
                subject.StudentSubject.Add(studentSubject);
                session.Save(studentSubject);

                transaction.Commit();
            }

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {

                student.FirstName = "Name";
                student.LastName = "Surname";
                student.BirthDate = DateTime.Now.AddYears(-25);
                student.UpdatedAt = DateTime.Now.AddDays(1);
                session.Update(student);

                subject.ClassesYear = 1;
                subject.Ects = 8;
                subject.ExamType = ExamType.PROJECT;
                subject.SubjectName = "New Subject";
                subject.UpdatedAt = DateTime.Now.AddDays(1);

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan SingleUpdateOneToMany()
        {
            var student = NHibernateDataGenerator.GetStudent();
            var @class = NHibernateDataGenerator.GetClass();

            using (var transaction = session.BeginTransaction())
            {
                session.Save(student);
                @class.Student.Add(student);
                session.Save(@class);

                transaction.Commit();
            }

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {
                student.FirstName = "Name";
                student.LastName = "Surname";
                student.BirthDate = DateTime.Now.AddYears(-25);
                student.UpdatedAt = DateTime.Now.AddDays(1);
                session.Update(student);

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan SingleUpdateOneToOne()
        {
            var student = NHibernateDataGenerator.GetStudent();
            var index = NHibernateDataGenerator.GetIndex(1);

            using (var transaction = session.BeginTransaction())
            {

                student.IndexId = index;
                session.Save(index);
                session.Save(student);

                transaction.Commit();
            }

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {

                index.UpdatedAt = DateTime.Now.AddDays(1);

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
        }

        public TimeSpan SingleUpdateWithoutRelationship()
        {
            var @class = NHibernateDataGenerator.GetClass();

            using (var transaction = session.BeginTransaction())
            {

                session.Save(@class);


                transaction.Commit();
            }

            var watch = new Stopwatch();
            watch.Start();

            using (var transaction = session.BeginTransaction())
            {

                @class.DegreeCourse = "Elektrotechnika";
                @class.UpdatedAt = DateTime.Now.AddDays(1);
                @class.Year = 6;
                @class.GroupNumber = 11;
                session.Update(@class);

                transaction.Commit();
            }

            watch.Stop();
            return watch.Elapsed;
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
