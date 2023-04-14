using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildCare.DTOs
{
    public class RegistrationViewModel
    {
        public Guid UserId { get; set; }
        public string? DisplayName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? PhoneNumber { get; set; }
        public string? SaltKey { get; set; }

        public int? UserRoleId { get; set; }

        public int? TypeId { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsVerified { get; set; }
        public DateTime CreatedOn { get; set; }


    }
}
