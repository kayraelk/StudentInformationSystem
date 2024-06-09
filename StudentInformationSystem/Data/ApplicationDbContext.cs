using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCode> CourseCodes { get; set; }
        public DbSet<Unit> Units { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Unit>()
                .HasMany(u => u.ChildUnits)
                .WithOne(u => u.ParentUnit)
                .HasForeignKey(u => u.UnitId);

            modelBuilder.Entity<Unit>()
                .HasMany(u => u.Courses)
                .WithOne(c => c.Unit)
                .HasForeignKey(c => c.UnitId);

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
