using System.Web;
using System.Web.Mvc;

namespace AspNetWebApi.OutputCache.Redis.Demo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
