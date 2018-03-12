using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApi.CustomFilters
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Filters;

    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public CustomExceptionFilter(string err = "")
        {

        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;

           
            //We can log this exception message to the file or database.  
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An unhandled exception was thrown by service1."),
                ReasonPhrase = "Internal Server Error.Please Contact your Administrator."
            };

            actionExecutedContext.Response = response;
        }

        //public override void OnException(HttpActionExecutedContext actionExecutedContext)
        //{
        //    string exceptionMessage = string.Empty;
        //    if (actionExecutedContext.Exception.InnerException == null)
        //    {
        //        exceptionMessage = actionExecutedContext.Exception.Message;
        //    }
        //    else
        //    {
        //        exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
        //    }
        //    //We can log this exception message to the file or database.  
        //    var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        //    {
        //        Content = new StringContent("An unhandled exception was thrown by service1."),
        //        ReasonPhrase = "Internal Server Error.Please Contact your Administrator."
        //    };

        //    actionExecutedContext.Response = response;
        //}
    }
}