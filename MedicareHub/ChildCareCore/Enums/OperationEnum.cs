using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildCareCore.Enums
{
    public enum OperationEnum
    {
        [Description("Normal")]
        Normal = 1,
        [Description("Creatical")]
        Creatical = 2,

    }
     public enum GenderEnum
    {
        [Description("Male")]
        Male=1,
        [Description("Female")]
        Female=2,
        
    }

    

}


