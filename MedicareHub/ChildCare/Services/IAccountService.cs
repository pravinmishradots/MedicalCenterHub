using ChildCare.DTOs;
using ChildCareApi.DTOs;
using ChildCareCore.Entities;

namespace ChildCare.Services
{
    public interface IAccountService
    {
        Task<BaseApiResponseModel<UserDto>> CreateUserAsync(RegistrationViewModel model);
        Task<BaseApiResponseModel<object>> DeleteUser(RegistrationViewModel model);
        Task<BaseApiResponseModel<object>> UpdateUser(RegistrationViewModel model);
        Task<BaseApiResponseModel<UserDto>> GetLoginUser(LoginViewModel model);
        //Task<UserDto> GetAllUsers(bool data = false);
        //Task<BaseApiResponseModel<UserDto>> GetAllUsers(RegistrationViewModel model);


    }
}
