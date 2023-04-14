using ChildCare.Controllers;
using ChildCare.DTOs;
using ChildCare.Services.Dashboard;
using ChildCareCore.Enums;
using ChildCareCore.Helper;
using ChildCareCore.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ChildCare.Areas.SuperAdmin.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class LabAssistantManagerController : BaseController
    {



        #region SERVICE IMPLEMENTATION

        private readonly IDashboardService _dashboardService;

        #endregion

        #region CONSTRUCTOR

        public LabAssistantManagerController(IDashboardService dashboardservice)
        {
            _dashboardService = dashboardservice;
        }
        #endregion


        #region Show LabAssistant List data


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<DataTableResultExt>Index(ChildCareCore.Helper.DataTableExtend dataTable, UserSpecification model)
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
            var doctorlist = returndata.Data!.Where(x => x.UserRoleId == 3).ToList();

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
                   
                });
                count++;
            }

            return new DataTableResultExt(dataTable, table.Count(), total, table);
        }




        #endregion

        #region Add Edit Clinic Manager

        [HttpGet]
        public IActionResult AddEditLabAssistant(Guid? Id)
        {

            RegistrationViewModel model = new RegistrationViewModel();

            return PartialView("_AddEditLabAssistant", model);
        }

        #endregion

    }
}
