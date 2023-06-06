using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Dto.RequestModel;
using UniversityManagementMvc.Dto.ResponseModel;

namespace UniversityManagementMvc.Interface.Services
{
    public interface IUserServices
    {
        BaseResponseModel AddUser(CreateUserRequestModel model);
        Task<BaseResponseModel>  DeleteUser(int id);
        Task<UserResponseModel> Login(string email, string passWord);
        Task<BaseResponseModel> EditUser(UpdateUserRequestModel requestModel, int id);
        Task<UserResponseModel> ViewUser(int Id);
        UserResponseModel ViewAllUsers();
        // public User ApproveUser(int userId);
    }
}