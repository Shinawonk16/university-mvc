using UniversityManagementMvc.Data.InheritEntities;

namespace UniversityManagementMvc.Data.Entities
{
    public class LecturerCourse : AuditableEntity
    {
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}