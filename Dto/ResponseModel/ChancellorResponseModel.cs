namespace UniversityManagementMvc.Dto.ResponseModel
{
    public class ChancellorResponseModel : BaseResponseModel
    {
        public ChancellorDto ChancellorDto { get; set; }
    }
    public class ChancellorsResponseModel : BaseResponseModel
    {
        public IList<ChancellorDto> ChancellorDtos { get; set; }
    }
}