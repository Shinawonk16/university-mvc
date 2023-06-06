using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UniversityManagementMvc.Data.ContextClass;
using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Interface.Respository;

namespace UniversityManagementMvc.Implementation.Respository
{
    public class StudentRespository : IStudentRespository
    {
        private readonly ApplicationContext _contextClass;
        public StudentRespository(ApplicationContext context)
        {
            _contextClass = context;
        }
        public Student CreateStudent(Student student)
        {
            _contextClass.Students.Add(student);
            _contextClass.SaveChanges();
            return student;
        }

        public async Task<bool> DeleteStudent(Student student)
        {
            _contextClass.Students.Remove(student);
           await _contextClass.SaveChangesAsync();
            return true;
        }

        public List<Student> GetAllStudent()
        {
            return _contextClass.Students.ToList();
        }

        public async Task<Student> GetStudent(string matricNumber)
        {
            return await _contextClass.Students.FindAsync(matricNumber);
        }

        public async Task<Student> GetStudent(Expression<Func<Student, bool>> expression)
        {
            return await _contextClass.Set<Student>().SingleOrDefaultAsync(expression);
        }

        public async Task<Student> Login(string email, string passWord)
        {
            return await _contextClass.Students.Where(x => x.User.Email == email && x.User.PassWord == passWord).SingleOrDefaultAsync();

        }

        public async Task<Student> UpdateStudent(Student student)
        {
            _contextClass.Students.Update(student);
            await _contextClass.SaveChangesAsync();
            return student;
        }
    }
}