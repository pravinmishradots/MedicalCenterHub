using ChildCare.Code.Attributes;
using ChildCare.Controllers;
using ChildCareCore.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChildCare.Areas.SuperAdmin.Controllers
{
    //[Area("SuperAdmin")]

    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminDashboardController : BaseController
    {

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
