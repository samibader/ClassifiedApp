using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using ClassifiedApp.BusinessModels;
using ClassifiedApp.BusinessServices;
using ClassifiedApp.WebApi.Filters;
using ClassifiedApp.WebApi.ActionFilters;
using ClassifiedApp.BusinessServices.Categories.ViewModels;
using ClassifiedApp.BusinessServices.Categories.Interfaces;
using System.Web;
using System.Threading.Tasks;
using ClassifiedApp.WebApi.Providers;
using ClassifiedApp.BusinessServices.Classifieds.ViewModels;
using System.IO;
using ClassifiedApp.BusinessServices.SubCategories.ViewModels;
using ClassifiedApp.BusinessServices.Classifieds.Interfaces;
//using ClassifiedApp.WebApi.ErrorHelper;

namespace ClassifiedApp.WebApi.Controllers
{
    public class CategoryController : ApiBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IClassifiedService _classifiedService;
        public CategoryController(ICategoryService categoryService,IClassifiedService classifiedService)
        {
            _categoryService = categoryService;
            _classifiedService = classifiedService;
        }

        /// <summary>
        /// Get all the categories (optional language parameter "ar" or "en").
        /// </summary>
        /// <returns>list of categories</returns>
        // GET: api/Category
        [HttpGet]
        [Route("~/api/Categories/{lang:goodlang?}")]
        public IEnumerable<CategoryModel> Get(string lang=null)
        {
            var categories = _categoryService.GetAllCategories(lang);
           
            if (categories!=null)
            {
                 //var categoriesModels = categories as List<CategoryModel> ?? categories.ToList();
                 //return Request.CreateResponse(HttpStatusCode.OK, categories);
                return categories;
            }
            else
                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "لا يوجد فئات");

                throw new HttpResponseException(NotFoundMessage("لا يوجد فئات"));
        }

        ///// <summary>
        ///// Get all the categories ordered by "Name" , testing for "Routing"
        ///// </summary>
        ///// <returns>
        ///// list of categories ordered by "Name"
        ///// </returns>
        ///// 
        //[HttpGet]
        //[Route("~/api/OrderedCategories")]
        //// GET: api/OrderedCategories
        //public HttpResponseMessage GetAllOrdered(string lang)
        //{
        //    var categories = _categoryService.GetAllCategories(lang);
        //    if (categories==null)
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "categories not found");
        //    var categoriesModels = categories as List<CategoryModel> ?? categories.ToList();
        //    if (categoriesModels.Any())
        //        return Request.CreateResponse(HttpStatusCode.OK, categoriesModels);
        //    else
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "categories not found");


        //}
        
        

        // GET: api/Category/5
        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id">id of the category</param>
        /// <returns>Category model</returns>
        public CategoryModel Get(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category != null)
                return category;
            throw new HttpResponseException(NotFoundMessage("لا يوجد فئة تتبع لهذا الرقم"));
        }

        // POST: api/Category
        /// <summary>
        /// Add new category
        /// </summary>
        /// <param name="categoryModel">new category model</param>
        /// <returns>id of the newly created model</returns>
        public int Post([FromBody]InputCategoryModel categoryModel)
        {
            //categoryModel.Language = CurrentLanguage;
            return _categoryService.CreateCategory(categoryModel);
        }

        // PUT: api/Category/5
        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="id">id of the category</param>
        /// <param name="categoryModel">category model with new data</param>
        /// <returns>boolean value</returns>
        public bool Put(int id, [FromBody]CategoryModel categoryModel)
        {
            if (id > 0)
            {
                return _categoryService.UpdateCategory(id, categoryModel);
            }
            return false;
        }

        // DELETE: api/Category/5
        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id">id of the category</param>
        /// <returns>boolean value</returns>
        public bool Delete(int id)
        {
            if (id > 0)
                return _categoryService.DeleteCategory(id);
            return false;
        }

        [HttpGet]
        [Route("~/api/Category/{catId:int}/PropertyDefinitions")]
        public IEnumerable<PropertyDefinitionModel> GetCategoryPropertyDefinitions(int catId)
        {
            var list = _categoryService.GetCategoryPropertyDefinitions(catId);

            if (list != null)
            {
                return list;
            }
            else
                throw new HttpResponseException(NotFoundMessage("لا يوجد واصفات خاصة لهذه الفئة"));
        }

        [HttpGet]
        [Route("~/api/Category/{catId:int}/SubCategories")]
        public IEnumerable<SubCategoryModel> GetCategorySubs(int catId)
        {
            var list = _categoryService.GetCategorySubs(catId);

            if (list != null)
            {
                return list;
            }
            else
                throw new HttpResponseException(NotFoundMessage("لا يوجد فئات فرعية لهذه الفئة"));
        }


        //[HttpPost]
        //[Route("~/api/Category/Upload")]
        //public async Task<HttpResponseMessage> UploadCategoryImage(HttpRequestMessage request, int categoryId)
        //{
        //    // Check if the request contains multipart/form-data.
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }
        //    string root = HttpContext.Current.Server.MapPath(string.Join("/","~",BusinessSettings.CategoryImageFolderBase));
        //    var provider = new MultipartFormDataStreamProvider(root);

        //    try
        //    {
        //        //return Request.CreateResponse(HttpStatusCode.OK, new { a = provider.FileData[0].Headers.ContentType});
        //        // Read the form data.
        //        await Request.Content.ReadAsMultipartAsync(provider);
        //        string aa = "";
        //        string bb = "";
        //        // This illustrates how to get the file names.
        //        foreach (MultipartFileData file in provider.FileData)
        //        {
        //            aa += file.Headers.ContentDisposition.FileName;
        //            bb += "Server file path: " + file.LocalFileName;
        //        }
        //        return Request.CreateResponse(HttpStatusCode.OK, new { a = aa, b = bb });
        //    }
        //    catch (System.Exception e)
        //    {
        //        Elmah.ErrorSignal.FromContext(HttpContext.Current).Raise(e);
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    }
        //}

        [HttpPost]
        [Route("~/api/Category/UploadImage")]
        public Task<IEnumerable<ClassifiedImageModel>> UploadImage()
        {
            string folderName = "images";
            string PATH = HttpContext.Current.Server.MapPath("~/" + BusinessSettings.CategoryImageFolderBase);
            string rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);

            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new CustomMultipartFormDataStreamProvider(PATH);
                var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<IEnumerable<ClassifiedImageModel>>(t =>
                {

                    if (t.IsFaulted || t.IsCanceled)
                    {
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                    }

                    var fileInfo = streamProvider.FileData.Select(i =>
                    {
                        var info = new FileInfo(i.LocalFileName);
                        return new ClassifiedImageModel() { ImageUrl = info.Name + "sss" + rootUrl + "/" + folderName + "/" + info.Name + " sss " + info.Length / 1024 };
                        
                    });
                    return fileInfo;
                });

                return task;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }

        }

        [HttpGet]
        [Route("~/api/Category/{id:int}/SearchClassifieds/{pageSize:int}/{pageNumber:int}/{keyword}")]
        public IHttpActionResult SearchClassifieds(int id, int pageSize, int pageNumber,string keyword)
        {
            int totalCount = 0;
            int totalPages = 0;
            var classifieds = _classifiedService.SearchByCatId(id,keyword, pageSize, pageNumber, out totalCount, out totalPages);
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
    }
}
