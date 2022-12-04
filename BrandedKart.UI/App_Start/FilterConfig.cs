using System.Web;
using System.Web.Mvc;

namespace BrandedKart.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //<--Release Mode-->
            //GlobalFilters.Filters.Add(new RequireHttpsAttribute());
        }
    }
}
