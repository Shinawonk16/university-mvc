using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Dto.RequestModel;
using UniversityManagementMvc.Dto.ResponseModel;

namespace UniversityManagementMvc.Interface.Services
{
    public interface IStudentService
    {
        BaseResponseModel AddOldStudent(CreateOldStudentRequest model);
        BaseResponseModel AddStudent(CreateStudentRequestModel model);
        Task<BaseResponseModel> DeleteStudent(string matricNumber);
        // Student Login(string email, string passWord);
        Task<BaseResponseModel> EditStudent(UpdateStudentRequestModel requestModel, string studentId);
        Task<StudentResponseModel> ViewStudent(string matricNumber);
        StudentsResponseModel ViewAllStudents();
               Task<Student> Login(string email, string passWord);

        public  Task<BaseResponseModel> ApproveStudent(string matricNumber);
    }
}