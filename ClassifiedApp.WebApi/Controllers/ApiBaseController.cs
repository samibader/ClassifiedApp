using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ClassifiedApp.WebApi.Controllers
{
    public class ApiBaseController : ApiController
    {
        protected string lang = "ar";
        
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            if (!controllerContext.Request.Headers.Contains("lang"))
                controllerContext.Request.Headers.Add("lang", "ar");

            
                var lang = controllerContext.Request.Headers.GetValues("lang").First();
                if (lang.ToLower() == "en")
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                    lang = "en";
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ar-SY");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-SY");
                    lang = "ar";
                }
            
            base.Initialize(controllerContext);
        }

        public HttpResponseMessage OkMessage(string message)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = message , Status=true });
        }

        public HttpResponseMessage ErrorMessage(string message)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = message, Status = false });
        }

        public HttpResponseMessage NotFoundMessage(string message)
        {
            //return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
            return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = message, Status = false });
        }

        public int CurrentUserId
        {
            get {
                return int.Parse(ControllerContext.Request.Headers.GetValues("User").First());
            }
        }

        public string CurrentLanguage
        {
            get {
                if (ControllerContext.Request.Headers.GetValues("lang").First().ToLower() == "en")
                    return "en";
                else
                    return "ar";
            }
        }

        
    }
}
