namespace UniversityManagementMvc.Dto.ResponseModel
{
    public class LecturerResponseModel : BaseResponseModel

    {
        public LecturerDto LecturerDto { get; set; }
    }
    public class LecturersResponseModel : BaseResponseModel
    {
        public ICollection<LecturerDto> LecturerDtos { get; set; }
    }
}