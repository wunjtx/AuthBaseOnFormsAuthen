using System.Web;
using System.Web.Mvc;

namespace AccountManager.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new AuthorizeAttribute());//will add [Authorize] to all controller
            filters.Add(new HandleErrorAttribute());
        }
    }
}
