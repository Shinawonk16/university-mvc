using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UniversityManagementMvc.Data.Enum;
using UniversityManagementMvc.Data.InheritEntities;

namespace UniversityManagementMvc.Data.Entities
{
    public class Student : AuditableEntity
    {

         public DateTime DateOfBirth{get;set;}

          [DisplayName("Marital Status")]
        [Required]
        public Status MaritalStatus { get; set; }

        public string NextOfKin { get; set; }
        public Gender Gender{get;set;}
        public User User { get; set; }
        public int UserId { get; set; }
        public byte[] ProfilePicture{get;set;}
        public string MatricNumber { get; set; } 
        public bool IsApproved { get; set; } = false;
        public byte[] OLevelResult { get; set; }
    }
}