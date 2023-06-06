using UniversityManagementMvc.Data.Enum;

namespace UniversityManagementMvc.Dto
{
    public class StudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
         public string Role { get; set; }
        public string PhoneNumber { get; set; }
         public DateTime DateOfBirth{get;set;}
        public Status MaritalStatus { get; set; }
        public Gender Gender { get; set; }
        public byte[] ProfilePicture{get;set;}
        public DateTime? CreatedOn { get; set; }
        public string Email { get; set; }
         public string NextOfKin{get;set;}
        public string MatricNumber { get; set; }
    }
}