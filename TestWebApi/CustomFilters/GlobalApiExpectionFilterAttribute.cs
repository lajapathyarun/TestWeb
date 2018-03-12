using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace TestWebApi.Custom
{
    public class GlobalApiExpectionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;

            HttpResponseMessage response;

            if (exception is DbEntityValidationException)
            {
                List<string> excepMsg = new List<string>();

                foreach (var errMsg in ((DbEntityValidationException)exception).EntityValidationErrors.SelectMany(msg => msg.ValidationErrors))
                {
                    excepMsg.Add($"Error at '{errMsg.PropertyName}' due to: '{errMsg.ErrorMessage}'");
                }

                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(exception.Message)
                };
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(exception.Message)
                };
            }

            actionExecutedContext.Response = response;
        }
    }

    //    public override void OnException(HttpActionExecutedContext actionExecutedContext)
    //    {
    //        string exceptionMessage = string.Empty;

    //        if (actionExecutedContext.Exception.InnerException == null)
    //        {
    //            exceptionMessage = actionExecutedContext.Exception.Message;
    //        }
    //        else
    //        {
    //            exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
    //        }
    //        //We can log this exception message to the file or database.  
    //        var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
    //        {
    //            Content = new StringContent("An unhandled exception was thrown by service1."),
    //            ReasonPhrase = "Internal Server Error.Please Contact your Administrator."
    //        };

    //        actionExecutedContext.Response = response;
    //    }
    //}
}