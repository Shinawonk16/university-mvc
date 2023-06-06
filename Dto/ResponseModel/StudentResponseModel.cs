namespace UniversityManagementMvc.Dto.ResponseModel
{
    public class StudentResponseModel : BaseResponseModel
    {
        public StudentDto StudentDto { get; set; }
    }
    public class StudentsResponseModel : BaseResponseModel
    {
        public ICollection<StudentDto> StudentDto { get; set; }
    }
}