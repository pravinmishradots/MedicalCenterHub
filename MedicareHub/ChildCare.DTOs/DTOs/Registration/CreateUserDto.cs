using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildCare.DTOs
{
    public class CreateUserDto
    {    
        public string? DisplayName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public int? UserRoleId { get; set; }

        public int? TypeId { get; set; }


    }
}
