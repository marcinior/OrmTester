using EntityFramework.Entity;
using OrmTesterLib.Interfaces;
using OrmTesterLib.TestCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EntityFramework
{
    public class EntityFrameworkTester : BaseTester, ITestOperations
    {
        private EfDbContext db;
        private TestDataFactory testDataFactory;

        public EntityFrameworkTester(TestParametersBuilder testParametersBuilder) : base(testParametersBuilder)
        {
            db = new EfDbContext();
            testDataFactory = new TestDataFactory();
            db.Indexes.RemoveRange(db.Indexes);
            db.Classes.RemoveRange(db.Classes);
            db.StudentSubjects.RemoveRange(db.StudentSubjects);
            db.Students.RemoveRange(db.Students);
            db.Subjects.RemoveRange(db.Subjects);
            db.SaveChanges();
        }

        public TimeSpan BulkCreateManyToMany()
        {
            var students = testDataFactory.GetStudents(3);
            db.Students.AddRange(students);

            var subjects = testDataFactory.GetSubjects(3);
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

        public TimeSpan BulkCreateOneToMany()
        {
            var students = testDataFactory.GetStudents(500);
            db.Students.AddRange(students);

            Class @class = testDataFactory.GetClass();
            @class.Students = new List<Student>(students);
            db.Classes.Add(@class);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkCreateOneToOne()
        {
            var indexes = testDataFactory.GetIndexes(500);
            db.Indexes.AddRange(indexes);

            var students = testDataFactory.GetStudents(500);
            for (int i = 0; i < students.Count; i++)
                students[i].Index = indexes[i];

            db.Students.AddRange(students);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkCreateWithoutRelationship()
        {
            var classes = testDataFactory.GetClasses(500);
            db.Classes.AddRange(classes);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkDeleteManyToMany()
        {
            var students = testDataFactory.GetStudents(3);
            db.Students.AddRange(students);

            var subjects = testDataFactory.GetSubjects(3);
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

        public TimeSpan BulkDeleteWithoutRelationship()
        {
            var classes = testDataFactory.GetClasses(500);
            db.Classes.AddRange(classes);
            db.SaveChanges();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.Classes.RemoveRange(classes);
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan BulkDeleteOneToMany()
        {
            var students = testDataFactory.GetStudents(500);
            db.Students.AddRange(students);

            Class @class = testDataFactory.GetClass();
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

        public TimeSpan BulkDeleteOneToOne()
        {
            var indexes = testDataFactory.GetIndexes(500);
            db.Indexes.AddRange(indexes);

            var students = testDataFactory.GetStudents(500);
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

        public TimeSpan BulkUpdateManyToMany()
        {
            var students = testDataFactory.GetStudents(3);
            db.Students.AddRange(students);

            var subjects = testDataFactory.GetSubjects(3);
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

        public TimeSpan BulkUpdateOneToMany()
        {
            var students = testDataFactory.GetStudents(500);
            db.Students.AddRange(students);

            Class @class = testDataFactory.GetClass();
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

        public TimeSpan BulkUpdateOneToOne()
        {
            var indexes = testDataFactory.GetIndexes(500);
            db.Indexes.AddRange(indexes);

            var students = testDataFactory.GetStudents(500);
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

        public TimeSpan BulkUpdateWithoutRelationship()
        {
            var classes = testDataFactory.GetClasses(500);
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
            Student student = testDataFactory.GetStudent();
            db.Students.Add(student);

            Subject subject = testDataFactory.GetSubject();
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
            Student student = testDataFactory.GetStudent();
            db.Students.Add(student);

            Class @class = testDataFactory.GetClass();
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
            Index index = testDataFactory.GetIndex();
            db.Indexes.Add(index);

            Student student = testDataFactory.GetStudent();
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
            Class c = testDataFactory.GetClass();
            db.Classes.Add(c);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            db.SaveChanges();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan SingleDeleteManyToMany()
        {
            Student student = testDataFactory.GetStudent();
            db.Students.Add(student);

            Subject subject = testDataFactory.GetSubject();
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
            Student student = testDataFactory.GetStudent();
            db.Students.Add(student);

            Class @class = testDataFactory.GetClass();
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
            Index index = testDataFactory.GetIndex();
            db.Indexes.Add(index);

            Student student = testDataFactory.GetStudent();
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
            Class @class = testDataFactory.GetClass();
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
            Student student = testDataFactory.GetStudent();
            db.Students.Add(student);

            Subject subject = testDataFactory.GetSubject();
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
            Student student = testDataFactory.GetStudent();
            db.Students.Add(student);

            Class @class = testDataFactory.GetClass();
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
            Index index = testDataFactory.GetIndex();
            db.Indexes.Add(index);

            Student student = testDataFactory.GetStudent();
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
            Class @class = testDataFactory.GetClass();
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
    }
}
