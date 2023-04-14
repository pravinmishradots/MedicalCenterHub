using ChildCare.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChildCareCore.Helper;
using ChildCare.Services.Dashboard;

using ChildCareCore.Specifications;

using ChildCareCore.Enums;
using ChildCare.DTOs;
using System.Drawing.Printing;
using ChildCareApi.DTOs.GetUserRequest;
using ChildCare.Models.Common;
using static ChildCare.Code.LIBS.Enums;
using ChildCare.Code.LIBS;

namespace ChildCare.Areas.SuperAdmin.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class DoctorManagerController : BaseController
    {
        #region SERVICE IMPLEMENTATION

        private readonly IDashboardService _dashboardService;

        #endregion

        #region CONSTRUCTOR

        public DoctorManagerController(IDashboardService dashboardservice)
        {
            _dashboardService = dashboardservice;
        }
        #endregion


        #region  Doctor Index 
        [HttpGet]
        public IActionResult Index()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<DataTableResultExt> Index(ChildCareCore.Helper.DataTableExtend dataTable, UserSpecification model)
    
        {

            string title = "";
            if (!string.IsNullOrEmpty(dataTable.sSearch))
            {
                title = dataTable.sSearch.ToLower();

            }

            int pagesize = dataTable.iDisplayLength;
            int pageNumber = (dataTable.iDisplayStart / dataTable.iDisplayLength) + 1;

            List<DataTableRow> table = new List<DataTableRow>();

            List<int> column1 = new List<int>();
            for (int i = dataTable.iDisplayStart; i < dataTable.iDisplayStart + dataTable.iDisplayLength; i++)
            {
                column1.Add(i);
            }

            int count = dataTable.iDisplayStart + 1, total = 0;

            //  var returndata = await _dashboardService.GetAllUsersByFilter(00000000,pagesize,pageNumber);
            var returndata = await _dashboardService.GetAllUsersData();
            var doctorlist = returndata.Data!.Where(x => x.UserRoleId == 1).ToList();

            foreach (var item in doctorlist)
            {

                table.Add(new DataTableRow("rowId" + count.ToString(), "dtrowclass")
                {
                    item.UserId.ToString(),//0
                       count.ToString(),//1
                    item.DisplayName!, //2
                 
                    item.Email!.ToString(),//3
                    ((UserRolesEnum)item.UserRoleId!).GetDescription(),//4
                   
                    
                    item.CreatedOn.ToString(),//5
                   
                }) ;
                count++;
            }
      
            return new DataTableResultExt(dataTable, table.Count(), total, table);
        }

        #endregion

        #region Add Edit Doctor


        [HttpGet]
        public IActionResult AddEditProvider(Guid? Id)
        {

            RegistrationViewModel model = new RegistrationViewModel();

            return PartialView("_AddEditProvider", model);
        }

        [HttpPost]
        public IActionResult SaveDoctorData(Guid? Id, RegistrationViewModel model)
        {

    
            return PartialView();
        }




        #endregion


        #region Delete  Provider data 

        [HttpGet]
        public IActionResult Delete()
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure you want to delete this User?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete User" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });

        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var data = _dashboardService.DeleteUserById(id);
                if (data != null)
                {
                    ShowSuccessMessage("Success", "Doctor is successfully deleted", false);
                }
                return Redirect("/SuperAdmin/DoctorManager/Index");
            }
            catch (Exception ex)
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ex.GetBaseException().Message, IsSuccess = false });
            }
        }
        #endregion Delete

    }
}

