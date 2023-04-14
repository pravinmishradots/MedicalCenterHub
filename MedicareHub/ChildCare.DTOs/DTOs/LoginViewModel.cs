using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChildCare.DTOs
{
    public class LoginViewModel
    {

        public string ?Email { get; set; }

     
        public string? Password { get; set; }

        public string? Token { get; set; }
        public int? RoleId { get; set; }
        public int? Typeid { get; set; }



    }
}
