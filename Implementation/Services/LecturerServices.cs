using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Dto;
using UniversityManagementMvc.Dto.RequestModel;
using UniversityManagementMvc.Dto.ResponseModel;
using UniversityManagementMvc.Interface.Respository;
using UniversityManagementMvc.Interface.Services;

namespace UniversityManagementMvc.Implementation.Services
{
    public class LecturerServices : ILecturerService
    {
        private readonly ILecturerRespository _lecturerRespository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LecturerServices(ILecturerRespository lecturerRespository)
        {
            _lecturerRespository = lecturerRespository;
        }
        public BaseResponseModel AddLecturer(CreateLecturerRequestModel model)
        {
            if (model == null)
            {
                return new BaseResponseModel
                {
                    Message = "Please fill the form completely",
                    Success = false
                };
            }
         
            var lecturer = new Lecturer
            {

                DateOfBirth = model.DateOfBirth,
                Qualification =model.Qualification,
                NextOfKin = model.NextOfKin,
                Gender = model.Gender,
                ProfilePicture = model.ProfilePicture,
                // Gender = model.Gender,
                User = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    PassWord = model.PassWord,
                    Address = model.Address,
                    Role = model.Role="Lecturer",
                }
            };
            _lecturerRespository.CreateLecturer(lecturer);
            return new BaseResponseModel
            {
                Message = "Your Account Was Created Successfully",
                Success = true
            };
        }

        public async Task<BaseResponseModel> ApproveLecturer(string lecturerId)
        {
            var lecturer = await _lecturerRespository.GetLecturer(x => x.IsApproved == false && x.LecturerId == lecturerId);
            if (lecturer == null)
            {
                return new BaseResponseModel
                {

                    Message = "Lecturer not found",
                    Success = false
                };
            }
            lecturer.IsApproved = true;
            await _lecturerRespository.UpdateLecturer(lecturer);
            return new BaseResponseModel
            {
                Message = "Lecturer is Approved Successfully ",
                Success = true
            };
        }

        public async Task<BaseResponseModel> DeleteLecturer(string lecturerId)
        {
            var let = await _lecturerRespository.GetLecturer(l => l.IsDeleted == false && l.LecturerId == lecturerId);
            if (let == null)
            {
                return new BaseResponseModel
                {
                    Message = "Lecturer does not exist",
                    Success = false,
                };
            }
            let.IsDeleted = true;
            await _lecturerRespository.UpdateLecturer(let);
            return new BaseResponseModel
            {
                Message = "Lecturer deleted Successfully",
                Success = true,
            };
        }

        public async Task<BaseResponseModel> EditLecturer(UpdateLecturerRequestModel requestModel, string lecturerId)
        {
            if (requestModel == null)
            {
                return new BaseResponseModel
                {
                    Message = "Pls fill the form completely",
                    Success = false
                };
            }
            var let = await _lecturerRespository.GetLecturer(x => x.LecturerId == lecturerId);
            if (let == null)
            {
                return new BaseResponseModel
                {
                    Message = "Cannot match Lecturer",
                    Success = false
                };
            }
            let.User.FirstName = requestModel.FirstName;
            let.User.LastName = requestModel.LastName;
            let.DateOfBirth = requestModel.DateOfBirth;
            let.User.PhoneNumber = requestModel.PhoneNumber;
            let.User.Email = requestModel.Email;
            let.User.PassWord = requestModel.PassWord;
            let.ProfilePicture = requestModel.ProfilePicture;
            let.User.Address = requestModel.Address;
            await _lecturerRespository.UpdateLecturer(let);
            return new BaseResponseModel
            {
                Message = $"Lecturer with {let.LecturerId} is Updated Successfully ",
                Success = true
            };
        }

        public async Task<Lecturer> Login(string email, string passWord)
        {
            return await _lecturerRespository.Login(email,passWord);
        }

        // public Lecturer Login(string email, string passWord)
        // {
        //     return _lecturerRespository.Login(email, passWord);
        // }

        public LecturersResponseModel ViewAllLecturers()
        {
            var get = _lecturerRespository.GetAllLecturer();
            if (get == null)
            {
                return new LecturersResponseModel
                {
                    Message = " Having some problem try again",
                    Success = false,
                };
            }
            return new LecturersResponseModel
            {
                Message = "Tracked Successfully",
                Success = true,
            };
        }

        public async Task<LecturerResponseModel> ViewLecturer(string lecturerId)
        {
            var ret = await _lecturerRespository.GetLecturer(x => x.LecturerId == lecturerId);
            if (ret == null)
            {
                return new LecturerResponseModel
                {
                    Message = "Can't find lecturerId",
                    Success = false,

                };
            }
            return new LecturerResponseModel
            {
                Message = "lecturerId track successfully",
                Success = true,
                LecturerDto = new LecturerDto
                {
                    FirstName = ret.User.FirstName,
                    LastName = ret.User.LastName,
                    PhoneNumber = ret.User.PhoneNumber,
                    Email = ret.User.Email,
                    LecturerId = ret.LecturerId,
                    Address = ret.User.Address,
                    MaritalStatus = ret.MaritalStatus,
                    Gender = ret.Gender,
                    CreatedOn = ret.CreatedOn
                }
            };
        }
    }
}