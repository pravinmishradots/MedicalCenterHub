using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildCareCore.Specifications
{
    public class UserSpecification: GenericSearchSpecs
    {

        public string? Title { get; set; }
        public int Status { get; set; }
        public string? OrderBy { get; set; }
    }
}
