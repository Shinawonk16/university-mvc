using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Dto;
using UniversityManagementMvc.Dto.RequestModel;
using UniversityManagementMvc.Dto.ResponseModel;
using UniversityManagementMvc.Interface.Respository;
using UniversityManagementMvc.Interface.Services;

namespace UniversityManagementMvc.Implementation.Services
{
    public class UserService : IUserServices
    {
     private readonly IUserRespository _repository;
        public UserService(IUserRespository repository)
        {
            _repository = repository;
        }

        public BaseResponseModel AddUser(CreateUserRequestModel model)
        {
              if (model != null)
            {
                return new BaseResponseModel
                {
                    Message = "user Already Exist",
                    Success = false
                };
            }
          
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PassWord = model.Password,
                PhoneNumber = model.PhoneNumber,
                IsActive = model.IsActive,
                Role = model.Role = "User",
            };
            _repository.CreateUser(user);
            return new BaseResponseModel
            {
                Message = "Your Account was created succesfully",
                Success = true,
            };
        }

        public async Task<BaseResponseModel> DeleteUser(int id)
        {
             var det = await _repository.GetUser(l => l.IsDeleted == false && l.Id == id);
            if (det == null)
            {
                return new BaseResponseModel
                {
                    Message = "Lecturer does not exist",
                    Success = false,
                };
            }
            det.IsDeleted = true;
            det.IsActive = false;
            await _repository.UpdateUser(det);
            return new BaseResponseModel
            {
                Message = "Student deleted Successfully",
                Success = true,
            };
        }

        public async Task<BaseResponseModel> EditUser(UpdateUserRequestModel requestModel, int id)
        {
             if (requestModel == null)
            {
                return new BaseResponseModel
                {
                    Message = "Pls fill the form completely",
                    Success = false
                };
            }
            var use = await _repository.GetUser(x => x.Id == id);
            if (use == null)
            {
                return new BaseResponseModel
                {
                    Message = "Cannot match student",
                    Success = false
                };
            }
            use.FirstName = requestModel.FirstName;
            use.LastName = requestModel.LastName;
            use.PhoneNumber = requestModel.PhoneNumber;
            // use.DateOfBirth = requestModel.DateOfBirth;
            use.Email = requestModel.Email;
            use.PassWord = requestModel.PassWord;
            await _repository.UpdateUser(use);
            return new BaseResponseModel
            {
                Message = $"User with {use.Id} is successfuly updated",
                Success = true
            };
        }

        public async Task<UserResponseModel> Login(string email, string passWord)
        {
            var user = await _repository.Login(email,passWord);
            if (user == null)
            {
                return new UserResponseModel
                {
                     Success = false,
                    Message = "Unable to log in try again"
               
                };
            }
            // if (user.IsActive == false)
            // {
            //       return new UserResponseModel
            //     {
            //          Success = false,
            //         Message = "you are not active"
               
            //     };
                
            // }
            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Chancellors = user.Chancellors,
                Lecturers = user.Lecturers,
                Students =user.Students,
                IsActive = user.IsActive= true,
                IsDeleted = user.IsDeleted,
                Managements = user.Managements,
                Role = user.Role,
                Password = user.PassWord,
                PhoneNumber = user.PhoneNumber,
                

            };
            return new UserResponseModel
            {
              Message= "logged successfully",
              Success = true,
              UserDto = userDto
            };
             
        }

        public UserResponseModel ViewAllUsers()
        {
              var view = _repository.GetAllUser();
            if (view == null)
            {
                return new UserResponseModel
                {
                    Message = " Having some problem try again",
                    Success = false,
                };
            }
            return new UserResponseModel
            {
                Message = "Tracked Successfully",
                Success = true,
            };
        }

        public async Task<UserResponseModel> ViewUser(int id)
        {
            var get = await _repository.GetUser(x => x.Id == id);
            if (get == null)
            {
                return new UserResponseModel
                {
                    Message = "Can't find user",
                    Success = false,

                };
            }
            return new UserResponseModel
            {
                Message = "id tracked succesfully",
                Success = true,
                UserDto = new UserDto
                {
                    FirstName = get.FirstName,
                    LastName = get.LastName,
                    PhoneNumber = get.PhoneNumber,
                    Email = get.Email,
                    Role = get.Role,
                   
                }
            };
        }
    }
}
