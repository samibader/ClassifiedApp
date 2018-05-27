using ClassifiedApp.BusinessServices.Classifieds.Interfaces;
using ClassifiedApp.BusinessServices.FeedbackTickets.Interfaces;
using ClassifiedApp.BusinessServices.FeedbackTickets.ViewModels;
using ClassifiedApp.BusinessServices.ReportTickets.Interfaces;
using ClassifiedApp.BusinessServices.ReportTickets.ViewModels;
using ClassifiedApp.BusinessServices.Users.Interfaces;
using ClassifiedApp.BusinessServices.Users.ViewModels;
using ClassifiedApp.WebApi.ActionFilters;
using ClassifiedApp.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClassifiedApp.WebApi.Controllers
{
    
    public class UserController : ApiBaseController
    {
        private readonly IUserService _userService;
        private readonly IClassifiedService _classifiedService;
        private readonly IReportService _reportService;
        private readonly IFeedbackService _feedbackService;
        public UserController(IUserService userService, IClassifiedService classifiedService,IReportService reportService, IFeedbackService feedbackService )
        {
            _userService = userService;
            _classifiedService = classifiedService;
            _reportService = reportService;
            _feedbackService = feedbackService;
        }

        [AuthorizationRequired]
        [HttpGet]
        [Route("~/api/User/Current")]
        public UserModel GetCurrentUserInfo()
        {
            var user = _userService.GetUserInfo(CurrentUserId);
            if (user != null)
                return user;
            throw new HttpResponseException(NotFoundMessage("لا يوجد مستخدم"));
        }

        [AuthorizationRequired]
        [HttpPut]
        [Route("~/api/User/Current/ChangePassword")]
        public bool ChangePassword([FromBody]ChangePasswordModel model)
        {
            var user = _userService.GetUserInfo(CurrentUserId);
            if (user == null)
                throw new HttpResponseException(NotFoundMessage("لا يوجد مستخدم"));
            
            if(!_userService.ValidatePassword(CurrentUserId,model.OldPassword))
                throw new HttpResponseException(NotFoundMessage("كلمة المرور القديمة غير صحيحة"));

            return _userService.ChangePassword(CurrentUserId, model);
        }

        [AuthorizationRequired]
        [HttpPut]
        [Route("~/api/User/Current/EditProfile")]
        public HttpResponseMessage EditProfile([FromBody]EditProfileModel model)
        {
            var user = _userService.GetUserInfo(CurrentUserId);
            if (user == null)
                throw new HttpResponseException(NotFoundMessage("لا يوجد مستخدم"));
            bool result = _userService.EditProfile(CurrentUserId, model);
            if(!result)
                throw new HttpResponseException(ErrorMessage("خطأ في عملية تعديل بروفايل المستخدم"));
            return OkMessage("تم التعديل بنجاح");
        }


        /// <summary>
        /// Add another User to the favorite list of the current user
        /// </summary>
        /// <param name="favReceiverId">Another User ID</param>
        /// <returns>bool</returns>
        /// 
        [AuthorizationRequired]
        [Route("~/api/User/Current/FavoriteAnotherUser/{favReceiverId:int}")]
        public bool FavoriteAnotherUser(int favReceiverId)
        {
            if (!_userService.CheckIfExists(favReceiverId))
                throw new HttpResponseException(NotFoundMessage("المستخدم الذي يتم تفضيله غير موجود"));

            if (_userService.IsPreviouslyFavorited(CurrentUserId, favReceiverId))
                throw new HttpResponseException(NotFoundMessage("المستخدم تمت إضافته مسبقاً للمفضّلة"));

            return _userService.FavoriteAnotherUser(CurrentUserId, favReceiverId);
                
        }

        /// <summary>
        /// remove another User from the favorite list of the current user
        /// </summary>
        /// <param name="favReceiverId">Another User ID</param>
        /// <returns></returns>
        [AuthorizationRequired]
        [Route("~/api/User/Current/UnFavoriteAnotherUser/{favReceiverId:int}")]
        public bool UnFavoriteAnotherUser(int favReceiverId)
        {
            if (!_userService.CheckIfExists(favReceiverId))
                throw new HttpResponseException(NotFoundMessage("المستخدم الذي يتم إلغاء تفضيله غير موجود"));

            if (!_userService.IsPreviouslyFavorited(CurrentUserId, favReceiverId))
                throw new HttpResponseException(NotFoundMessage("المستخدم لم تتم إضافته مسبقاً للمفضّلة"));

            return _userService.UnFavoriteAnotherUser(CurrentUserId, favReceiverId);
                
        }

        [AuthorizationRequired]
        [Route("~/api/User/Current/UserIsPreviouslyFavorited/{favReceiverId:int}")]
        public bool UserIsPreviouslyFavorited(int favReceiverId)
        {
            return _userService.IsPreviouslyFavorited(CurrentUserId, favReceiverId);

        }

        [AuthorizationRequired]
        [Route("~/api/User/Current/LikeClassified/{classifiedId:int}")]
        public bool LikeClassified(int classifiedId)
        {
            if (!_classifiedService.CheckIfActive(classifiedId))
                throw new HttpResponseException(NotFoundMessage("هذا الإعلان غير متاح"));

            if (_classifiedService.IsPreviouslyLiked(CurrentUserId, classifiedId))
                throw new HttpResponseException(NotFoundMessage("الإعلان تم الإعجاب به مسبقاً"));

            return _classifiedService.LikeClassified(CurrentUserId, classifiedId);

        }

        [AuthorizationRequired]
        [Route("~/api/User/Current/FavoriteClassified/{classifiedId:int}")]
        public bool FavoriteClassified(int classifiedId)
        {
            if (!_classifiedService.CheckIfActive(classifiedId))
                throw new HttpResponseException(NotFoundMessage("هذا الإعلان غير متاح"));

            if (_classifiedService.IsPreviouslyFavorited(CurrentUserId, classifiedId))
                throw new HttpResponseException(NotFoundMessage("الإعلان تمت إضافته مسبقاً للمفضّلة"));

            return _classifiedService.FavoriteClassified(CurrentUserId, classifiedId);

        }

        [AuthorizationRequired]
        [Route("~/api/User/Current/UnLikeClassified/{classifiedId:int}")]
        public bool UnLikeClassified(int classifiedId)
        {
            if (!_classifiedService.CheckIfActive(classifiedId))
                throw new HttpResponseException(NotFoundMessage("هذا الإعلان غير متاح"));


            if (!_classifiedService.IsPreviouslyLiked(CurrentUserId, classifiedId))
                throw new HttpResponseException(NotFoundMessage("الإعلان لم يتم الإعجاب به مسبقاً"));

            return _classifiedService.UnLikeClassified(CurrentUserId, classifiedId);
        }
        [AuthorizationRequired]
        [Route("~/api/User/Current/IsPreviouslyLiked/{classifiedId:int}")]
        public bool IsPreviouslyLiked(int classifiedId)
        {

            return _classifiedService.IsPreviouslyLiked(CurrentUserId, classifiedId);
                
        }

        [AuthorizationRequired]
        [Route("~/api/User/Current/RateClassified/{classifiedId:int}/{rate:int:range(0,5)}")]
        public bool RateClassified(int classifiedId,byte rate)
        {
            if (!_classifiedService.CheckIfActive(classifiedId))
                throw new HttpResponseException(NotFoundMessage("هذا الإعلان غير متاح"));
         
            return _classifiedService.RateClassified(CurrentUserId, classifiedId,rate);

        }

        [AuthorizationRequired]
        [Route("~/api/User/Current/UnFavoriteClassified/{classifiedId:int}")]
        public bool UnFavoriteClassified(int classifiedId)
        {
            if (!_classifiedService.CheckIfActive(classifiedId))
                throw new HttpResponseException(NotFoundMessage("هذا الإعلان غير متاح"));
                

            if (!_classifiedService.IsPreviouslyFavorited(CurrentUserId, classifiedId))
                throw new HttpResponseException(NotFoundMessage("الإعلان لم تتم إضافته مسبقاً للمفضّلة"));

            return _classifiedService.UnFavoriteClassified(CurrentUserId, classifiedId);
        }

        [AuthorizationRequired]
        [Route("~/api/User/Current/ClassifiedIsPreviouslyFavorited/{classifiedId:int}")]
        public bool ClassifiedIsPreviouslyFavorited(int classifiedId)
        {
            return _classifiedService.IsPreviouslyFavorited(CurrentUserId, classifiedId);
                
        }

        [AuthorizationRequired]
        [HttpPost]
        [Route("~/api/User/Current/ReportAnotherUser")]
        public int ReportAnotherUser([FromBody] InputReportUserTicketModel model)
        {
            return _reportService.ReportUser(CurrentUserId, model);
        }

        [AuthorizationRequired]
        [HttpPost]
        [Route("~/api/User/Current/ReportClassified")]
        public int ReportClassified([FromBody] InputReportClassifiedTicketModel model)
        {
            return _reportService.ReportClassified(CurrentUserId, model);
        }

        [AuthorizationRequired]
        [HttpPost]
        [Route("~/api/User/Current/SendFeedback")]
        public int SendFeedback([FromBody] InputFeedbackTicketModel model)
        {
            return _feedbackService.SendFeedback(CurrentUserId, model);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("~/api/User/{id:int}/Classifieds/{pageSize:int}/{pageNumber:int}")]
        public IHttpActionResult GetByUser(int id, int pageSize, int pageNumber)
        {
            int totalCount = 0;
            int totalPages = 0;
            var classifieds = _classifiedService.GetClassifiedByUser(id, pageSize, pageNumber, out totalCount, out totalPages);
            if (classifieds == null)
                throw new HttpResponseException(NotFoundMessage("لا يوجد إعلانات"));
            var result = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Classifieds = classifieds
            };

            return Ok(result);
        }

        [AuthorizationRequired]
        [HttpGet]
        [Route("~/api/User/Current/FavoriteOwners")]
        public List<ClassifiedOwnerModel> GetCurrentUserFavoriteOwners()
        {
            var list = _userService.GetUserFavoriteOwners(CurrentUserId);
            if (list != null)
                return list;
            throw new HttpResponseException(NotFoundMessage("لا يوجد"));
        }

        [AuthorizationRequired]
        [HttpGet]
        [Route("~/api/User/Current/FavoriteClassifieds/{pageSize:int}/{pageNumber:int}")]
        public IHttpActionResult GetByUser(int pageSize, int pageNumber)
        {
            int totalCount = 0;
            int totalPages = 0;
            var classifieds = _classifiedService.GetClassifiedFavoritedByUser(CurrentUserId, pageSize, pageNumber, out totalCount, out totalPages);
            if (classifieds == null)
                throw new HttpResponseException(NotFoundMessage("لا يوجد إعلانات"));
            var result = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Classifieds = classifieds
            };

            return Ok(result);
        }



        [AllowAnonymous]
        [HttpGet]
        [Route("~/api/User/Test")]
        public List<TokenModel> GetAllUsers()
        {
            return _userService.Test();
        }
    }
}
