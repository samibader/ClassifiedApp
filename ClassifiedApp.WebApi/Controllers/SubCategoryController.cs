using ClassifiedApp.BusinessServices.Classifieds.Interfaces;
using ClassifiedApp.BusinessServices.SubCategories.Interfaces;
using ClassifiedApp.BusinessServices.SubCategories.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClassifiedApp.WebApi.Controllers
{
    public class SubCategoryController : ApiBaseController
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly IClassifiedService _classifiedService;
        public SubCategoryController(ISubCategoryService subCategoryService, IClassifiedService classifiedService)
        {
            _subCategoryService = subCategoryService;
            _classifiedService = classifiedService;
        }

        /// <summary>
        /// Get the SubCategoryModel for a specific subcategory
        /// </summary>
        /// <param name="id">id of the subcategory</param>
        /// <returns>SubCategory Model</returns>
        [HttpGet]
        [Route("~/api/SubCategory/{id:int}")]
        // GET: api/SubCategory/5
        public SubCategoryModel Get(int id)
        {
            var subcategory = _subCategoryService.GetSubCategoryById(id);
            if (subcategory != null)
                return subcategory;
            throw new HttpResponseException(NotFoundMessage("لا يوجد فئة فرعية تتبع لهذا الرقم"));
        }


        // POST: api/SubCategory
        /// <summary>
        /// Add new SubCategory
        /// </summary>
        /// <param name="subCategoryModel">new subcategory model</param>
        /// <returns>id of the newly created subcategory</returns>
        public int Post([FromBody]InputSubCategoryModel subCategoryModel)
        {
            return _subCategoryService.CreateSubCategory(subCategoryModel);
        }

        // PUT: api/SubCategory/5
        public bool Put(int id, [FromBody]SubCategoryModel subCategoryModel)
        {
            if (id > 0)
            {
                return _subCategoryService.UpdateSubCategory(id, subCategoryModel);
            }
            return false;
        }

        [HttpDelete]
        [Route("~/api/SubCategory/{id:int}")]
        // DELETE: api/SubCategory/5
        public bool Delete(int id)
        {
            if (id > 0)
                return _subCategoryService.DeleteSubCategory(id);
            return false;
        }

        [HttpGet]
        [Route("~/api/SubCategory/{id:int}/Classifieds/{pageSize:int}/{pageNumber:int}")]
        public IHttpActionResult GetBySubCategory(int id,int pageSize, int pageNumber)
        {
            int totalCount = 0;
            int totalPages = 0;
            var classifieds = _classifiedService.GetClassifiedBySubCategory(id,pageSize, pageNumber, out totalCount, out totalPages);
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

        [HttpGet]
        [Route("~/api/SubCategory/{id:int}/FeaturedClassifieds/{pageSize:int}/{pageNumber:int}")]
        public IHttpActionResult GetFeaturedBySubCategory(int id, int pageSize, int pageNumber)
        {
            int totalCount = 0;
            int totalPages = 0;
            var classifieds = _classifiedService.GetFeaturedClassifiedBySubCategory(id, pageSize, pageNumber, out totalCount, out totalPages);
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

        [HttpGet]
        [Route("~/api/SubCategory/{id:int}/SearchClassifieds/{pageSize:int}/{pageNumber:int}/{keyword}")]
        public IHttpActionResult SearchClassifieds(int id, int pageSize, int pageNumber, string keyword)
        {
            int totalCount = 0;
            int totalPages = 0;
            var classifieds = _classifiedService.SearchBySubCatId(id, keyword, pageSize, pageNumber, out totalCount, out totalPages);
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
