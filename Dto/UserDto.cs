using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Data.Enum;

namespace UniversityManagementMvc.Dto
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string Password { get; set; }
        public string Address{get;set;}
        public bool IsActive{get;set;}
        public bool? IsDeleted{get;set;}
        public string Role { get; set; }
        
        public byte[] ProfilePicture{get;set;}
        public Student Students { get; set; }
        public Management Managements { get; set; }
        public Chancellor Chancellors { get; set; }
        public Lecturer Lecturers { get; set; }
           public string Message { get; set; }
    }
}