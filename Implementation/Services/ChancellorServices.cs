using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Dto;
using UniversityManagementMvc.Dto.RequestModel;
using UniversityManagementMvc.Dto.ResponseModel;
using UniversityManagementMvc.Interface.Respository;
using UniversityManagementMvc.Interface.Services;

namespace UniversityManagementMvc.Implementation.Services
{
    public class ChancellorServices : IChancellorService
    {
        private readonly IChancellorRespository _chancellorRespository;
        private readonly IUserRespository _userRepository;

        public ChancellorServices(IChancellorRespository chancellorRespository, IUserRespository userRepository)
        {
            _chancellorRespository = chancellorRespository;
            _userRepository = userRepository;
         }

        public BaseResponseModel AddChancellor(CreateChancellorRequestModel model)
        {
            var chan = _chancellorRespository.GetChancellor(a => a.User.Email == model.Email);
            if (model == null)
            {
                return new BaseResponseModel
                {
                    Message = "Pls fill the form completely",
                    Success = false
                };
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Address = model.Address,
                Role = model.Role = "Chancellor",
                PassWord = model.PassWord = $"CP{Guid.NewGuid().ToString().Replace("_","").Substring(0,6)}a",
            };
            var adduser = _userRepository.CreateUser(user);
          
     
            _userRepository.UpdateUser(adduser);
            var chancellor = new Chancellor
            {
                UserId = adduser.Id,
                User = adduser,
                DateOfBirth = model.DateOfBirth,
                ChancellorId = model.ChancellorId,
                NextOfKin = model.NextOfKin,
                Gender = model.Gender,
                IsDeleted = false,
                ProfilePicture = model.ProfilePicture,
                MaritalStatus = model.MaritalStatus,

            };
            _chancellorRespository.CreateChancellor(chancellor);
            return new BaseResponseModel
            {
                Message = $"Your Account Was Created Successfully and your ChancellorId is {model.ChancellorId}",
                Success = true
            };

        }

        public async Task<BaseResponseModel> ApproveChancellor(string chancellorId)
        {
            var chancellor = await _chancellorRespository.GetChancellor(x => x.IsApproved == false && x.ChancellorId == chancellorId);
            if (chancellor == null)
            {
                return new BaseResponseModel
                {

                    Message = "Chancellor not found",
                    Success = false
                };
            }
            chancellor.IsApproved = true;
            await _chancellorRespository.UpdateChancellor(chancellor);
            return new BaseResponseModel
            {
                Message = "Chancellor is Approved Successfully ",
                Success = true
            };
        }

        public async Task<BaseResponseModel> DeleteChancellor(string chancellorId)
        {

            var chan = await _chancellorRespository.GetChancellor(chans => chans.IsDeleted == false && chans.ChancellorId == chancellorId);
            if (chan == null)
            {
                return new BaseResponseModel
                {
                    Message = "Chancellor does not exist",
                    Success = false,
                };
            }
            chan.IsDeleted = true;
            await _chancellorRespository.UpdateChancellor(chan);
            return new BaseResponseModel
            {
                Message = "Chancellor deleted Successfully",
                Success = true,
            };
        }

        public async Task<BaseResponseModel> EditChancellor(UpdateChancellorRequestModel requestModel, string chancellorId)
        {
            if (requestModel == null)
            {
                return new BaseResponseModel
                {
                    Message = "Pls fill the form completely",
                    Success = false
                };
            }
            var chancellor = await _chancellorRespository.GetChancellor(x => x.ChancellorId == chancellorId);
            if (chancellor == null)
            {
                return new BaseResponseModel
                {
                    Message = "Cannot match ChancellorId",
                    Success = false
                };
            }
             chancellor.User.FirstName = requestModel.FirstName;
            chancellor.User.LastName = requestModel.LastName;
            chancellor.User.PhoneNumber = requestModel.PhoneNumber;
            chancellor.User.Email = requestModel.Email;
            chancellor.DateOfBirth = requestModel.DateOfBirth;
            chancellor.User.PassWord = requestModel.PassWord;
            chancellor.ProfilePicture = requestModel.ProfilePicture;
            chancellor.User.Address = requestModel.Address;
            await _chancellorRespository.UpdateChancellor(chancellor);
            return new BaseResponseModel
            {
                Message = $"Chancellor with {chancellor.ChancellorId} is Updated Successfully ",
                Success = true
            };
        }

        public async Task<Chancellor> Login(string email, string passWord)
        {
            return await _chancellorRespository.Login(email,passWord);
        }

        // public Chancellor Login(string email, string passWord)
        // {
        //     return _chancellorRespository.Login(email, passWord);
        // }

        public ChancellorsResponseModel ViewAllChancellors()
        {
            var get = _chancellorRespository.GetAllChancellor();
            if (get == null)
            {
                return new ChancellorsResponseModel
                {
                    Message = " Having some problem try again",
                    Success = false,
                };
            }
            return new ChancellorsResponseModel
            {
                ChancellorDtos = get.Select(x => new ChancellorDto
                {
                    LastName = x.User.LastName,
                    FirstName = x.User.FirstName,
                    Address = x.User.Address,
                    Email = x.User.Email,
                    PhoneNumber = x.User.PhoneNumber,
                    Gender = x.Gender,
                    Role = x.User.Role

                }).ToList(),
                Message = "Tracked Successfully",
                Success = true,
                
            };
        }

        public async Task<ChancellorResponseModel> ViewChancellor(string chancellorId)
        {
            var retor = await _chancellorRespository.GetChancellor(chancellorId);
            if (retor == null)
            {
                return new ChancellorResponseModel
                {
                    Message = "Can't find Id",
                    Success = false,

                };
            }
            return new ChancellorResponseModel
            {
                Message = "doctorId track successfully",
                Success = true,
                ChancellorDto = new ChancellorDto
                {
                    FirstName = retor.User.FirstName,
                    LastName = retor.User.LastName,
                    PhoneNumber = retor.User.PassWord,
                    Email = retor.User.Email,
                    Address = retor.User.Address,
                    ChancellorId = retor.ChancellorId,
                    MaritalStatus = retor.MaritalStatus,
                    Gender = retor.Gender,
                    CreatedOn = retor.CreatedOn,
                    Role = retor.User.Role,
                }
            };
        }
    }
}