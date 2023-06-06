using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Dto.RequestModel;
using UniversityManagementMvc.Dto.ResponseModel;

namespace UniversityManagementMvc.Interface.Services
{
    public interface IManagementService
    {
        BaseResponseModel AddManagement(CreateManagementRequestModel model);
        Task<Management> Login(string email,string passWord);
            Task<BaseResponseModel> EditManagement(UpdateManagementReequestModel requestModel, int id);
        Task<ManagementResponseModel> ViewManagement(int id);
     
    }
}