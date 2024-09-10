using IUR_Backend.Models;
using IUR_Backend.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IUR_Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
        .HasDiscriminator<string>("Discriminator")
        .HasValue<ApplicationUser>("ApplicationUser")
        .HasValue<Student>("Student")
        .HasValue<Teacher>("Teacher");

            // Configura StudentCourse
            builder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.ApplicationUserId, sc.CourseId });

            builder.Entity<StudentCourse>()
                .HasOne(sc => sc.ApplicationUser)
                .WithMany(u => u.StudentCourses)
                .HasForeignKey(sc => sc.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configura StudentExam
            builder.Entity<StudentExam>()
                .HasKey(se => new { se.ApplicationUserId, se.ExamId });

            builder.Entity<StudentExam>()
                .HasOne(se => se.ApplicationUser)
                .WithMany(u => u.StudentExams)
                .HasForeignKey(se => se.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<StudentExam>()
                .HasOne(se => se.Exam)
                .WithMany(e => e.StudentExams)
                .HasForeignKey(se => se.ExamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configura Grade
            builder.Entity<Grade>()
                .HasOne(g => g.ApplicationUser)
                .WithMany()
                .HasForeignKey(g => g.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Grade>()
                .HasOne(g => g.Exam)
                .WithMany()
                .HasForeignKey(g => g.ExamId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configura UserRole
            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId);

                userRole.HasOne(ur => ur.User)
                        .WithMany(u => u.UserRoles)
                        .HasForeignKey(ur => ur.UserId);
            });
        }
    }
}
