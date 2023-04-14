using ChildCare.DTOs;
using ChildCareApi.DTOs;
using ChildCareApi.DTOs.GetUserRequest;
using System.Security.Claims;

namespace ChildCare.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {

        #region  Service implement

        private readonly IHttpService httpService;
        private readonly IHttpContextAccessor httpContextAccessor;
        #endregion

        #region Constructor Add
        public DashboardService(HttpClient httpClient, IHttpContextAccessor _httpContextAccessor, IHttpService _httpService)
        {
            httpService = _httpService;
            httpService.Client = httpClient;
            httpContextAccessor = _httpContextAccessor;
            httpService.AccessToken = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.SerialNumber)!;

        }
        public string UserId => httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);


        #endregion

        #region  Add New User
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



        #endregion

        #region Get All User List Data

        public async Task<BaseApiResponseModel<List<RegistrationViewModel>>> GetAllUsersData()
        {
  
            return await httpService.GetAsync<List<RegistrationViewModel>>("api/UserManagement/GetAllUserList");
        }

        #endregion

        #region  Get User List By PageNumber and orderBy

        public async Task<BaseApiResponseModel<List<RegistrationViewModel>>> GetAllUsersByFilter(int pagesize, int pageNumber, int sortColumnIndex, string sortDirection = "asc", string title = "")
        {
            UserListRequestDto userlistdto = new UserListRequestDto();

            var data = await httpService.PostAsync<List<RegistrationViewModel>>("api/Account/GetAllUserList", userlistdto);
            return data;
        }

        #endregion

        #region Get List ByUserId
        public async Task<BaseApiResponseModel<UserDto>>GetUserByUserId(Guid id)
        {
          GetUserRequestModel model = new GetUserRequestModel();
         model.UserId = id;
            return await httpService.PostAsync<UserDto>("api/UserManagement/GetUserById",model);

        }

        #endregion



        #region Delete UserBy Id 

        public async Task<BaseApiResponseModel<RegistrationViewModel>> DeleteUserById(Guid id)
        {

            return await httpService.PostAsync<RegistrationViewModel>("api/UserManagement/DeleteUserById",id);

        }





        #endregion



    }
}
