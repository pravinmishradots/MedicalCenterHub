using ChildCare.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ChildCare.Areas.User.Controllers
{
    public class NewbornController : BaseController
    {

        #region Newborn Index
        public IActionResult NewbornIndex()
        {
            return View();
        }
        #endregion


        #region Add Edit New Born
        public IActionResult AddEditNewborn()
        {

            return PartialView("_AddEditNewborn");
        }
        #endregion




    }
}
