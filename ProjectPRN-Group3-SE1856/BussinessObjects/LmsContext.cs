using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BussinessObjects;

public partial class LmsContext : DbContext
{
    public LmsContext()
    {
    }

    public LmsContext(DbContextOptions<LmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<BlogNews> BlogNews { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseType> CourseTypes { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Forum> Forums { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-J6PLHHB;Database=LMS;User ID=sa;Password=1234567890;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Assignme__32499E571125442E");

            entity.ToTable("Assignment");

            entity.Property(e => e.AssignmentId)
                .ValueGeneratedNever()
                .HasColumnName("AssignmentID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Course).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Assignmen__Cours__440B1D61");
        });

        modelBuilder.Entity<BlogNews>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__BlogNews__AA12603812F3CE88");

            entity.Property(e => e.PostId)
                .ValueGeneratedNever()
                .HasColumnName("PostID");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.BlogNews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__BlogNews__UserID__49C3F6B7");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__C92D718730F5196A");

            entity.ToTable("Course");

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("CourseID");
            entity.Property(e => e.CourseName).HasMaxLength(100);
            entity.Property(e => e.CourseTypeId).HasColumnName("CourseTypeID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.CourseType).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CourseTypeId)
                .HasConstraintName("FK__Course__CourseTy__3E52440B");

            entity.HasOne(d => d.Department).WithMany(p => p.Courses)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Course__Departme__3D5E1FD2");
        });

        modelBuilder.Entity<CourseType>(entity =>
        {
            entity.HasKey(e => e.CourseTypeId).HasName("PK__CourseTy__81736952935FD167");

            entity.ToTable("CourseType");

            entity.Property(e => e.CourseTypeId)
                .ValueGeneratedNever()
                .HasColumnName("CourseTypeID");
            entity.Property(e => e.CourseTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCDEE1AB689");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.HeadId).HasColumnName("HeadID");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF6F72D8E651");

            entity.ToTable("Document");

            entity.Property(e => e.DocumentId)
                .ValueGeneratedNever()
                .HasColumnName("DocumentID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Course).WithMany(p => p.Documents)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Document__Course__534D60F1");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__7F6877FBE64735A1");

            entity.ToTable("Enrollment");

            entity.Property(e => e.EnrollmentId)
                .ValueGeneratedNever()
                .HasColumnName("EnrollmentID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Enrollmen__Cours__412EB0B6");
        });

        modelBuilder.Entity<Forum>(entity =>
        {
            entity.HasKey(e => e.ForumId).HasName("PK__Forum__E210AC4F14FEACF0");

            entity.ToTable("Forum");

            entity.Property(e => e.ForumId)
                .ValueGeneratedNever()
                .HasColumnName("ForumID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Course).WithMany(p => p.Forums)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Forum__CourseID__4CA06362");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Post__AA1260384422FD6D");

            entity.ToTable("Post");

            entity.Property(e => e.PostId)
                .ValueGeneratedNever()
                .HasColumnName("PostID");
            entity.Property(e => e.ForumId).HasColumnName("ForumID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Forum).WithMany(p => p.Posts)
                .HasForeignKey(d => d.ForumId)
                .HasConstraintName("FK__Post__ForumID__4F7CD00D");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Post__UserID__5070F446");
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("PK__Submissi__449EE10554A8D8BC");

            entity.ToTable("Submission");

            entity.Property(e => e.SubmissionId)
                .ValueGeneratedNever()
                .HasColumnName("SubmissionID");
            entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");
            entity.Property(e => e.Grade).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Assignment).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.AssignmentId)
                .HasConstraintName("FK__Submissio__Assig__46E78A0C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCAC50333617");

            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
