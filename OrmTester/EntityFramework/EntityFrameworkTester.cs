using EntityFramework.Entity;
using Microsoft.EntityFrameworkCore;
using OrmTesterLib.Interfaces;
using OrmTesterLib.TestCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EntityFramework
{
    public class EntityFrameworkTester : BaseTester, ITestOperations, IDisposable
    {
        private EfDbContext db;

        public EntityFrameworkTester(TestParametersBuilder testParametersBuilder) : base(testParametersBuilder)
        {
            db = new EfDbContext();
            db.Database.OpenConnection();
            TruncateDatabase();
        }

        public void TruncateDatabase()
        {
            db.Indexes.RemoveRange(db.Indexes);
            db.Classes.RemoveRange(db.Classes);
            db.StudentSubjects.RemoveRange(db.StudentSubjects);
            db.Students.RemoveRange(db.Students);
            db.Subjects.RemoveRange(db.Subjects);
            db.SaveChanges();
        }

        public TimeSpan BulkCreateManyToMany(int numberOfRecords)
        {
            var students = TestDataFactory.GetStudents(numberOfRecords);
            db.Students.AddRange(students);

            var subjects = TestDataFactory.GetSubjects(numberOfRecords);
            subjects.ForEach(s =>
            {
                s.StudentSubjects = new List<StudentSubject>();
                for (int i = 0; i < students.Count; i++)
                    s.StudentSubjects.Add(new StudentSubject
                    {
                        Student = students[i],
                        Subject = s
                    });
            });

            db.Subjects.AddRange(subjects);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkCreateOneToMany(int numberOfRecords)
        {
            var students = TestDataFactory.GetStudents(numberOfRecords);
            db.Students.AddRange(students);

            Class @class = TestDataFactory.GetClass();
            @class.Students = new List<Student>(students);
            db.Classes.Add(@class);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkCreateOneToOne(int numberOfRecords)
        {
            var indexes = TestDataFactory.GetIndexes(numberOfRecords);
            db.Indexes.AddRange(indexes);

            var students = TestDataFactory.GetStudents(numberOfRecords);
            for (int i = 0; i < students.Count; i++)
                students[i].Index = indexes[i];

            db.Students.AddRange(students);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkCreateWithoutRelationship(int numberOfRecords)
        {
            var classes = TestDataFactory.GetClasses(numberOfRecords);
            db.Classes.AddRange(classes);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkDeleteManyToMany(int numberOfRecords)
        {
            var students = TestDataFactory.GetStudents(numberOfRecords);
            db.Students.AddRange(students);

            var subjects = TestDataFactory.GetSubjects(numberOfRecords);
            subjects.ForEach(s =>
            {
                s.StudentSubjects = new List<StudentSubject>();
                for (int i = 0; i < students.Count; i++)
                    s.StudentSubjects.Add(new StudentSubject
                    {
                        Student = students[i],
                        Subject = s
                    });
            });

            db.Subjects.AddRange(subjects);
            db.SaveChanges();

            var studentSubjects = db.StudentSubjects.Where(ss => students.Contains(ss.Student));
            db.StudentSubjects.RemoveRange(studentSubjects);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkDeleteWithoutRelationship(int numberOfRecords)
        {
            var classes = TestDataFactory.GetClasses(numberOfRecords);
            db.Classes.AddRange(classes);
            db.SaveChanges();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.Classes.RemoveRange(classes);
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkDeleteOneToMany(int numberOfRecords)
        {
            var students = TestDataFactory.GetStudents(numberOfRecords);
            db.Students.AddRange(students);

            Class @class = TestDataFactory.GetClass();
            @class.Students = new List<Student>(students);
            db.Classes.Add(@class);
            db.SaveChanges();

            db.Students.RemoveRange(students);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkDeleteOneToOne(int numberOfRecords)
        {
            var indexes = TestDataFactory.GetIndexes(numberOfRecords);
            db.Indexes.AddRange(indexes);

            var students = TestDataFactory.GetStudents(numberOfRecords);
            for (int i = 0; i < students.Count; i++)
                students[i].Index = indexes[i];

            db.Students.AddRange(students);
            db.SaveChanges();

            db.Indexes.RemoveRange(indexes);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkUpdateManyToMany(int numberOfRecords)
        {
            var students = TestDataFactory.GetStudents(numberOfRecords);
            db.Students.AddRange(students);

            var subjects = TestDataFactory.GetSubjects(numberOfRecords);
            subjects.ForEach(s =>
            {
                s.StudentSubjects = new List<StudentSubject>();
                for (int i = 0; i < students.Count; i++)
                    s.StudentSubjects.Add(new StudentSubject
                    {
                        Student = students[i],
                        Subject = s
                    });
            });

            db.Subjects.AddRange(subjects);
            db.SaveChanges();

            students.ForEach(s =>
            {
                s.FirstName = "Name";
                s.LastName = "Surname";
                s.BirthDate = DateTime.Now.AddYears(-25);
                s.UpdatedAt = DateTime.Now.AddDays(1);
            });

            subjects.ForEach(s =>
            {
                s.ClassYear = 1;
                s.Ects = 8;
                s.ExamType = ExamType.PROJECT;
                s.SubjectName = "New Subject";
                s.UpdatedAt = DateTime.Now.AddDays(1);
            });

            db.Students.UpdateRange(students);
            db.Subjects.UpdateRange(subjects);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkUpdateOneToMany(int numberOfRecords)
        {
            var students = TestDataFactory.GetStudents(numberOfRecords);
            db.Students.AddRange(students);

            Class @class = TestDataFactory.GetClass();
            @class.Students = new List<Student>(students);
            db.Classes.Add(@class);
            db.SaveChanges();

            students.ForEach(s =>
            {
                s.FirstName = "Name";
                s.LastName = "Surname";
                s.BirthDate = DateTime.Now.AddYears(-25);
                s.UpdatedAt = DateTime.Now.AddDays(1);
            });
            db.Students.UpdateRange(students);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkUpdateOneToOne(int numberOfRecords)
        {
            var indexes = TestDataFactory.GetIndexes(numberOfRecords);
            db.Indexes.AddRange(indexes);

            var students = TestDataFactory.GetStudents(numberOfRecords);
            for (int i = 0; i < students.Count; i++)
                students[i].Index = indexes[i];

            db.Students.AddRange(students);
            db.SaveChanges();

            indexes.ForEach(i => i.UpdatedAt = DateTime.Now.AddDays(1));
            db.Indexes.UpdateRange(indexes);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkUpdateWithoutRelationship(int numberOfRecords)
        {
            var classes = TestDataFactory.GetClasses(numberOfRecords);
            db.Classes.AddRange(classes);
            db.SaveChanges();

            classes.ForEach(c =>
            {
                c.DegreeCourse = "Elektrotechnika";
                c.UpdatedAt = DateTime.Now.AddDays(1);
                c.Year = 6;
                c.GroupNumber = 11;
            });

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.Classes.UpdateRange(classes);
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleCreateManyToMany()
        {
            Student student = TestDataFactory.GetStudent();
            db.Students.Add(student);

            Subject subject = TestDataFactory.GetSubject();
            subject.StudentSubjects = new List<StudentSubject>
            {
                new StudentSubject()
                {
                    Student = student,
                    Subject = subject
                }
            };
            db.Subjects.Add(subject);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleCreateOneToMany()
        {
            Student student = TestDataFactory.GetStudent();
            db.Students.Add(student);

            Class @class = TestDataFactory.GetClass();
            @class.Students = new List<Student>
            {
                student
            };
            db.Classes.Add(@class);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleCreateOneToOne()
        {
            Index index = TestDataFactory.GetIndex();
            db.Indexes.Add(index);

            Student student = TestDataFactory.GetStudent();
            student.Index = index;
            db.Students.Add(student);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleCreateWithoutRelationship()
        {
            Class c = TestDataFactory.GetClass();
            db.Classes.Add(c);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleDeleteManyToMany()
        {
            Student student = TestDataFactory.GetStudent();
            db.Students.Add(student);

            Subject subject = TestDataFactory.GetSubject();
            StudentSubject studentSubject = new StudentSubject()
            {
                Student = student,
                Subject = subject
            };

            subject.StudentSubjects = new List<StudentSubject>
            {
                studentSubject
            };
            db.Subjects.Add(subject);
            db.SaveChanges();

            db.StudentSubjects.Remove(studentSubject);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleDeleteOneToMany()
        {
            Student student = TestDataFactory.GetStudent();
            db.Students.Add(student);

            Class @class = TestDataFactory.GetClass();
            @class.Students = new List<Student>
            {
                student
            };
            db.Classes.Add(@class);
            db.SaveChanges();

            db.Students.Remove(student);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleDeleteOneToOne()
        {
            Index index = TestDataFactory.GetIndex();
            db.Indexes.Add(index);

            Student student = TestDataFactory.GetStudent();
            student.Index = index;
            db.Students.Add(student);
            db.SaveChanges();

            db.Indexes.Remove(index);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleDeleteWithoutRelationship()
        {
            Class @class = TestDataFactory.GetClass();
            db.Classes.Add(@class);
            db.SaveChanges();

            Stopwatch stopwatch = new Stopwatch();
            db.Classes.Remove(@class);
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleUpdateManyToMany()
        {
            Student student = TestDataFactory.GetStudent();
            db.Students.Add(student);

            Subject subject = TestDataFactory.GetSubject();
            subject.StudentSubjects = new List<StudentSubject>
            {
                new StudentSubject()
                {
                    Student = student,
                    Subject = subject
                }
            };
            db.Subjects.Add(subject);
            db.SaveChanges();

            student.FirstName = "Name";
            student.LastName = "Surname";
            student.BirthDate = DateTime.Now.AddYears(-25);
            student.UpdatedAt = DateTime.Now.AddDays(1);

            subject.ClassYear = 1;
            subject.Ects = 8;
            subject.ExamType = ExamType.PROJECT;
            subject.SubjectName = "New Subject";
            subject.UpdatedAt = DateTime.Now.AddDays(1);

            db.Students.Update(student);
            db.Subjects.Update(subject);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleUpdateOneToMany()
        {
            Student student = TestDataFactory.GetStudent();
            db.Students.Add(student);

            Class @class = TestDataFactory.GetClass();
            @class.Students = new List<Student>
            {
                student
            };
            db.Classes.Add(@class);
            db.SaveChanges();

            student.FirstName = "Name";
            student.LastName = "Surname";
            student.BirthDate = DateTime.Now.AddYears(-25);
            student.UpdatedAt = DateTime.Now.AddDays(1);

            db.Students.Update(student);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleUpdateOneToOne()
        {
            Index index = TestDataFactory.GetIndex();
            db.Indexes.Add(index);

            Student student = TestDataFactory.GetStudent();
            student.Index = index;
            db.Students.Add(student);
            db.SaveChanges();

            index.UpdatedAt = DateTime.Now.AddDays(1);
            db.Indexes.Update(index);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleUpdateWithoutRelationship()
        {
            Class @class = TestDataFactory.GetClass();
            db.Classes.Add(@class);
            db.SaveChanges();

            @class.DegreeCourse = "Elektrotechnika";
            @class.GroupNumber = 11;
            @class.UpdatedAt = DateTime.Now.AddDays(1);
            @class.Year = 6;

            Stopwatch stopwatch = new Stopwatch();
            db.Classes.Update(@class);
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public void Dispose()
        {
            db.Database.CloseConnection();
            db.Dispose();
        }
    }
}
