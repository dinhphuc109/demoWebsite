using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace demoWebOrderFood
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*botdetect}",
   new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });


            routes.MapRoute(
                name: "Trade Category",
                url: "san-pham/{Metatitle}-{id}",
                defaults: new { controller = "Trade", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "Detail",
                url: "chi-tiet/{Metatitle}-{id}",
                defaults: new { controller = "Trade", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );
            routes.MapRoute(
                name: "Content Detail",
                url: "tin-tuc/{Metatitle}-{id}",
                defaults: new { controller = "Content", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "Tags",
                url: "tag/{tagId}",
                defaults: new { controller = "Content", action = "Tag", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "Content",
                url: "tin-tuc",
                defaults: new { controller = "Content", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "Search",
                url: "tim-kiem",
                defaults: new { controller = "Trade", action = "Search", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "ReSearchPost",
                url: "post-recommended",
                defaults: new { controller = "SearchPost", action = "ReSearchPost", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "news Search",
                url: "news-search",
                defaults: new { controller = "Content", action = "Search", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "Profile",
                url: "user-profile",
                defaults: new { controller = "UserProfile", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "Cart",
                url: "do-dung",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );
            

            routes.MapRoute(
                name: "order",
                url: "order",
                defaults: new { controller = "Trade", action = "order", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "GiveAndReceive",
                url: "give",
                defaults: new { controller = "Cart", action = "GiveAndReceive", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "Add Cart",
                url: "dang-ki-nhan",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "success",
                url: "success",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "demoWebOrderFood.Controllers" }
            );
        }
    }
}
