using System.Web;
using System.Web.Mvc;

namespace Coopers.BusinessLayer.Database.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
