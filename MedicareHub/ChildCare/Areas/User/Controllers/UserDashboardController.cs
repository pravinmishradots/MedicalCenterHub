using ChildCare.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ChildCare.Areas.User.Controllers
{
    public class UserDashboardController :BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
