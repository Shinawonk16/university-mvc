using System.Linq.Expressions;
using UniversityManagementMvc.Data.Entities;

namespace UniversityManagementMvc.Interface.Respository
{
    public interface IChancellorRespository
    {
           Chancellor CreateChancellor(Chancellor chancellor);
        Task<Chancellor> UpdateChancellor (Chancellor chancellor);
        Task<Chancellor> Login(string email,string passWord);
        Task<Chancellor> GetChancellor(string chancellorId);
        
        Task<Chancellor> GetChancellor(Expression<Func<Chancellor, bool>> expression);
     
        List<Chancellor> GetAllChancellor();
        Task<bool> DeleteChancellor(Chancellor chancellor);
    }
}