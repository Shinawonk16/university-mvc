using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Dto.RequestModel;
using UniversityManagementMvc.Dto.ResponseModel;

namespace UniversityManagementMvc.Interface.Services
{
    public interface IChancellorService
    {
        BaseResponseModel AddChancellor(CreateChancellorRequestModel model);
        Task<BaseResponseModel> DeleteChancellor(string chancellorId);
        Task<Chancellor> Login(string email,string passWord);
        Task<BaseResponseModel> EditChancellor(UpdateChancellorRequestModel requestModel, string chancellorId);
        Task<ChancellorResponseModel> ViewChancellor(string chancellorId);
        ChancellorsResponseModel ViewAllChancellors();
        public Task<BaseResponseModel> ApproveChancellor(string chancellorId);
    }
}