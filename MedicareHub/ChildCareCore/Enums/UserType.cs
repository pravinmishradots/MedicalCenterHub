using System.ComponentModel;

namespace ChildCareCore.Enums
{
    public enum UserType
    {
        [Description("Provider")]
        Provider = 1,

        [Description("Clinic")]
        Clinic = 2,

        [Description("Laboratory")]
        Laboratory = 3,

        [Description("Admin")]
        Admin = 4,


    }
}
