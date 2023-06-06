using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Dto;
using UniversityManagementMvc.Dto.RequestModel;
using UniversityManagementMvc.Dto.ResponseModel;
using UniversityManagementMvc.Interface.Respository;
using UniversityManagementMvc.Interface.Services;

namespace UniversityManagementMvc.Implementation.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRespository _studentrespository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentService(IStudentRespository studentRespository)
        {
            _studentrespository = studentRespository;
        }

        public BaseResponseModel AddOldStudent(CreateOldStudentRequest model)
        {
            if (model == null)
            {
                return new BaseResponseModel
                {
                    Message = "Please fill the form completely",
                    Success = false
                };

            };
            var student = new Student
            {
                DateOfBirth = model.DateOfBirth,
                NextOfKin = model.NextOfKin,
                // OLevelResult = OLevelResult,
                User = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PassWord = model.PassWord,
                    Email = model.Email
                }
            };
            if (model.MatricNumber.Contains('U') || model.MatricNumber.Length == 8)
            {
                return new BaseResponseModel
                {
                    Message = "Matricnumber verified successfully",
                    Success = true,
                };
            }
            _studentrespository.CreateStudent(student);
            return new BaseResponseModel
            {
                Message = "Your Account was created succesfully",
                Success = true,
            };
        }
        public BaseResponseModel AddStudent(CreateStudentRequestModel model)
        {
            if (model == null)
            {
                return new BaseResponseModel
                {
                    Message = "Please fill the form completely",
                    Success = false
                };
            }
         
            
            var student = new Student
            {
                MatricNumber = model.MatricNumber = $"U{Guid.NewGuid().ToString().Replace("_", "").Substring(0, 6)}",
                DateOfBirth = model.DateOfBirth,
                NextOfKin = model.NextOfKin,
                OLevelResult = model.OLevelResult,
                ProfilePicture = model.ProfilePicture,
                Gender = model.Gender,
                User = new User
                {
                    Address = model.Address,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PassWord = model.PassWord,
                    Email = model.Email,
                    Role = model.Role = "Student",
                }
            };
            _studentrespository.CreateStudent(student);
            return new BaseResponseModel
            {
                Message = $"Your Account was created succesfully and your MatricNumber is {model.MatricNumber}",
                Success = true,
            };
        }

        public async Task<BaseResponseModel> ApproveStudent(string matricNumber)
        {
            var stud = await _studentrespository.GetStudent(x => x.IsApproved == false && x.MatricNumber == matricNumber);
            if (stud == null)
            {
                return new BaseResponseModel
                {

                    Message = "Lecturer not found",
                    Success = false
                };
            }
            stud.IsApproved = true;
            await _studentrespository.UpdateStudent(stud);
            return new BaseResponseModel
            {
                Message = "Student is Approved Successfully ",
                Success = true
            };
        }

        public async Task<BaseResponseModel> DeleteStudent(string matricNumber)
        {
            var det = await _studentrespository.GetStudent(l => l.IsDeleted == false && l.MatricNumber == matricNumber);
            if (det == null)
            {
                return new BaseResponseModel
                {
                    Message = "Lecturer does not exist",
                    Success = false,
                };
            }
            det.IsDeleted = true;
            await _studentrespository.UpdateStudent(det);
            return new BaseResponseModel
            {
                Message = "Student deleted Successfully",
                Success = true,
            };
        }

        public async Task<BaseResponseModel> EditStudent(UpdateStudentRequestModel requestModel, string matricNumber)
        {
            if (requestModel == null)
            {
                return new BaseResponseModel
                {
                    Message = "Pls fill the form completely",
                    Success = false
                };
            }
            var student = await _studentrespository.GetStudent(x => x.MatricNumber == matricNumber);
            if (student == null)
            {
                return new BaseResponseModel
                {
                    Message = "Cannot match student",
                    Success = false
                };
            }
            student.User.FirstName = requestModel.FirstName;
            student.User.LastName = requestModel.LastName;
            student.User.PhoneNumber = requestModel.PhoneNumber;
            student.DateOfBirth = requestModel.DateOfBirth;
            student.User.Email = requestModel.Email;
            student.User.Address = requestModel.Address;
            student.User.PassWord = requestModel.PassWord;
            student.ProfilePicture = requestModel.ProfilePicture;
            await _studentrespository.UpdateStudent(student);
            
            return new BaseResponseModel
            {
                Message = $"Student with {student.MatricNumber} is successfuly updated",
                Success = true
            };
        }

        public async Task<Student> Login(string email, string passWord)
        {
            return await _studentrespository.Login(email, passWord);
        }


        // public Student Login(string email, string passWord)
        // {
        //     return _studentrespository.Login(email, passWord);
        // }

        public StudentsResponseModel ViewAllStudents()
        {
            var view = _studentrespository.GetAllStudent();
            if (view == null)
            {
                return new StudentsResponseModel
                {
                    Message = " Having some problem try again",
                    Success = false,
                };
            }
            return new StudentsResponseModel
            {
                Message = "Tracked Successfully",
                Success = true,
            };
        }

        public async Task<StudentResponseModel> ViewStudent(string matricNumber)
        {
            var get = await _studentrespository.GetStudent(x => x.MatricNumber == matricNumber);
            if (get == null)
            {
                return new StudentResponseModel
                {
                    Message = "Can't find lecturerId",
                    Success = false,

                };
            }
            return new StudentResponseModel
            {
                Message = "matricNum tracked succesfully",
                Success = true,
                StudentDto = new StudentDto
                {
                    FirstName = get.User.FirstName,
                    LastName = get.User.LastName,
                    PhoneNumber = get.User.PhoneNumber,
                    Email = get.User.Email,
                    MatricNumber = get.MatricNumber,
                    Address = get.User.Address,
                    MaritalStatus = get.MaritalStatus,
                    Gender = get.Gender,
                    CreatedOn = get.CreatedOn,
                }
            };
        }
        
    }

}


