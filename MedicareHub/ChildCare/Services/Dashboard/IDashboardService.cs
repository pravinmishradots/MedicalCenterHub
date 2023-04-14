using ChildCare.DTOs;


namespace ChildCare.Services.Dashboard
{
    public interface IDashboardService
    {

        Task<BaseApiResponseModel<List<RegistrationViewModel>>> GetAllUsersData();

        Task<BaseApiResponseModel<List<RegistrationViewModel>>> GetAllUsersByFilter(int pagesize, int pageNumber, int sortColumnIndex, string sortDirection = "asc", string title = "");


        Task<BaseApiResponseModel<UserDto>> GetUserByUserId(Guid id);

        Task<BaseApiResponseModel<RegistrationViewModel>> DeleteUserById(Guid id);

        Task<BaseApiResponseModel<UserDto>> CreateUserAsync(RegistrationViewModel model);



    }
}
