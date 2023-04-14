using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildCareCore.Enums
{
    public enum UserRolesEnum
    {
        [Description("Provider")]
        Provider = 1,

        [Description("Clinic")]
        Clinic = 2,

        [Description("LabAssistant")]
        Laboratory = 3,

        [Description("SuperAdmin")]
        SuperAdmin = 4,
    }





}
