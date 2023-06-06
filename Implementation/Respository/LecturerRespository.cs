using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UniversityManagementMvc.Data.ContextClass;
using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Interface.Respository;

namespace UniversityManagementMvc.Implementation.Respository
{
    public class LecturerRespository : ILecturerRespository
    {
        private readonly ApplicationContext _contextClass;
        public LecturerRespository(ApplicationContext context)
        {
            _contextClass = context;
        }

        public Lecturer CreateLecturer(Lecturer lecturer)
        {
            _contextClass.Lecturers.Add(lecturer);
            _contextClass.SaveChanges();
            return lecturer;
        }

        public async Task<bool> DeleteLecturer(Lecturer lecturer)
        {
            _contextClass.Set<Lecturer>().Remove(lecturer);
           await _contextClass.SaveChangesAsync();
            return true;
        }

        public List<Lecturer> GetAllLecturer()
        {
            return _contextClass.Lecturers.ToList();
        }

        public async Task<Lecturer> GetLecturer(string lecturerId)
        {
            return  await _contextClass.Set<Lecturer>().FindAsync(lecturerId);
        }

        public async Task<Lecturer> GetLecturer(Expression<Func<Lecturer, bool>> expression)
        {
            
            return await _contextClass.Set<Lecturer>().SingleOrDefaultAsync();
        }

        public async Task<Lecturer> Login(string email, string passWord)
        {
            return await _contextClass.Lecturers.Where(x => x.User.Email == email && x.User.PassWord == passWord).SingleOrDefaultAsync();
       
        }

        public async Task<Lecturer> UpdateLecturer(Lecturer lecturer)
        {
             _contextClass.Lecturers.Update(lecturer);
           await _contextClass.SaveChangesAsync();
            return lecturer;
        }
    }
}