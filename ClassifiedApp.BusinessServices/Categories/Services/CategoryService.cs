using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ClassifiedApp.BusinessModels;
using ClassifiedApp.DataAccess.UnitOfWork;
using ClassifiedApp.DataAccess.Extensions;
using AutoMapper;
using System.Transactions;
using ClassifiedApp.DataAccess;
using ClassifiedApp.Models;
using ClassifiedApp.BusinessServices.Categories.ViewModels;
using ClassifiedApp.BusinessServices.Categories.Interfaces;
using ClassifiedApp.BusinessServices.SubCategories.ViewModels;
using ClassifiedApp.BusinessServices.Classifieds.Interfaces;

namespace ClassifiedApp.BusinessServices.Categories.Services
{
    /// <summary>
    /// Offers services for product specific CRUD operations
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly UnitOfWork _unitOfWork;
        /// <summary>
        /// Public constructor.
        /// </summary>
        public CategoryService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches category details by id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public CategoryModel GetCategoryById(int categoryId)
        {
            var category = _unitOfWork.CategoryRepository.GetById(categoryId);
            if(category!=null)
            {
                //Mapper.CreateMap<Category, CategoryModel>();
                //Mapper.CreateMap<PropertyDefinition, PropertyDefinitionModel>();
                var categoryModel = Mapper.Map<Category, CategoryModel>(category);
                return categoryModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the Categories.
        /// </summary>
        /// <returns></returns>
        public List<CategoryModel> GetAllCategories(string lang)
        {
            IEnumerable<Category> categories ;
            if (lang!=null)
                categories = _unitOfWork.CategoryRepository.GetAll(x => x.Language == lang);
            else
                categories = _unitOfWork.CategoryRepository.GetAll();
            if (categories.Any())
            {
                
                //Mapper.CreateMap<Category, CategoryModel>();
                
                var categoriesModel = Mapper.Map<List<Category>, List<CategoryModel>>(categories.ToList());
                return categoriesModel;
            }
            return null;
        }

        public int CreateCategory(InputCategoryModel categoryModel)
        {
            using (var scope = new TransactionScope())
            {
                
                var category = new Category
                {
                    Name = categoryModel.Name,
                    Language=categoryModel.Language,
                    ImageUrl=categoryModel.ImageUrl
                };
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.Save();
                scope.Complete();
                return category.Id;
            }
        }

        public bool UpdateCategory(int categoryId, CategoryModel categoryModel)
        {
            var success = false;
            if (categoryModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var category = _unitOfWork.CategoryRepository.GetById(categoryId);
                    if (category != null)
                    {
                        category.Name = categoryModel.Name;
                        _unitOfWork.CategoryRepository.Edit(category);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteCategory(int categoryId)
        {
            var success = false;
            if (categoryId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var category = _unitOfWork.CategoryRepository.GetById(categoryId);
                    if (category != null)
                    {

                        _unitOfWork.CategoryRepository.Delete(category);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public List<PropertyDefinitionModel> GetCategoryPropertyDefinitions(int categoryId)
        {
            var propertyDefinitions = _unitOfWork.PropertyDefinitionRepository.GetAll(x => x.CategoryId == categoryId);
            if (propertyDefinitions.Any())
            {
                //Mapper.CreateMap<Category, CategoryModel>();
                //Mapper.CreateMap<PropertyDefinition, PropertyDefinitionModel>();
                var propertyDefinitionsModels = Mapper.Map<List<PropertyDefinition>, List<PropertyDefinitionModel>>(propertyDefinitions.ToList());
                return propertyDefinitionsModels;
            }
            return null;
        }


        public List<SubCategoryModel> GetCategorySubs(int categoryId)
        {
            var list = _unitOfWork.SubCategoryRepository.GetAll(x => x.CategoryId == categoryId);
            if (list.Any())
            {
                var modelsList = Mapper.Map<List<SubCategory>, List<SubCategoryModel>>(list.ToList());
                return modelsList;
            }
            return null;
        }
    }
}
