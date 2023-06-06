using System.Linq.Expressions;
using UniversityManagementMvc.Data.Entities;

namespace UniversityManagementMvc.Interface.Respository
{
    public interface ILecturerRespository
    {
           Lecturer CreateLecturer(Lecturer lecturer);
        Task<Lecturer> UpdateLecturer (Lecturer lecturer);
        Task<Lecturer> Login(string email,string passWord);
        Task<Lecturer> GetLecturer(string lecturerId);
        
        Task<Lecturer> GetLecturer(Expression<Func<Lecturer, bool>> expression);
     
        List<Lecturer> GetAllLecturer();
        Task<bool> DeleteLecturer(Lecturer lecturer);
    }
}