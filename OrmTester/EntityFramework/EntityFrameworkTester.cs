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

        public EntityFrameworkTester(TestParameters testParameters) : base(testParameters)
        {
            db = new EfDbContext();
            db.Database.OpenConnection();
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
            var students = numberOfRecords < 100 ? TestDataFactory.GetStudents(numberOfRecords) : TestDataFactory.GetStudents(100);
            var subjects = numberOfRecords < 100 ? TestDataFactory.GetSubjects(1) : TestDataFactory.GetSubjects(numberOfRecords / 100);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            db.Students.AddRange(students);

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

            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkCreateOneToMany(int numberOfRecords)
        {
            var students = TestDataFactory.GetStudents(numberOfRecords);
            Class @class = TestDataFactory.GetClass();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            db.Students.AddRange(students);
            @class.Students = new List<Student>(students);
            db.Classes.Add(@class);
            db.SaveChanges();

            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkCreateOneToOne(int numberOfRecords)
        {
            var indexes = TestDataFactory.GetIndexes(numberOfRecords);
            var students = TestDataFactory.GetStudents(numberOfRecords);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            db.Indexes.AddRange(indexes);
            for (int i = 0; i < students.Count; i++)
                students[i].Index = indexes[i];

            db.Students.AddRange(students);
            db.SaveChanges();

            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkCreateWithoutRelationship(int numberOfRecords)
        {
            var classes = TestDataFactory.GetClasses(numberOfRecords);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            db.Classes.AddRange(classes);
            db.SaveChanges();

            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkDeleteManyToMany(int numberOfRecords)
        {
            var students = numberOfRecords < 100 ? TestDataFactory.GetStudents(numberOfRecords) : TestDataFactory.GetStudents(100);
            var subjects = numberOfRecords < 100 ? TestDataFactory.GetSubjects(1) : TestDataFactory.GetSubjects(numberOfRecords / 100);
            db.Students.AddRange(students);

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

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            db.StudentSubjects.RemoveRange(studentSubjects);
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

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            db.Students.RemoveRange(students);
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

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            db.Indexes.RemoveRange(indexes);
            db.SaveChanges();

            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkUpdateManyToMany(int numberOfRecords)
        {
            var students = numberOfRecords < 100 ? TestDataFactory.GetStudents(numberOfRecords) : TestDataFactory.GetStudents(100);
            var subjects = numberOfRecords < 100 ? TestDataFactory.GetSubjects(1) : TestDataFactory.GetSubjects(numberOfRecords / 100);
            db.Students.AddRange(students);

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


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

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

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            students.ForEach(s =>
            {
                s.FirstName = "Name";
                s.LastName = "Surname";
                s.BirthDate = DateTime.Now.AddYears(-25);
                s.UpdatedAt = DateTime.Now.AddDays(1);
            });
            db.Students.UpdateRange(students);
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

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            indexes.ForEach(i => i.UpdatedAt = DateTime.Now.AddDays(1));
            db.Indexes.UpdateRange(indexes);
            db.SaveChanges();

            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkUpdateWithoutRelationship(int numberOfRecords)
        {
            var classes = TestDataFactory.GetClasses(numberOfRecords);
            db.Classes.AddRange(classes);
            db.SaveChanges();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            classes.ForEach(c =>
            {
                c.DegreeCourse = "Elektrotechnika";
                c.UpdatedAt = DateTime.Now.AddDays(1);
                c.Year = 6;
                c.GroupNumber = 11;
            });

            db.Classes.UpdateRange(classes);
            db.SaveChanges();

            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleCreateManyToMany()
        {
            Student student = TestDataFactory.GetStudent();
            Subject subject = TestDataFactory.GetSubject();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            db.Students.Add(student);
            db.Subjects.Add(subject);
            subject.StudentSubjects = new List<StudentSubject>
            {
                new StudentSubject()
                {
                    Student = student,
                    Subject = subject
                }
            };
            db.SaveChanges();

            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleCreateOneToMany()
        {
            Student student = TestDataFactory.GetStudent();
            Class @class = TestDataFactory.GetClass();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            db.Students.Add(student);
            @class.Students = new List<Student> { student };
            db.Classes.Add(@class);
            db.SaveChanges();

            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleCreateOneToOne()
        {
            Index index = TestDataFactory.GetIndex();
            Student student = TestDataFactory.GetStudent();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            db.Indexes.Add(index);
            db.Students.Add(student);
            student.Index = index;
            db.SaveChanges();

            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleCreateWithoutRelationship()
        {
            Class c = TestDataFactory.GetClass();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            db.Classes.Add(c);
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

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            db.StudentSubjects.Remove(studentSubject);
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

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            db.Students.Remove(student);
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

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            db.Indexes.Remove(index);
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
            stopwatch.Start();

            db.Classes.Remove(@class);
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

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

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

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            student.FirstName = "Name";
            student.LastName = "Surname";
            student.BirthDate = DateTime.Now.AddYears(-25);
            student.UpdatedAt = DateTime.Now.AddDays(1);

            db.Students.Update(student);
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

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            index.UpdatedAt = DateTime.Now.AddDays(1);
            db.Indexes.Update(index);
            db.SaveChanges();

            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleUpdateWithoutRelationship()
        {
            Class @class = TestDataFactory.GetClass();
            db.Classes.Add(@class);
            db.SaveChanges();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            @class.DegreeCourse = "Elektrotechnika";
            @class.GroupNumber = 11;
            @class.UpdatedAt = DateTime.Now.AddDays(1);
            @class.Year = 6;

            db.Classes.Update(@class);
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
