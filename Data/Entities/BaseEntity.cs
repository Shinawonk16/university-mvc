using System.ComponentModel.DataAnnotations;

namespace UniversityManagementMvc.Data.Entities
{
    public class BaseEntity
    {
         [Key]
        public int Id { get; set; }
      
    }
}