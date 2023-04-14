
using ChildCare.Models.Security;
using ChildCareCore.Entities;
using ChildCareCore.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChildCare.Code.Attributes
{
    public class CustomAuthorization : Attribute, IAuthorizationFilter
    {
        public HttpContextAccessor accessor;

        private UserRolesEnum[] UserRoless;
        public CustomAuthorization(params UserRolesEnum[] UserRole)
        {
            this.UserRoless = UserRole;
        }

        protected virtual CustomPrincipal CurrentUser
        {
            get { return new CustomPrincipal(ContextProvider.HttpContext.User); }
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (CurrentUser == null || !CurrentUser.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/admin/");
            }

        }

        private void ReturnAccessDenied(AuthorizationFilterContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                controller = "error",
                action = "accessDenied"
            }));

        }

    }
    public class CustomActionAuthorization :ActionFilterAttribute
    {
        protected virtual CustomPrincipal CurrentUser
        {
            get { return new CustomPrincipal(ContextProvider.HttpContext.User); }
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext != null)
            {
                if (!CurrentUser.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult("~/account/index");
                }
            }
        }
    }
}
