using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using ClassifiedApp.BusinessModels;
using ClassifiedApp.BusinessServices;
using ClassifiedApp.WebApi.Filters;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Globalization;
using System.Threading;
using ClassifiedApp.Resources;
using ClassifiedApp.WebApi.ActionFilters;
using ClassifiedApp.BusinessServices.Users.Interfaces;
using ClassifiedApp.BusinessServices.Users.ViewModels;
using System.Net.Mail;
using ClassifiedApp.BusinessServices.Notifications.ViewModels;
using ClassifiedApp.BusinessServices.Notifications.Interfaces;
//using ClassifiedApp.WebApi.ErrorHelper;

namespace ClassifiedApp.WebApi.Controllers
{
    
    public class AuthenticateController : ApiBaseController
    {
        private static CultureInfo DetermineBestCulture(HttpRequestMessage request)
        {
            // Somehow determine the best-suited culture for the specified request,
            // e.g. by looking at route data, passed headers, user preferences, etc.
            return request.GetRouteData().Values["lang"].ToString() == "en"
                ? CultureInfo.GetCultureInfo("en-US")
                : CultureInfo.GetCultureInfo("ar-AR");
        }
        #region Private variable.
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly IActivationCodeService _activationCodeService;
        #endregion
        #region Public Constructor
        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public AuthenticateController(ITokenService tokenService, IUserService userService, IActivationCodeService activationCodeService,INotificationService notificationService)
        {
            _tokenService = tokenService;
            _userService = userService;
            _activationCodeService = activationCodeService;
            _notificationService = notificationService;
        }
        #endregion
        /// <summary>
        /// Authenticates user and returns token with expiry.
        /// </summary>
        /// <returns></returns>
        [ApiAuthenticationFilter]
        [HttpPost]
        [Route("~/api/authenticate")]
        public HttpResponseMessage Authenticate()
        {
            if (System.Threading.Thread.CurrentPrincipal != null && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var basicAuthenticationIdentity = System.Threading.Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if (basicAuthenticationIdentity != null)
                {
                    var userId = basicAuthenticationIdentity.UserId;
                    return GetAuthToken(userId);
                }
            }
            return null;
        }

        // POST api/Account/Register
        [ValidateModel]
        [AllowAnonymous]
        [Route("~/api/Register")]
        public async Task<HttpResponseMessage> Register(RegisterUserModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    //return Request.CreateResponse(HttpStatusCode.NotAcceptable, GetValidationErrors());
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            //}

            if (!_userService.ValidateUniqueUsername(model.Username))
                throw new HttpResponseException(ErrorMessage(Resources.ErrorMessages.UsernameNotAvailable));

            int newUserId = _userService.CreateUser(model);
            var provider = this.Configuration
                           .DependencyResolver.GetService(typeof(IActivationCodeService)) as IActivationCodeService;
            var codeModel = provider.GenerateCode(newUserId, "GSM");
            bool response = false;
            string responseResult = "";
            try
            {
                var responseBody = await provider.SendCode(newUserId, codeModel);
                responseResult = responseBody.Body.SendSMSResult;
                response = true;
            }
            catch (Exception ex)
            {
                responseResult = "خطأ في إرسال كود التفعيل";
                response = false;
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { Message = "تم إنشاء الحساب بنجاح", Status = true, userId = newUserId, SendSMSResponseMessage = responseResult, RequestIsOK = response });

        }

