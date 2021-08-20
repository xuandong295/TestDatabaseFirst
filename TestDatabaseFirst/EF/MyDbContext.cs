using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TestDatabaseFirst.Models
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Score> Scores { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
/*            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=123qweasd;database=studentmanagement", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.26-mysql"));
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("class");

                entity.Property(e => e.Classid)
                    .ValueGeneratedNever()
                    .HasColumnName("classid");

                entity.Property(e => e.Majors)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("majors");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Score>(entity =>
            {
                entity.HasKey(e => new { e.Studentid, e.Subjectid })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("score");

                entity.HasIndex(e => e.Studentid, "studentid_idx");

                entity.HasIndex(e => e.Subjectid, "subjectid_idx_fk");

                entity.Property(e => e.Studentid).HasColumnName("studentid");

                entity.Property(e => e.Subjectid).HasColumnName("subjectid");

                entity.Property(e => e.Firstscore).HasColumnName("firstscore");

                entity.Property(e => e.Secondscore).HasColumnName("secondscore");

                entity.Property(e => e.Semester).HasColumnName("semester");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Scores)
                    .HasForeignKey(d => d.Studentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("studentid_idx_fk");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Scores)
                    .HasForeignKey(d => d.Subjectid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("objectid_idx_fk");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.HasIndex(e => e.Class, "class_idx");

                entity.Property(e => e.Studentid)
                    .ValueGeneratedNever()
                    .HasColumnName("studentid");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.Class).HasColumnName("class");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("gender");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.HasOne(d => d.ClassNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.Class)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("class_idx_fk");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subject");

                entity.Property(e => e.Subjectid)
                    .ValueGeneratedNever()
                    .HasColumnName("subjectid");

                entity.Property(e => e.Credits).HasColumnName("credits");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
