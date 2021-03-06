﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HMSNew
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "FEAccomodations",
                url: "{controller}/{action}/{id}",
                defaults: new {area ="", controller = "Accomodations", action = "Index" },
                namespaces:new []{ "HMSNew.Controllers" }
            );
            routes.MapRoute(
              name: "AccomodationPackageDetails",
              url: "accomodation-package/{accomodationPackageId}",
              defaults: new { area = "", controller = "Accomodations", action = "Details" },
              namespaces: new[] { "HMSNew.Controllers" }
          );
            routes.MapRoute(
            name: "CheckAvailability",
            url: "accomodation-check-availability",
            defaults: new { area = "", controller = "Accomodations", action = "CheckAvailability" },
            namespaces: new[] { "HMSNew.Controllers" }
        );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
