﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace TestWebApi.Custom
{
    // Derive from the DefaultHttpControllerSelector class
    public class CustomControllerSelectorHeaders : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;

        public CustomControllerSelectorHeaders(HttpConfiguration config) : base(config)
        {
            _config = config;
        }

        public override HttpControllerDescriptor
            SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();
            var routeData = request.GetRouteData();

            var controllerName = routeData.Values["controller"].ToString();

            // Default the version number to 1
            string versionNumber = "1";

            // Comment the code that gets the version number from Query String
            // var versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);
            // if (versionQueryString["v"] != null)
            // {
            //     versionNumber = versionQueryString["v"];
            // }

            // Get the version number from Custom version header
            // This custom header can have any name. We have to use this
            // same header to specify the version when issuing a request
            string customHeader = "X-StudentService-Version";
            if (request.Headers.Contains(customHeader))
            {
                versionNumber = request.Headers.GetValues(customHeader).FirstOrDefault();
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