using Microsoft.EntityFrameworkCore;
using UniversityManagementMvc.Data.Entities;

namespace UniversityManagementMvc.Data.ContextClass
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        public DbSet<Management> Managements
        {
            get; set;
        }
        public DbSet<Student> Students
        {
            get; set;
        }
        public DbSet<Chancellor> Chancellors
        {
            get; set;
        }
        public DbSet<Lecturer> Lecturers
        {
            get; set;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<LecturerCourse> LecturerCourses { get; set; }
        public DbSet<Department> Departments { get; set; }


    }
}