using System.Linq.Expressions;
using UniversityManagementMvc.Data.Entities;

namespace UniversityManagementMvc.Interface.Respository
{
    public interface IUserRespository
    {
        User CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> Login(string email, string passWord);
        Task<User> GetUser(int id);
        Task<User> GetUser(Expression<Func<User, bool>> expression);
        List<User> GetAllUser();
        Task<bool> DeleteUser(User user);
    }
}