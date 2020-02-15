using NHibernateTester.Entities;
using System;
using System.Collections.Generic;
using OrmTesterLib.Generators;
using NHibernateTester.Enums;

namespace NHibernateTester
{
    class NHibernateDataGenerator
    {
        public static List<Student> GetStudents(int count)
        {
            var students = new List<Student>();
            for (var i = 0; i < count; i++)
            {
                var student = GetStudent();
                students.Add(student);
            }
            return students;
        }

        public static Student GetStudent()
        {
            var now = DateTime.Now;
            var birthDate = DateTime.Now;
            birthDate.AddYears(-20);
            return new Student
            {
                FirstName = DataGenerator.GenerateRandomString(),
                LastName = DataGenerator.GenerateRandomString(),
                CreatedAt = now,
                UpdatedAt = now,
                BirthDate = birthDate,
                Gender = now.Millisecond % 2 == 0 ? Enums.Gender.FEMALE : Enums.Gender.MALE,
                Pesel = DataGenerator.GeneratePesel()
            };
        }

        public static List<Index> GetIndices(int count)
        {
            var indices = new List<Index>();
            for(var i = 0; i < count; i++)
            {
                var index = GetIndex(i + 1);
                indices.Add(index);
            }
            return indices;
        }

        public static Index GetIndex(int index)
        {
            var now = DateTime.Now;
            return new Index
            {
                IndexNumber = index.ToString().PadLeft(7, '0'),
                CreatedAt = now,
                UpdatedAt = now
            };
        }

        public static List<Class> GetClasses(int count)
        {
            var classes = new List<Class>();
            for(var i = 0; i<count; i++)
            {
                var @class = GetClass();
                classes.Add(@class);
            }
            return classes;
        }

        public static Class GetClass()
        {
            var now = DateTime.Now;
            var random = new Random();
            return new Class
            {
                DegreeCourse = DataGenerator.GenerateRandomString(),
                GroupNumber = (byte)random.Next(1, 9),
                Year = (byte)random.Next(1, 5),
                CreatedAt = now,
                UpdatedAt = now
            };
        }

        public static List<Subject> GetSubjects(int count)
        {
            var subjects = new List<Subject>();
            for(var i = 0; i< count; i++)
            {
                var subject = GetSubject();
                subjects.Add(subject);
            }
            return subjects;
        }

        public static Subject GetSubject()
        {
            var random = new Random();
            var now = DateTime.Now;
            return new Subject
            {
                ClassesYear = (byte)random.Next(1, 5),
                Ects = (byte)random.Next(1, 5),
                SubjectName = DataGenerator.GenerateRandomString(),
                ExamType = (ExamType)random.Next(0, 2),
                CreatedAt = now,
                UpdatedAt = now
            };
        }
    }
}
