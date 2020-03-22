using EntityFramework.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFramework
{
    public class EfDbContext : DbContext
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EntityFramework;Integrated Security=True";

        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options) { }

        public EfDbContext() : base()
        {
            this.Database.Migrate();
        }

        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[] {new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()});

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>()
                .HasOne(s => s.Index)
                .WithOne(i => i.Student)
                .HasForeignKey<Student>(s => s.IndexForeignKey)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

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
