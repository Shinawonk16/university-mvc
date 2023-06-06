using UniversityManagementMvc.Data.InheritEntities;

namespace UniversityManagementMvc.Data.Entities
{
    public class Course : AuditableEntity
    {
        public string CourseName { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
        public string CourseNum { get; set; } = Guid.NewGuid().ToString().Replace("_", "").Substring(0, 2);
        public ICollection<LecturerCourse> LecturerCourses{get;set;} = new HashSet<LecturerCourse>();
    }
}