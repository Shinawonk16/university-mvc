using UniversityManagementMvc.Data.Enum;

namespace UniversityManagementMvc.Dto.RequestModel
{
    public class CreateOldStudentRequest
    {
           public string FirstName{get;set;}
           public string LastName {get;set;}
        public string Email{get;set;}
         public string NextOfKin{get;set;}
         public string Address{get;set;}
        //  public DateTime DateOfBirth{get;set;}
        public byte[] ProfilePicture{get;set;}
         public DateTime DateOfBirth{get;set;}
         public Status MaritalStatus{get;set;} 
        public string MatricNumber{get;set;}
        public Gender Gender{get;set;}
        public string PassWord{get;set;}
        public string Role{get;set;}
        public string PhoneNumber { get; set; } 
    }
}