using EntityFramework.Entity;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class EfDbContext : DbContext
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OrmTesterEfDb;Integrated Security=True";

        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options) {}

        public EfDbContext() : base()
        {
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>()
                .HasIndex(s => s.Pesel)
                .IsUnique(true);

            builder.Entity<Student>()
                .HasOne(s => s.Index)
                .WithOne(i => i.Student)
                .HasForeignKey<Student>(s => s.IndexForeignKey)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Index>()
                .HasIndex(i => i.IndexNumber)
                .IsUnique(true);

            builder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<StudentSubject>()
                .HasOne(ss => ss.Subject)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<StudentSubject>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Index> Indexes { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentSubject> StudentSubjects { get; set; }
    }
}
