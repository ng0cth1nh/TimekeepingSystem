using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.EntityFramework
{
    public partial class TimekeepingDbContext : DbContext
    {
        public TimekeepingDbContext()
            : base("name=Timekeeping")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AttendanceDaily> AttendanceDailies { get; set; }
        public virtual DbSet<AttendanceMonthly> AttendanceMonthlies { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<CompleteTag> CompleteTags { get; set; }
        public virtual DbSet<CompleteTagDetail> CompleteTagDetails { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Step> Steps { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttendanceMonthly>()
                .HasMany(e => e.AttendanceDailies)
                .WithRequired(e => e.AttendanceMonthly)
                .HasForeignKey(e => e.MonthID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Color>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Color)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompleteTag>()
                .Property(e => e.Table)
                .IsFixedLength();

            modelBuilder.Entity<CompleteTag>()
                .HasMany(e => e.CompleteTagDetails)
                .WithRequired(e => e.CompleteTag)
                .HasForeignKey(e => e.TagID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompleteTag>()
                .HasOptional(e => e.Tag)
                .WithRequired(e => e.CompleteTag);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AttendanceDailies)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CompleteTagDetails)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeRole>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.EmployeeRole)
                .HasForeignKey(e => e.RoleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeType>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.EmployeeType)
                .HasForeignKey(e => e.TypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Steps)
                .WithMany(e => e.Products)
                .Map(m => m.ToTable("ProductDetail").MapLeftKey("ProductID").MapRightKey("StepID"));

            modelBuilder.Entity<Size>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Size)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Step>()
                .HasMany(e => e.CompleteTagDetails)
                .WithRequired(e => e.Step)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.CompleteTagDetails)
                .WithRequired(e => e.Tag)
                .WillCascadeOnDelete(false);
        }
    }
}
