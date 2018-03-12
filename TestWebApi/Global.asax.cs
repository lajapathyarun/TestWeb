using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TestWebApi
{
    public class UserDetail
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class WebApiApplication : System.Web.HttpApplication
    {
        public static List<UserDetail> UserDetails;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureData();
        }

        public void ConfigureData()
        {
            UserDetails = new List<UserDetail>() {
                new UserDetail { UserName= "arun", Password="arun"},
                new UserDetail { UserName= "abi", Password="abi"}
            };
        }
    }
}
