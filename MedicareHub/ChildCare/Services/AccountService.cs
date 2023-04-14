
using ChildCare.DTOs;
using ChildCareApi.DTOs;
using Microsoft.Build.Execution;

namespace ChildCare.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHttpService httpService;

        public AccountService(HttpClient httpClient, IHttpService _httpService)
        {
            httpService = _httpService;
            httpService.Client = httpClient;
        }

        #region UserList Calling Api 
        public async Task<dynamic> GetAllUsers(bool data = false)
        {
            return await httpService.Client.GetFromJsonAsync<dynamic>("api/Account/GetAllUserList");
        }


        #endregion


        #region Create User Calling Api
        public async Task<BaseApiResponseModel<UserDto>> CreateUserAsync(RegistrationViewModel model)
        {
            CreateUserDto userDto = new CreateUserDto();
            userDto.DisplayName = model.DisplayName?.Trim();
            userDto.Email = model.Email?.Trim();
            userDto.Password = model.Password;
            userDto.UserRoleId = model.UserRoleId;
            userDto.TypeId = model.TypeId;
            var data = await httpService.PostAsync<UserDto>("api/Account/RegisterUser", userDto);
            return data;
        }


        #endregion


        #region Delete User Api Calling Method

        public async Task<BaseApiResponseModel<object>> DeleteUser(RegistrationViewModel model)
        {
            var data = await httpService.PostAsync<object>("api/Account/DeleteUser", model);
            return data;
        }
        #endregion




        #region UpdateUser Calling Api

        public async Task<BaseApiResponseModel<object>> UpdateUser(RegistrationViewModel model)
        {
            var data = await httpService.PostAsync<object>("api/Account/UpdateUser", model);
            return data;
        }
        #endregion


        #region Login User Calling APi method
        public async Task<BaseApiResponseModel<UserDto>> GetLoginUser(LoginViewModel model)
        {
            LoginRequestDto userDto = new LoginRequestDto();
            userDto.EmailId = model.Email!.Trim();
            userDto.Password = model.Password!;
            var data = await httpService.PostAsync<UserDto>("api/Account/Login", userDto);
            return data;
        }
        #endregion

    }
}
