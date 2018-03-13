using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace TuinCentrumMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Routes in controllers activeren
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "Alleplanten",
                "Plantenlijst",
                new { controller = "Plants", action = "Index" }
            );

            routes.MapRoute(
                "PlantByNr",
                "Plants/{id}",
                new { controller = "Plants", action = "Details" },
                new { id = new IntRouteConstraint() }
            );

            routes.MapRoute(
                "PlantenVanEenSoort",
                "Soorts/{soortnaam}/planten",
                new { controller = "Plants", action = "FindPlantsBySoortNaam" },
                new { soortnaam = new CompoundRouteConstraint(
                    new List<IRouteConstraint>
                    {
                        new AlphaRouteConstraint(),
                        new MinLengthRouteConstraint(3),
                        new MaxLengthRouteConstraint(10)})}
            );

            routes.MapRoute(
                "PlantenVanEenLeverancier",
                "leveranciers/{levnr}/planten",
                new { controller = "Plants", action = "FindPlantenByLeverancier" },
                new { levnr = new MaxRouteConstraint(10) }
            );

            routes.MapRoute(
                "FindPlantenByPrijsBetween",
                "plants",
                new { controller = "Plants", action = "FindPlantenBetweenPrijzen" },
                new { QueryConstraint = new QueryStringConstraint(new string[] { "minprijs", "maxprijs" })}
            );

            routes.MapRoute(
                "FindPlantenByKleur", 
                "plants",
                new { controller = "Plants", action = "FindPlantenVanEenKleur" },
                new { QueryConstraint = new QueryStringConstraint( new string[] { "kleur" })}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
