using ClassifiedApp.BusinessServices;
using ClassifiedApp.BusinessServices.Users.Interfaces;
using ClassifiedApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ClassifiedApp.WebApi.ActionFilters
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        private const string Token = "Token";
        private const string User = "User";

        
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            //  Get API key provider
            var provider = filterContext.ControllerContext.Configuration
                .DependencyResolver.GetService(typeof(ITokenService)) as ITokenService;
            var userServiceProvider = filterContext.ControllerContext.Configuration
                .DependencyResolver.GetService(typeof(IUserService)) as IUserService;

            if (filterContext.Request.Headers.Contains(Token))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(Token).First();
                var userValue = filterContext.Request.Headers.GetValues(User).First();

                // check if user gsm is activated
                if (userServiceProvider != null && !userServiceProvider.IsGsmApproved(Int32.Parse(userValue)))
                {
                    //var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "User is not activated", RequestMessage="" };
                    var responseMessage = filterContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ErrorMessages.UserGsmNotApproved);
                    filterContext.Response = responseMessage;
                }

                // Validate Token
                if (provider != null && !provider.ValidateToken(tokenValue,userValue))
                {
                    var responseMessage = filterContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ErrorMessages.InvalidToken);
                    filterContext.Response = responseMessage;
                }
            }
            else
            {
                filterContext.Response = filterContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ErrorMessages.UnauthorizedRequest);
            }

            base.OnActionExecuting(filterContext);

        }
    }
}