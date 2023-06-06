using UniversityManagementMvc.Data.InheritEntities;

namespace UniversityManagementMvc.Data.Entities
{
    public class Management : AuditableEntity
    {
        // public string Name { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public byte[] ProfilePicture{get;set;}
        // public string PhoneNumber { get; set; }
        // public Wallet Wallet { get; set; }

    }
}