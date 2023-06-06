namespace UniversityManagementMvc.Dto.RequestModel
{
    public class UpdateManagementReequestModel
    { public string FirstName { get; set; }
        public string LastName{get;set;}
        public string Email { get; set; }
        public string Address{get;set;}
        public string PassWord{get;set;}
        public string PhoneNumber{get;set;}
        public byte[] ProfilePicture{get;set;}
    }
}