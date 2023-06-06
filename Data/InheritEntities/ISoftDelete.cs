namespace UniversityManagementMvc.Data.InheritEntities
{
    public interface ISoftDelete
    {
        DateTime? DeletedOn { get; set; }
        int? DeletedBy { get; set; }
        bool? IsDeleted { get; set; }
    }
}