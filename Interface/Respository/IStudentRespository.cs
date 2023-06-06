using System.Linq.Expressions;
using UniversityManagementMvc.Data.Entities;

namespace UniversityManagementMvc.Interface.Respository
{
    public interface IStudentRespository
    {
         Student CreateStudent(Student student);
        Task<Student> UpdateStudent (Student student);
        Task<Student> Login(string email,string passWord);
        Task<Student> GetStudent(string matricNumber);
        
        Task<Student> GetStudent(Expression<Func<Student, bool>> expression);
     
        List<Student> GetAllStudent();
        Task<bool> DeleteStudent(Student student);
    }
}