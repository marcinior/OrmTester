namespace OrmTesterLib.nHibernate.entity
{
    class StudentSubject
    {
        public virtual int Id { get; set; }
        public virtual Student StudentId { get; set; }
        public virtual Subject SubjectId { get; set; }
    }
}
