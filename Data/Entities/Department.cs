using UniversityManagementMvc.Data.InheritEntities;

namespace UniversityManagementMvc.Data.Entities
{
    public class Department : AuditableEntity
    {
        
        public string DepartmentName { get; set; }

    }
}