        [ValidateModel]
        [AllowAnonymous]
        [Route("~/api/SendGsmActivationCode")]
        public async Task<HttpResponseMessage> SendGsmActivationCode(int userId)
        {
            var provider = this.Configuration
                               .DependencyResolver.GetService(typeof(IActivationCodeService)) as IActivationCodeService;
            bool response = false;
            var codeModel = provider.GenerateCode(userId, "GSM");
            string responseResult = "";
            try
            {
                var responseBody = await provider.SendCode(userId, codeModel);
                responseResult = responseBody.Body.SendSMSResult;
                response = true;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                responseResult = "خطأ في إرسال كود التفعيل";
                response = false;
            }
            if(response)
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "تم إرسال الرسالة بنجاح", Status = true, SendSMSResponseMessage = responseResult, RequestIsOK = response });
            else
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "خطأ في إرسال كود التفعيل", Status = false, SendSMSResponseMessage = responseResult, RequestIsOK = response });
            //return Request.CreateResponse(HttpStatusCode.OK, new { SendSMSResponseMessage = responseResult, RequestIsOK = response });
        }

        [AllowAnonymous]
        [Route("~/api/SendEmailActivationCode")]
        public HttpResponseMessage SendEmailActivationCode(int userId)
        {
            var provider = this.Configuration
                               .DependencyResolver.GetService(typeof(IActivationCodeService)) as IActivationCodeService;
            var codeModel = provider.GenerateCode(userId, "Email");
            
            if (!provider.SendEmailCode(userId,codeModel))
                throw new HttpResponseException(ErrorMessage("خطأ في عملية إرسال كود التفعيل عبر البريد الالكتروني"));

            return OkMessage("تم إرسال كود التفعيل بنجاح");
        }

        [AllowAnonymous]
        [Route("~/api/ReSendEmailActivationCode")]
        public HttpResponseMessage ReSendEmailActivationCode(int userId)
        {
            var provider = this.Configuration
                               .DependencyResolver.GetService(typeof(IActivationCodeService)) as IActivationCodeService;

            if (!provider.ReSendEmailCode(userId, "Email"))
                throw new HttpResponseException(ErrorMessage("خطأ في عملية إرسال كود التفعيل عبر البريد الالكتروني"));

            return OkMessage("تم إرسال كود التفعيل بنجاح");
        }

        [AllowAnonymous]
        [Route("~/api/ResendGsmActivationCode")]
        public async Task<HttpResponseMessage> ResendActivationCode(int userId)
        {
                var provider = this.Configuration
                               .DependencyResolver.GetService(typeof(IActivationCodeService)) as IActivationCodeService;
                bool response = false;
                string responseResult = "";
                try
                {
                    var responseBody = await provider.ReSendCode(userId, "GSM");
                    responseResult = responseBody.Body.SendSMSResult;
                    response = true;
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    responseResult = "خطأ في إرسال كود التفعيل";
                    response = false;
                }
                if (response)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = "تم إرسال الرسالة بنجاح", Status = true, SendSMSResponseMessage = responseResult, RequestIsOK = response });
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = "خطأ في إرسال كود التفعيل", Status = false, SendSMSResponseMessage = responseResult, RequestIsOK = response });
                //return Request.CreateResponse(HttpStatusCode.OK, new { SendSMSResponseMessage = responseResult, RequestIsOK = response });
           
        }

        [AllowAnonymous]
        [Route("~/api/ActivateGsm")]
        public HttpResponseMessage ActivateGsm(int userId,string code)
        {
            if(_activationCodeService.ValidateActivationCode(code,userId,"GSM"))
            {
                _userService.ActivateUserGSM(userId);
                _activationCodeService.DeleteByUserId(userId, "GSM");

                return OkMessage("تم تفعيل رقم هاتف المستخدم بنجاح");
            }
            //else
            //{
            throw new HttpResponseException(ErrorMessage("خطأ في عملية تفعيل رقم الهاتف"));

            //}
        }
        [AllowAnonymous]
        [Route("~/api/ActivateEmail")]
        public HttpResponseMessage ActivateEmail(int userId, string code)
        {
            if (_activationCodeService.ValidateActivationCode(code, userId, "Email"))
            {
                _userService.ActivateUserEmail(userId);
                _activationCodeService.DeleteByUserId(userId, "Email");
                return OkMessage("تم تفعيل بريد المستخدم بنجاح");
                //return Request.CreateResponse(HttpStatusCode.OK, );
            }
            //else
            //{
            throw new HttpResponseException(ErrorMessage("خطأ في عملية تفعيل البريد الالكتروني"));
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "خطأ في عملية تفعيل البريد الالكتروني");
            //}
        }

        

        //private Dictionary<string, List<string>> GetValidationErrors()
        //{
        //    Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
        //    foreach (var state in ModelState)
        //    {
        //        errors.Add(state.Key, state.Value.Errors.Select(e => e.ErrorMessage).ToList<string>());
        //    }

        //    return errors;
        //}
        
        /// <summary>
        /// Returns auth token for the validated user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private HttpResponseMessage GetAuthToken(int userId)
        {
            var token = _tokenService.GenerateToken(userId);
            var response = Request.CreateResponse(HttpStatusCode.OK, token);
            response.Headers.Add("Token", token.AuthToken.ToString());
            //response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["AuthTokenExpiry"]);
            response.Headers.Add("Access-Control-Expose-Headers", "Token");
            return response;
        }

        //[AllowAnonymous]
        //[Route("SendSMS")]
        //public async Task<IEnumerable<string>> SendSMS()
        //{
        //    var result = await GetExternalResponse();

        //    return new string[] { result, "value2" };
        //}

        //private async Task<string> GetExternalResponse()
        //{
        //    var client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync(_address);
        //    response.EnsureSuccessStatusCode();
        //    var result = await response.Content.ReadAsStringAsync();
        //    return result;
        //}
        [AuthorizationRequired]
        [HttpGet]
        [Route("~/api/ProtectedRequest")]
        public IEnumerable<string> ProtectedRequest()
        {
            return new string[] { "value1", "value2" };
        }

        [AuthorizationRequired]
        [HttpGet]
        [Route("~/api/Logout")]
        public HttpResponseMessage Logout()
        {
            if (_userService.Logout(CurrentUserId))
                return OkMessage("تم تسجيل الخروج بنجاح");
            else
                return ErrorMessage("لم تتم العملية بنجاح");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("~/api/RegisterDevice")]
        public HttpResponseMessage RegisterDevice([FromBody]InputDeviceModel model)
        {
            if(!_notificationService.RegisterDevice(model))
                throw new HttpResponseException(ErrorMessage("الجهاز مسجل مسبقاً"));
            return OkMessage("تم تسجيل الجهاز بنجاح");
        }
    }
}
