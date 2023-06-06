using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UniversityManagementMvc.Data.ContextClass;
using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Interface.Respository;

namespace UniversityManagementMvc.Implementation.Respository
{
    public class ChancellorRespository : IChancellorRespository
    {
        
        private readonly ApplicationContext _contextClass;
        public ChancellorRespository(ApplicationContext context)
        {
            _contextClass = context;
        }
        public Chancellor CreateChancellor(Chancellor chancellor)
        {
            _contextClass.Chancellors.Add(chancellor);
            _contextClass.SaveChanges();
            return chancellor;
        }

        public async Task<bool> DeleteChancellor(Chancellor chancellor)
        {
            _contextClass.Set<Chancellor>().Update(chancellor);
            await _contextClass.SaveChangesAsync();
            return  true;

        }

        public List<Chancellor> GetAllChancellor()
        {
            return _contextClass.Chancellors.ToList();
        }

        public async Task<Chancellor> GetChancellor(string chancellorId)
        {
         return await _contextClass.Set<Chancellor>().FindAsync(chancellorId);
        }

        public async Task<Chancellor> GetChancellor(Expression<Func<Chancellor, bool>> expression)
        {
            return await _contextClass.Set<Chancellor>().SingleOrDefaultAsync(expression);
        }

        public async Task<Chancellor> Login(string email, string passWord)
        {
           return await _contextClass.Chancellors.Where(x => x.User.Email == email && x.User.PassWord == passWord).SingleOrDefaultAsync();
        }

        public async Task<Chancellor> UpdateChancellor(Chancellor chancellor)
        {
            _contextClass.Chancellors.Update(chancellor);
            await _contextClass.SaveChangesAsync();
            return chancellor;
        }
    }
 }