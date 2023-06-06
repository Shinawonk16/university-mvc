using UniversityManagementMvc.Data.Entities;
using UniversityManagementMvc.Dto;
using UniversityManagementMvc.Dto.RequestModel;
using UniversityManagementMvc.Dto.ResponseModel;
using UniversityManagementMvc.Interface.Respository;
using UniversityManagementMvc.Interface.Services;

namespace UniversityManagementMvc.Implementation.Services
{
    public class ManagementServices : IManagementService
    {
        private readonly IManagementRespository _managementRespository;
        public ManagementServices(IManagementRespository managementRespository)
        {
            _managementRespository = managementRespository;
        }
        public BaseResponseModel AddManagement(CreateManagementRequestModel model)
        {
            if (model == null)
            {
                return new BaseResponseModel
                {
                    Message = "Pls fill the form completely",
                    Success = false
                };

            }
            var management = new Management
            {
                ProfilePicture = model.ProfilePicture,
                User = new User
                {
                    PassWord = model.PassWord,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    Role = model.Role = "Management"
                }
            };
            _managementRespository.CreateManagement(management);
            return new BaseResponseModel
            {
                Message = "Management Created Successfully",
                Success = true
            };
        }

        public async Task<BaseResponseModel> EditManagement(UpdateManagementReequestModel requestModel, int id)
        {
               if (requestModel == null)
            {
                return new BaseResponseModel
                {
                    Message = "Pls fill the form completely",
                    Success = false
                };
            }
            var man = await _managementRespository.GetManagement(x => x.Id == id);
            if (man == null)
            {
                return new BaseResponseModel
                {
                    Message = "Cannot match student",
                    Success = false
                };
            }

            man.User.FirstName = requestModel.FirstName;
            man.User.LastName = requestModel.LastName;
            man.User.PhoneNumber = requestModel.PhoneNumber;
            man.User.Email = requestModel.Email;
            man.User.PassWord = requestModel.PassWord;
            man.ProfilePicture = requestModel.ProfilePicture;
            await _managementRespository.UpdateManagement(man);
            
            return new BaseResponseModel
            {
                Message = $"Mangement is successfuly updated",
                Success = true
            };
        }

        public async Task<Management> Login(string email, string passWord)
        {
            return await _managementRespository.Login(email,passWord);
        }

        public async Task<ManagementResponseModel> ViewManagement(int id)
        {
             var get = await _managementRespository.GetManagement(x => x.Id == id);
            if (get == null)
            {
                return new ManagementResponseModel
                {
                    Message = "Can't find lecturerId",
                    Success = false,

                };
            }
            return new ManagementResponseModel
            {
                Message = "matricNum tracked succesfully",
                Success = true,
                ManagementDto = new ManagementDto
                {
                    FirstName = get.User.FirstName,
                    LastName = get.User.LastName,
                    PhoneNumber = get.User.PhoneNumber,
                    Email = get.User.Email,
                }
            };
        }

        // public Management Login(string email, string passWord)
        // {
        //    return _managementRespository.Login(email,passWord);
        // }
    }



}