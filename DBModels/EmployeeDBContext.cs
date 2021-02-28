using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAPI.DBModels
{
    public partial class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext()
        {
        }

        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Petition> Petitions { get; set; }
        public virtual DbSet<Signature> Signatures { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS01; Initial Catalog=EmployeeDB; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).ValueGeneratedOnAdd();

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Employee");

                entity.Property(e => e.DateOfJoining).HasColumnType("date");

                entity.Property(e => e.Department)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId).ValueGeneratedOnAdd();

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoFileName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Petition>(entity =>
            {
                entity.ToTable("Petition");

                entity.Property(e => e.PetitionId).HasColumnName("petition_id");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ClosingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("closing_date");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.PhotoFilename)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("photo_filename");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Petitions)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Petition__author__2C3393D0");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Petitions)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Petition__catego__2D27B809");
            });

            modelBuilder.Entity<Signature>(entity =>
            {
                entity.HasKey(e => new { e.SignatoryId, e.PetitionId })
                    .HasName("PK__Signatur__9565AAB960BADF73");

                entity.ToTable("Signature");

                entity.Property(e => e.SignatoryId).HasColumnName("signatory_id");

                entity.Property(e => e.PetitionId).HasColumnName("petition_id");

                entity.Property(e => e.SignedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("signed_date");

                entity.HasOne(d => d.Petition)
                    .WithMany(p => p.Signatures)
                    .HasForeignKey(d => d.PetitionId)
                    .HasConstraintName("FK__Signature__petit__30F848ED");

                entity.HasOne(d => d.Signatory)
                    .WithMany(p => p.Signatures)
                    .HasForeignKey(d => d.SignatoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Signature__signa__300424B4");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.AuthToken, "UQ__Users__081E0F6410FE901B")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__AB6E61648C7506CB")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AuthToken)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("auth_token");

                entity.Property(e => e.City)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhotoFilename)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("photo_filename");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
