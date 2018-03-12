using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TestWebApi.Custom
{
    public class GlobalValidationFilterAttribute : ActionFilterAttribute
    {
        //The method responds with Bad Request HttpStatus Code with the 
        //Model state validation errors
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                //actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);

                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);

                //foreach (var errMsg in ModelState.Values.SelectMany(d => d.Errors))
                //{
                //    exceptionMsg.Add(errMsg.ErrorMessage);
                //}

                //foreach (var item in ModelState.Values.SelectMany(d => d.Errors))
                //{
                //    excepMsg.Add(GlobalValidator.IsEntityEmpty(item.Exception) ? item.ErrorMessage : item.Exception.Message);

                //}
            }
        }
    }
}