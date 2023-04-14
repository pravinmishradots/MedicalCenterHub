using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildCare.DTOs
{
    public class UserRoleViewModel
    {
        public int RoleId { get; set; }

        public string? Name { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
