using ChildCare.Models.Security;
using Microsoft.AspNetCore.Mvc.Razor;

namespace ChildCare.Code.Attributes
{


        public abstract class BaseViewPage<TModel> : RazorPage<TModel>
        {
            protected CustomPrincipal CurrentUser => new CustomPrincipal(ContextProvider.HttpContext.User);

            protected object getHtmlAttributes(bool readonl, string cssClass)
            {
                if (readonl)
                {
                    return new { @class = cssClass, @readonly = true };
                }
                return new { @class = cssClass };
            }

        }


    
}
