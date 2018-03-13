using System.Web;
using System.Web.Mvc;
using TuinCentrumMVC.Filters;

namespace TuinCentrumMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            HandleErrorAttribute handleErrorAttributeDB = new HandleErrorAttribute();
            handleErrorAttributeDB.View = "DatabaseError";
            handleErrorAttributeDB.ExceptionType = typeof(System.Data.Entity.Core.EntityException);
            handleErrorAttributeDB.Order = 1;

            filters.Add(handleErrorAttributeDB);
            filters.Add(new HandleErrorAttribute());
            filters.Add(new StatistiekActionFilter());         
        }
    }
}
