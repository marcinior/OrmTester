﻿// <auto-generated />
using System;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFramework.Migrations
{
    [DbContext(typeof(EfDbContext))]
    partial class EfDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EntityFramework.Entity.Class", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("DegreeCourse")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<byte>("GroupNumber");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<byte>("Year");

                    b.HasKey("ClassId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("EntityFramework.Entity.Index", b =>
                {
                    b.Property<int>("IndexId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("IndexNumber")
                        .IsRequired()
                        .HasMaxLength(7);

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("IndexId");

                    b.ToTable("Indexes");
                });

            modelBuilder.Entity("EntityFramework.Entity.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<int?>("ClassId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("Gender");

                    b.Property<int?>("IndexForeignKey");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("Pesel")
                        .HasMaxLength(11);

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("StudentId");

                    b.HasIndex("ClassId");

                    b.HasIndex("IndexForeignKey")
                        .IsUnique()
                        .HasFilter("[IndexForeignKey] IS NOT NULL");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("EntityFramework.Entity.StudentSubject", b =>
                {
                    b.Property<int>("StudentSubjectId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StudentId");

                    b.Property<int>("SubjectId");

                    b.HasKey("StudentSubjectId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("StudentSubjects");
                });

            modelBuilder.Entity("EntityFramework.Entity.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("ClassYear");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<byte>("Ects");

                    b.Property<int>("ExamType");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("SubjectId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("EntityFramework.Entity.Student", b =>
                {
                    b.HasOne("EntityFramework.Entity.Class", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EntityFramework.Entity.Index", "Index")
                        .WithOne("Student")
                        .HasForeignKey("EntityFramework.Entity.Student", "IndexForeignKey")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("EntityFramework.Entity.StudentSubject", b =>
                {
                    b.HasOne("EntityFramework.Entity.Student", "Student")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EntityFramework.Entity.Subject", "Subject")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
