using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using TestWebApi.Custom;
using TestWebApi.CustomFilters;
using TestWebApi.DependencyResolution;
using TestWebApi.Repositories.Interfaces;
using TestWebApi.Repository;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;
using WebApiContrib.Formatting.Jsonp;

namespace TestWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Filters.Add(new ValidateModelStateFilter());

            // HANDLER
            //config.MessageHandlers();

            //DEPENDENCY INJECTION
            //var container = new UnityContainer();
            //container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());

            //config.DependencyResolver = new UnityResolver(container);

            //
            UnityConfig.RegisterComponents(config);

            ////Enabling CORS to Web API at Global Level
            //config.Filters.Add(new GlobalApiExpectionFilterAttribute());
            //config.Filters.Add(new CheckModelForNullAttribute());
            //config.Filters.Add(new GlobalValidationFilterAttribute());

            //HTTP to HTTPS RE-DIRECTION
            //config.Filters.Add(new RequireHttpsAttribute());

            //PostMan [Authorization:Basic YXJ1bjphcnVu]
            //https://www.base64encode.org/
            config.Filters.Add(new BasicAuthenticationAttribute());

            // Web API routes
            // ENABLE ATTRIBUTE ROUTING
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // Confgured & Enabled Global CORS
            //EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);

            // Confgured CORS
            config.EnableCors();

            //CUSTOM CONTROLLER SELECTOR USING QUERY STRING
            // config.Services.Replace(typeof(IHttpControllerSelector), new CustomControllerSelectorQueryString(config));

            //CUSTOM CONTROLLER SELECTOR USING Headers
            // config.Services.Replace(typeof(IHttpControllerSelector), new CustomControllerSelectorHeaders(config));

            //CUSTOM CONTROLLER SELECTOR USING ACCEPT Header
            // config.Services.Replace(typeof(IHttpControllerSelector), new CustomControllerSelectoAcceptHeader(config));

            //CUSTOM CONTROLLER SELECTOR USING CUSTOM MEDIA TYPES
            //config.Services.Replace(typeof(IHttpControllerSelector), new CustomControllerSelectorMediaTypes(config));

            //JsonFormatter SerializerSettings
            //config.Formatters.JsonFormatter.SerializerSettings.Formatting =Newtonsoft.Json.Formatting.Indented;
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            //    new CamelCasePropertyNamesContractResolver();

            //Only Support JSON FORMATTER
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            //ONLY support XML FORMATTER
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            //WEB BROWSER REQUEST (text/html)- APPROACH 1
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            //WEB BROWSER REQUEST(text/html) & RESPONSE (text/html)- APPROACH 2
            //config.Formatters.Add(new CustomJsonFormatter());

            //JSONP Formatter
            //var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            //config.Formatters.Insert(0, jsonpFormatter);

            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("application/vnd.pragimtech.employee.v1+json"));

            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("application/vnd.pragimtech.employee.v2+json"));

            // To add the custom media types to the XmlFormatter

            config.Formatters.XmlFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("application/vnd.pragimtech.employee.v1+xml"));

            config.Formatters.XmlFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("application/vnd.pragimtech.employee.v2+xml"));
        }
    }
}
