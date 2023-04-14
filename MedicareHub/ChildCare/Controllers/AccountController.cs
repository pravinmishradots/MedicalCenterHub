
using ChildCare.Code.LIBS;
using ChildCare.DTOs;
using ChildCare.Services;
using ChildCareCore.Entities;
using ChildCareCore.Enums;
using ChildCareCore.Helper;
using ChildCareCore.Resources;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Mvc;

namespace ChildCare.Controllers
{

    public class AccountController : BaseController
    {
        #region SERVICE IMPLEMENTATION

        private readonly IAccountService _accountService;

        #endregion

        #region CONSTRUCTOR

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        #endregion

        #region REGISTRATION 


        [HttpGet]
        public async Task<IActionResult> Register(int TypeId = 0)
        {

            RegistrationViewModel userRoleViewModel = new RegistrationViewModel();


            if (TypeId == 1)
            {
                userRoleViewModel.TypeId = TypeId;
                userRoleViewModel.UserRoleId = (int)UserRolesEnum.Provider;
                return View("DoctorSignUp", userRoleViewModel);
                

            }

            else if (TypeId == 2)
            {
                userRoleViewModel.TypeId = TypeId;
                userRoleViewModel.UserRoleId = (int)UserRolesEnum.Clinic;
                return View("ClinicSignUp", userRoleViewModel);

            }
            else if(TypeId==3)
            {
                userRoleViewModel.TypeId = TypeId;
                userRoleViewModel.UserRoleId = (int)UserRolesEnum.Laboratory;
                return View("laboratorySignUp", userRoleViewModel);

            }
            else
            {
                userRoleViewModel.TypeId = TypeId;
                userRoleViewModel.UserRoleId = (int)UserRolesEnum.SuperAdmin;
                return View("SuperAdminSignUp", userRoleViewModel);

            }

        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegistrationViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    ShowErrorMessage("failed", BaseResponseMessages.INVALID_DATA);
                    return NewtonSoftJsonResult(new RequestOutcome<string> { ErrorMessage = BaseResponseMessages.INVALID_DATA });
                }

                //Calling API Method

                var returndata = await _accountService.CreateUserAsync(model);

                ShowSuccessMessage("Success", BaseResponseMessages.ACCOUNT_CREATED);
                return RedirectToAction("SignIn", "Account");
                //return NewtonSoftJsonResult(new RequestOutcome<string> { Message = "Account has been created successfully." });
            }
            catch (Exception)
            {
                ShowErrorMessage("failed", BaseResponseMessages.INVALID_DATA);
                return NewtonSoftJsonResult(new RequestOutcome<string> { ErrorMessage = BaseResponseMessages.INVALID_DATA });
            }
        }

        #endregion

        #region LOGIN
        [HttpGet]
        public async Task<IActionResult> SignIn()

        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignInUser(LoginViewModel model)
        
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ShowErrorMessage("failed", BaseResponseMessages.INVALID_DATA);
                    return NewtonSoftJsonResult(new RequestOutcome<string> { ErrorMessage = BaseResponseMessages.INVALID_DATA });
                }


                //Calling API Method
                var returndata = await _accountService.GetLoginUser(model);

          
                var user = new User
                {
                    Email = returndata.Data!.Email,
                    Token = returndata.Data!.Token,
                    DisplayName = returndata.Data!.DisplayName,
                    Password = returndata.Data!.Password,
                    UserId = returndata.Data!.UserId,
                    UserRoleId = returndata.Data!.UserRoleId,
                };
                var roleId = user.UserRoleId;

                var roleType = ((UserRolesEnum)roleId!).GetDescription();

                await CreateAuthenticationTicket(user, roleType, false);

                switch (user.UserRoleId)
                {
                    case (int)UserRolesEnum.SuperAdmin:
                        return LocalRedirect("~/SuperAdmin/SuperAdminDashboard/Dashboard");

                    case (int)UserRolesEnum.Provider:
                        return LocalRedirect("~/User/UserDashboard/Index");

                    case (int)UserRolesEnum.Clinic:
                        return LocalRedirect("~/User/UserDashboard/Index");

                    case (int)UserRolesEnum.Laboratory:
                        return LocalRedirect("~/User/UserDashboard/Index");
                }
            
                ShowSuccessMessage("Success", BaseResponseMessages.ACCOUNT_CREATED);
                return CreateModelStateErrors();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("failed", BaseResponseMessages.INVALID_DATA);
                return RedirectToAction("SignIn");

            
            }
            
        }

        #endregion

        #region LOGOUT

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("SignIn");

        }


        #endregion


    }
}



