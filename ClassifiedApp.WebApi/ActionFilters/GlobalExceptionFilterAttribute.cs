using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace ClassifiedApp.WebApi.ActionFilters
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var exceptionType = context.Exception.GetType();
            if (exceptionType == typeof(ValidationException))
            {
                //var resp = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(context.Exception.Message), ReasonPhrase = "ValidationException", };
                //throw new HttpResponseException(resp);
                //actionContext.Response = actionContext.Request.CreateErrorResponse(
                //    HttpStatusCode.BadRequest, actionContext.ModelState);
                var response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request 111");
                throw new HttpResponseException(response);
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                //var resp = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("Unauthorized Access"), ReasonPhrase = "sasasasas", };
                var response = context.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized Access 111");
                throw new HttpResponseException(response);
            }
            else 
            {
                var response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error 111");
                throw new HttpResponseException(response);
            }
            //base.OnException(actionExecutedContext);
        }
    }
}