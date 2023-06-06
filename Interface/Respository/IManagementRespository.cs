using System.Linq.Expressions;
using UniversityManagementMvc.Data.Entities;

namespace UniversityManagementMvc.Interface.Respository
{
    public interface IManagementRespository
    {
        Management CreateManagement(Management management);
        // Management UpdateManagement (Management management);
        Task<Management> Login(string email, string passWord);
        Task<Management> GetManagement(int id);
        Task<Management> UpdateManagement(Management management);
        Task<Management> GetManagement(Expression<Func<Management, bool>> expression);

    }
}