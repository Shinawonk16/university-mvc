namespace UniversityManagementMvc.Dto.ResponseModel
{
    public class ManagementResponseModel : BaseResponseModel
    {
        public ManagementDto ManagementDto { get; set; }
    }
    public class ManagementsResponseModel:BaseResponseModel
    {
        public ICollection<ManagementDto> ManagementDtos{get;set;}
    }
}