using ClassifiedApp.BusinessServices;
using ClassifiedApp.BusinessServices.Classifieds.Interfaces;
using ClassifiedApp.BusinessServices.Classifieds.ViewModels;
using ClassifiedApp.WebApi.ActionFilters;
using ClassifiedApp.WebApi.Providers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ClassifiedApp.WebApi.Controllers
{
    
    public class ClassifiedController : ApiBaseController
    {
        private readonly IClassifiedService _classifiedService;
        public ClassifiedController(IClassifiedService classifiedService)
        {
            _classifiedService = classifiedService;
        }

        [AuthorizationRequired]
        [HttpPost]
        [Route("~/api/Classified")]
        public int Post([FromBody]InputClassifiedModel classifiedModel)
        {
            //if (!_classifiedService.UserCanAddNormalClassified(CurrentUserId))
                //throw new HttpResponseException(ErrorMessage("لا يمكن للمستخدم إضافة إعلان جديد حالياً"));
            classifiedModel.UserId = CurrentUserId;
            int newClassifiedId = _classifiedService.CreateNormalClassified(classifiedModel);
            return newClassifiedId;
        }

        [AuthorizationRequired]
        [HttpGet]
        [Route("~/api/MyClassifieds/{status:int:range(0,3)}")]
        public IEnumerable<ClassifiedModel> MyClassifieds(int status)
        {
            var classified = _classifiedService.GetClassifieds(CurrentUserId, status);

            if (classified != null)
                return classified;
            else
                throw new HttpResponseException(NotFoundMessage("لا يوجد"));
        }

        [AuthorizationRequired]
        [HttpPut]
        [Route("~/api/MyClassifieds/Edit/{classifiedId:int}")]
        public bool EditMyClassifieds(int classifiedId, [FromBody]EditClassifiedModel model)
        {
            if(!_classifiedService.ValidateUserClassified(CurrentUserId, classifiedId))
                throw new HttpResponseException(ErrorMessage("لا يمكنك تعديل هذا الإعلان"));
            return _classifiedService.EditClassified(model);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("~/api/Classified/public/{id:int}")]
        public ClassifiedModel GetPublic(int id)
        {
            var classified = _classifiedService.GetPublicById(id);

            if (classified != null)
                return classified;
            else
                throw new HttpResponseException(NotFoundMessage("لا يوجد إعلان يتبع لهذا الرقم"));
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("~/api/Classified/public/{id:int}/details")]
        public ClassifiedDetailsModel GetPublicDetails(int id)
        {
            var classified = _classifiedService.GetPublicDetailsById(id);

            if (classified != null)
            {
                _classifiedService.IncreaseViewCount(id);
                return classified;
            }
            else
                throw new HttpResponseException(NotFoundMessage("لا يوجد إعلان يتبع لهذا الرقم"));
        }

        [HttpGet]
        [Route("~/api/ClassifiedsPagingTest/{pageSize:int}/{pageNumber:int}")]
        public IHttpActionResult GetAll(int pageSize, int pageNumber)
        {
            int totalCount = 0;
            int totalPages = 0;
            var classifieds = _classifiedService.GetAllForPagingTest(pageSize, pageNumber, out totalCount, out totalPages);
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
        [Route("~/api/Classified/All/{id:int}")]
        public ClassifiedModel Get(int id)
        {
            var classified = _classifiedService.GetById(id);

            if (classified != null)
                return classified;
            else
                throw new HttpResponseException(NotFoundMessage("لا يوجد إعلان يتبع لهذا الرقم"));
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("~/api/Classified/All/{id:int}/details")]
        public ClassifiedDetailsModel GetDetails(int id)
        {
            var classified = _classifiedService.GetDetailsById(id);

            if (classified != null)
            {
                _classifiedService.IncreaseViewCount(id);
                return classified;
            }
            else
                throw new HttpResponseException(NotFoundMessage("لا يوجد إعلان يتبع لهذا الرقم"));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("~/api/Classified/All/{id:int}/SetPending")]
        public ClassifiedModel SetPending(int id)
        {
            if (_classifiedService.IfExists(id))
            {
                _classifiedService.SetClassifiedPending(id);
                var classified = _classifiedService.GetById(id);
                return classified;
            }
            else
                throw new HttpResponseException(NotFoundMessage("لا يوجد إعلان يتبع لهذا الرقم"));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("~/api/Classified/All/{id:int}/SetActive")]
        public ClassifiedModel SetActive(int id)
        {
            if (_classifiedService.IfExists(id))
            {
                _classifiedService.SetClassifiedActive(id);
                var classified = _classifiedService.GetById(id);
                return classified;
            }
            else
                throw new HttpResponseException(NotFoundMessage("لا يوجد إعلان يتبع لهذا الرقم"));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("~/api/Classified/All/{id:int}/SetExpired")]
        public ClassifiedModel SetExpired(int id)
        {
            if (_classifiedService.IfExists(id))
            {
                _classifiedService.SetClassifiedExpired(id);
                var classified = _classifiedService.GetById(id);
                return classified;
            }
            else
                throw new HttpResponseException(NotFoundMessage("لا يوجد إعلان يتبع لهذا الرقم"));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("~/api/Classified/All/{id:int}/SetRejected")]
        public ClassifiedModel SetExpired(int id, string rejectionNotes)
        {
            if (_classifiedService.IfExists(id))
            {
                _classifiedService.SetClassifiedRejected(id, rejectionNotes);
                var classified = _classifiedService.GetById(id);
                return classified;
            }
            else
                throw new HttpResponseException(NotFoundMessage("لا يوجد إعلان يتبع لهذا الرقم"));
        }

        [HttpPost]
        [Route("~/api/Classified/{id:int}/UploadImage")]
        public async Task<HttpResponseMessage> UploadImage(int id, string extension, bool? isMain = false)
        {
            string PATH = HttpContext.Current.Server.MapPath("~/" + BusinessSettings.ClassifiedImageFolderBase);
            //string rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);
            string imageData = await Request.Content.ReadAsStringAsync();
            byte[] bytes;
            try
            {
                bytes = Convert.FromBase64String(imageData);
            }
            catch (Exception)
            {
                throw new HttpResponseException(ErrorMessage("خطأ في عملية رفع الصورة"));
            }
            Image image;
            Guid g = Guid.NewGuid();
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
                switch (extension.ToLower())
                {
                    case "jpg":
                        image.Save(PATH + "/" + g.ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "png":
                        image.Save(PATH + "/" + g.ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case "gif":
                        image.Save(PATH + "/" + g.ToString() + ".gif", System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    default:
                        break;
                }

            }
            var model = new InputClassifiedImageModel()
            {
                ClassifiedId = id,
                ImageExtension = Path.GetExtension("."+extension.ToLower()),
                ImageGuid = g.ToString(),
                IsMainImage = isMain.HasValue ? isMain.Value : false
            };
            if(_classifiedService.AddClassifiedImage(model)==null)
                throw new HttpResponseException(ErrorMessage("خطأ في عملية رفع الصورة"));
                
            return OkMessage("تم الرفع بنجاح");

            //if (Request.Content.IsMimeMultipartContent())
            //{
            //    var streamProvider = new MyFormDataStreamProvider(PATH);
            //    await Request.Content.ReadAsMultipartAsync(streamProvider);
            //    if(!streamProvider.FileData.Any())
            //        throw new HttpResponseException(ErrorMessage("لا يمكن تحميل الملف المطلوب"));
            //    MultipartFileData file = streamProvider.FileData[0];
            //    var model = new InputClassifiedImageModel()
            //    {
            //        ClassifiedId = id,
            //        ImageExtension = Path.GetExtension(file.LocalFileName),
            //        ImageGuid =Path.GetFileNameWithoutExtension(file.LocalFileName),
            //        IsMainImage = isMain.HasValue ? isMain.Value : false
            //    };
            //    return _classifiedService.AddClassifiedImage(model);
            //}
            //else
            //{
            //    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            //}

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("~/api/Classified/TestPostImageBase64")]
        public async Task<string> TestPostImageBase64(string extension)
        {
            string PATH = HttpContext.Current.Server.MapPath("~/" + BusinessSettings.ClassifiedImageFolderBase);
            string imageData = await Request.Content.ReadAsStringAsync();
            
            //byte[] bytes = Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAKBueIx4ZKCMgoy0qqC+8P//8Nzc8P//////////////////////////////////////////////////////////2wBDAaq0tPDS8P//////////////////////////////////////////////////////////////////////////////wAARCAAKAAoDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwBVUFQSOf51HRRSND//2Q==");
            byte[] bytes = Convert.FromBase64String(imageData);
            Image image;
            //Guid g = Guid.NewGuid();
            int g = 1010101;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
                switch (extension.ToLower())
                {
                    case "jpg":
                        image.Save(PATH + "/" + g.ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "png":
                        image.Save(PATH + "/" + g.ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case "gif":
                        image.Save(PATH + "/" + g.ToString() + ".gif", System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    default:
                        break;
                }
                
            }
            return string.Join("/", BusinessSettings.BaseUrl, BusinessSettings.ClassifiedImageFolderBase, g.ToString() + "."+extension.ToLower());
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("~/api/Classified/TopRated/{fetchcount:int}")]
        public List<ClassifiedModel> GetTopRatedClassifieds(int fetchCount)
        {
            var classifieds = _classifiedService.GetTopRated(fetchCount);
            if (classifieds == null)
                throw new HttpResponseException(NotFoundMessage("لا يوجد إعلانات"));           
            return classifieds;
        }

        
        [AllowAnonymous]
        [HttpPost]
        [Route("~/api/Classified/TestPostFile")]
        public async Task<HttpResponseMessage> PostFile()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                StringBuilder sb = new StringBuilder(); // Holds the response body

                // Read the form data and return an async task.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the form data.
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        sb.Append(string.Format("{0}: {1}\n", key, val));
                    }
                }

                // This illustrates how to get the file names for uploaded files.
                foreach (var file in provider.FileData)
                {
                    FileInfo fileInfo = new FileInfo(file.LocalFileName);
                    sb.Append(string.Format("Uploaded file: {0} ({1} bytes)\n", fileInfo.Name, fileInfo.Length));
                }
                return new HttpResponseMessage()
                {
                    Content = new StringContent(sb.ToString())
                };
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("~/api/Classified/Search/{pageSize:int}/{pageNumber:int}/{keyword}")]
        public IHttpActionResult SearchClassifieds(int pageSize, int pageNumber, string keyword)
        {
            int totalCount = 0;
            int totalPages = 0;
            var classifieds = _classifiedService.SearchByKeyword(keyword, pageSize, pageNumber, out totalCount, out totalPages);
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
        [HttpPost]
        [Route("~/api/Classified/AdvancedSearch/{pageSize:int}/{pageNumber:int}")]
        public IHttpActionResult AdvancedSearchClassifieds([FromBody] SearchClassifiedModel model,int pageSize, int pageNumber)
        {
            int totalCount = 0;
            int totalPages = 0;
            var classifieds = _classifiedService.AdvancedSearch(model, pageSize, pageNumber, out totalCount, out totalPages);
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

        public Image LoadImage()
        {
            //get a temp image from bytes, instead of loading from disk
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAKBueIx4ZKCMgoy0qqC+8P//8Nzc8P//////////////////////////////////////////////////////////2wBDAaq0tPDS8P//////////////////////////////////////////////////////////////////////////////wAARCAAKAAoDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwBVUFQSOf51HRRSND//2Q==");

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }
    }
}
