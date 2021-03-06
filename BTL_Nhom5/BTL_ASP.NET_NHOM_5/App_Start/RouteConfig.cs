using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BTL_ASP.NET_NHOM_5
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           
            routes.MapRoute(
   name: "Payment",
   url: "thanh-toan",
   defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
   namespaces: new[] { "BTL_ASP.NET_NHOM_5.Controllers" }
);
            routes.MapRoute(
            name: "Add Cart",
            url: "them-gio-hang",
            defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
            namespaces: new[] { "BTL_ASP.NET_NHOM_5.Controllers" }
        );
            routes.MapRoute(
          name: "Payment Success",
          url: "hoan-thanh",
          defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
          namespaces: new[] { "BTL_ASP.NET_NHOM_5.Controllers" }
      );
            routes.MapRoute(
       name: "Cart",
       url: "gio-hang",
       defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
       namespaces: new[] { "OnlineShop.Controllers" }
   );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "BTL_ASP.NET_NHOM_5.Controllers" }
            );

        }
    }
}
