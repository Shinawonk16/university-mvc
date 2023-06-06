using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Dto.RequestModel;
using UniversityManagementMvc.Dto.ResponseModel;

namespace UniversityManagementMvc.Interface.Services
{
    public interface ILecturerService
    {
         BaseResponseModel AddLecturer(CreateLecturerRequestModel model);
        Task<BaseResponseModel> DeleteLecturer(string lecturerId);
        Task<Lecturer> Login(string email,string passWord);
        Task<BaseResponseModel> EditLecturer(UpdateLecturerRequestModel requestModel, string lecturerId);
        Task<LecturerResponseModel> ViewLecturer(string lecturerId);
        LecturersResponseModel ViewAllLecturers();
        public  Task<BaseResponseModel> ApproveLecturer(string lecturerId);
    }
}