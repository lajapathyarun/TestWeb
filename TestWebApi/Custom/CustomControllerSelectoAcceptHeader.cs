using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebAPI.Custom
{
    public class CustomControllerSelectoAcceptHeader : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;

        public CustomControllerSelectoAcceptHeader(HttpConfiguration config) : base(config)
        {
            _config = config;
        }

        public override HttpControllerDescriptor
            SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();
            var routeData = request.GetRouteData();

            var controllerName = routeData.Values["controller"].ToString();

            string versionNumber = "1";

            // Get the version number from the Accept header

            // Users can include multiple Accept headers in the request
            // Check if any of the Accept headers has a parameter with name version
            var acceptHeader = request.Headers.Accept.Where(a => a.Parameters
                                .Count(p => p.Name.ToLower() == "version") > 0);

            // If there is atleast one header with a "version" parameter
            if (acceptHeader.Any())
            {
                // Get the version parameter value from the Accept header
                versionNumber = acceptHeader.First().Parameters
                                .First(p => p.Name.ToLower() == "version").Value;
            }

            HttpControllerDescriptor controllerDescriptor;
            if (versionNumber == "1")
            {
                controllerName = string.Concat(controllerName, "V1");
            }
            else
            {
                controllerName = string.Concat(controllerName, "V2");
            }

            if (controllers.TryGetValue(controllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }

            return null;
        }
    }
}