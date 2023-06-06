using UniversityManagementMvc.Data.Entities;

namespace UniversityManagementMvc.Data.InheritEntities
{
    public class AuditableEntity:BaseEntity,IAuditableEntity
    {
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get ; set; }
        public int LastModifiedBy { get ; set ; }
        public DateTime? LastModifiedOn { get ; set ; }
        public DateTime? DeletedOn { get ; set; }
        public int? DeletedBy { get ; set ; }
        public bool? IsDeleted { get ; set ; }
        DateTime? IAuditableEntity.CreatedOn {get;set;}
        DateTime? IAuditableEntity.LastModifiedOn { get; set; }

    }
}