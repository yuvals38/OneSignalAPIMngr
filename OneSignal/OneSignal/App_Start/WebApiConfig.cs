using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;



namespace OneSignal
{
    public static class WebApiConfig
    {

        public const string URL_Notification = "https://onesignal.com/api/v1/apps";
        public const string APP_ID = "203d595c-0f60-435d-99bc-4bce584ebafc";
        public const string API_KEY = "ZDRiMjk3MWItMzZiMC00MzBhLTg5ZmYtMDA4YTEzNTM2YWRk";


        public static void Register(HttpConfiguration config)
        {
 
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
