using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UniversityManagementMvc.Data.ContextClass;
using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Interface.Respository;

namespace UniversityManagementMvc.Implementation.Respository
{
    public class UserRespository:IUserRespository
    {
        private readonly ApplicationContext _contextClass;
        public UserRespository(ApplicationContext Context)
        {
            _contextClass = Context;
        }

        public User CreateUser(User user)
        {
             _contextClass.Users.Add(user);
            _contextClass.SaveChanges();
            return user;
        }

        public async Task<bool> DeleteUser(User user)
        {
              _contextClass.Users.Remove(user);
            await _contextClass.SaveChangesAsync();
            return true;
        }

        public List<User> GetAllUser()
        {
            return _contextClass.Users.ToList();
        }

        public async Task<User> GetUser(int id)
        {
            return await _contextClass.Set<User>().FindAsync(id);
        }

        public async Task<User> GetUser(Expression<Func<User, bool>> expression)
        {
            return await _contextClass.Set<User>().SingleOrDefaultAsync(expression);

        }

        // public async Task<User> Login(string email, string passWord)
        // {
        //       var user = await _contextClass.Users
        //       .Include(x => x.Chancellors)
        //       .Include(x => x.Lecturers)
        //       .Include(x => x.Students)
        //       .Include(x => x.Managements)
        //       .SingleOrDefaultAsync(x => x.Email == email && x.PassWord == passWord);
        //       return user;
        // }


        public async Task<User> Login(string email, string passWord)
        {
              var user = await _contextClass.Users.FirstOrDefaultAsync(x => x.Email == email && x.PassWord == passWord);
              return user;
        }

        public async Task<User> UpdateUser(User user)
        {
             _contextClass.Users.Update(user);
          await  _contextClass.SaveChangesAsync();
            return user;
        }
    }
}
