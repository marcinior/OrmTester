using System;

namespace OrmTesterLib.nHibernate.entity
{
    class Student
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public string Pesel { get; set; }

        public int IndexId { get; set; }

        public int ClassId { get; set; }
    }
}
