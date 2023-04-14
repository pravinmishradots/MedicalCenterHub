using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildCare.DTOs
{
    public class UserListModel
    {
        public Guid UserId { get; set; }
        public string? DisplayName { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
       
        public int? UserRoleId { get; set; }

        public int? TypeId { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsVerified { get; set; }



        public int pagesize { get; set; }
        public int pageNumber { get; set; }
        public int sortColumnIndex { get; set; }
        public string? sortDirection { get; set; }
        public string? title { get; set; }


        //public List<ListGetDataValue> ListGetData { get; set; }

    }




}
