using EntityFramework.Entity;
using System;
using System.Collections.Generic;

namespace EntityFramework
{
    internal class TestDataFactory
    {
        private Random random;

        public TestDataFactory()
        {
            random = new Random();
        }

        public Index GetIndex() => new Index
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IndexNumber = (char)random.Next(65,90) + random.Next(100000, 999999).ToString()
        };

        public Class GetClass() => new Class
        {
            GroupNumber = Convert.ToByte(random.Next(1, 10)),
            Year = Convert.ToByte(random.Next(1, 5)),
            DegreeCourse = "Informatyka",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        public List<Class> GetClasses(int count)
        {
            List<Class> classes = new List<Class>();
            for (int i = 0; i < count; i++)
                classes.Add(GetClass());

            return classes;
        }

        public Student GetStudent() => new Student
        {
            FirstName = "Jan",
            LastName = "Nowak",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            BirthDate = DateTime.Now.AddYears(-20),
            Gender = Gender.FEMALE,
            Pesel = random.Next(100000, 999999) +  random.Next(10000, 99999).ToString()
        };

        public Subject GetSubject() => new Subject
        {
            SubjectName = "dfsdfsd",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Ects = Convert.ToByte(random.Next(1, 8)),
            ExamType = ExamType.EXAM,
            ClassYear = Convert.ToByte(random.Next(1, 5)),
        };

        public List<Index> GetIndexes(int count)
        {
            List<Index> indexes = new List<Index>();
            for (int i = 0; i < count; i++)
                indexes.Add(GetIndex());

            return indexes;
        }

        public List<Student> GetStudents(int count)
        {
            List<Student> students = new List<Student>();
            for (int i = 0; i < count; i++)
                students.Add(GetStudent());

            return students;
        }

        public List<Subject> GetSubjects(int count)
        {
            List<Subject> subjects = new List<Subject>();
            for (int i = 0; i < count; i++)
                subjects.Add(GetSubject());

            return subjects;
        }
    }
}
