﻿
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ChildCare.Models.Security;
using ChildCareCore.Entities;
using ChildCareCore.Helper;
using static ChildCare.Code.LIBS.Enums;

using ChildCare.Models;

namespace ChildCare.Controllers
{

    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        #region [ CLAIM PROPERTIES ]
        public ClaimsPrincipal LoggedinUser => HttpContext.User;
        public CustomPrincipal CurrentUser => new CustomPrincipal(HttpContext.User);

        #endregion [ CLAIM PROPERTIES ]

        #region [ CREATE AUTHENTICATION ]


        public async Task CreateAuthenticationTicket(User user, string roleType, bool isPersistent)
        {
            if (user != null)
            {
                //var permissions = user.UserPermission.Select(x => x.UserPermissionId).Distinct().ToArray();

                var claims = new List<Claim>{
                new Claim(ClaimTypes.PrimarySid, Convert.ToString(user.UserId)!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.DisplayName !),
                new Claim(ClaimTypes.Sid,user.UserRoleId.ToString()!),
                new Claim(ClaimTypes.SerialNumber,user.Token!),
                new Claim(ClaimTypes.Role, roleType),
                new Claim(nameof(user.UserId), Convert.ToString(user.UserId)!),
                new Claim(nameof(user.Email), user.Email!),
                new Claim(nameof(user.DisplayName), user.DisplayName!),
                new Claim(nameof(user.UserRoleId), Convert.ToString(user.UserRoleId)!),
                new Claim(nameof(user.Token),Convert.ToString(user.Token)!),





                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = isPersistent,
                    IsPersistent = isPersistent,
                    ExpiresUtc = DateTime.UtcNow.AddDays(1)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }
        }




        /// <summary>
        /// remove Authentication From Cookies..
        /// </summary>
        /// <returns>return true after set null </returns>
        public async Task RemoveAuthentication()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        #endregion [ CREATE AUTHENTICATION ]

        #region [ MESSAGE NOTIFICATION ]

        /// <summary>
        /// Shows Notification Message After Succesfully Record Submited or Failed to Submit any Record From Submit Of Form.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        /// <param name="isCurrentView"></param>
        private void ShowMessages(string title, string message, MessageType messageType, bool isCurrentView)
        {
            Notification model = new Notification
            {
                Heading = title,
                Message = message,
                Type = messageType
            };

            if (isCurrentView)
            {
                ViewData!.AddOrReplace("NotificationModel", model);
            }
            else
            {
                TempData["NotificationModel"] = JsonConvert.SerializeObject(model);
                TempData.Keep("NotificationModel");
            }
        }

        protected void ShowErrorMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Danger, isCurrentView);
        }

        protected void ShowSuccessMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Success, isCurrentView);
        }




        protected void ShowWarningMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Warning, isCurrentView);
        }

        protected void ShowInfoMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Info, isCurrentView);
        }


        #endregion [ MESSAGE NOTIFICATION ]

        #region [ HTTP ERROR REDIRECTION ]

        /// <summary>
        /// redirect to respected Status Code of Error
        /// </summary>
        /// <returns>redirect to 404</returns>
        protected ActionResult Redirect404()
        {
            return Redirect("~/error/pagenotfound");
        }

        /// <summary>
        /// redirect to respected Status Code of Error
        /// </summary>
        /// <returns>redirect to 500</returns>
        protected ActionResult Redirect500()
        {
            return Redirect("~/error/servererror");
        }

        protected ActionResult Redirect401()
        {
            return View();
        }

        #endregion [ HTTP ERROR REDIRECTION ]

        #region [ EXCEPTION HANDLING ]
        /// <summary>
        /// Handle Exception Of Model State Of Validation Summary
        /// </summary>
        /// <returns>return Error Message</returns>
        public PartialViewResult CreateModelStateErrors()
        {
            return PartialView("_ValidationSummary", ModelState.Values.SelectMany(x => x.Errors));
        }

        #endregion [ EXCEPTION HANDLING ]

        #region  [ SERIALIZATION ]
        /// <summary>
        /// Serilize Data into Json
        /// </summary>
        /// <param name="data"></param>
        /// <returns>return Json Data</returns>
        public IActionResult NewtonSoftJsonResult(object data)
        {
            if (data != null)
            {
                return Json(data);
            }
            else
            {
                throw new NullReferenceException("Data is null");
            }
        }
        #endregion [ SERIALIZATION ]

        #region [ DISPOSE ]

        /// <summary>
        /// Dispose All Service 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion [ DISPOSE ]

        public string GetIPAddress(IHttpContextAccessor accessor)
        {
            return accessor.HttpContext!.Connection.RemoteIpAddress!.ToString();
        }
        protected int? GetCurrentUserId(IHttpContextAccessor accessor)
        {
            string userId = accessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            return string.IsNullOrWhiteSpace(userId) ? Convert.ToInt32(userId) : (int?)null;
        }

    }
}
