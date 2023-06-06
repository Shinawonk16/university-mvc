using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UniversityManagementMvc.Data.Enum;
using UniversityManagementMvc.Data.InheritEntities;

namespace UniversityManagementMvc.Data.Entities
{
    public class User : AuditableEntity
    {
        [DisplayName("FirstName")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("LastName")]
        [Required]
        public string LastName { get; set; }
        [DisplayName("Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [DisplayName("Password")]
        [Required]
        public string PassWord { get; set; }
        public bool IsActive{get;set;}
        public string Address{get;set;}
        public Student Students { get; set; }
        public string Role{get;set;}
        public Management Managements { get; set; }
        public Chancellor Chancellors { get; set; }
        public Lecturer Lecturers { get; set; }
    }
}