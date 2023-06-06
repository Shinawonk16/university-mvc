using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UniversityManagementMvc.Data.ContextClass;
using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Interface.Respository;

namespace UniversityManagementMvc.Implementation.Respository
{
    public class ManagementRespository : IManagementRespository
    {
        private readonly ApplicationContext _contextClass;
        public ManagementRespository(ApplicationContext context)
        {
            _contextClass = context;
        }

        public Management CreateManagement(Management management)
        {
           _contextClass.Managements.Add(management);
           _contextClass.SaveChanges();
           return management;
        }

        public Task<Management> GetManagement(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Management> GetManagement(Expression<Func<Management, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<Management> Login(string email, string passWord)
        {
            return await _contextClass.Managements.Where(x => x.User.Email == email && x.User.PassWord == passWord).SingleOrDefaultAsync();
        }

         public async Task<Management> UpdateManagement(Management management)
        {
            _contextClass.Managements.Update(management);
           await _contextClass.SaveChangesAsync();
            return management;
        }

       
      
    }
}