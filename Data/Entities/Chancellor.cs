using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UniversityManagementMvc.Data.Enum;
using UniversityManagementMvc.Data.InheritEntities;

namespace UniversityManagementMvc.Data.Entities
{
    public class Chancellor:AuditableEntity
    {
         [Required]
        // public string Name{get;set;}
        
        // public string PhoneNumber{get;set;}
        public DateTime DateOfBirth{get;set;}
        public string NextOfKin { get; set; }
        public Gender Gender{get;set;}
         [DisplayName("Marital Status")]
        [Required]
        public Status MaritalStatus { get; set; }
        public byte[] ProfilePicture{get;set;}
        public User User{get;set;}
        public int UserId{get;set;}
        // public Wallet Wallet { get; set; }
        // [Key]
        public string ChancellorId{get;set;} = $"Dr{Guid.NewGuid().ToString().Replace("_","").Substring(0,6)}";
        public bool IsApproved{get;set;}=false;
        
        // public IFormFile Qualification{get;set;}
    }
}