using System.Web;
using System.Web.Mvc;

namespace MyWebFoundation.Webserver
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
