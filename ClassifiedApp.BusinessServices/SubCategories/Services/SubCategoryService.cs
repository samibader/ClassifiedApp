using ClassifiedApp.BusinessServices.SubCategories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassifiedApp.BusinessServices.SubCategories.ViewModels;
using ClassifiedApp.DataAccess.UnitOfWork;
using AutoMapper;
using ClassifiedApp.Models;
using System.Transactions;

namespace ClassifiedApp.BusinessServices.SubCategories.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly UnitOfWork _unitOfWork;

        public SubCategoryService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public SubCategoryModel GetSubCategoryById(int subCategoryId)
        {
            var subCategory = _unitOfWork.SubCategoryRepository.GetById(subCategoryId);
            if (subCategory != null)
            {
                var subCategoryModel = Mapper.Map<SubCategory, SubCategoryModel>(subCategory);
                return subCategoryModel;
            }
            return null;
        }

        public List<SubCategoryModel> GetSubCategories(int categoryId)
        {
            IEnumerable<SubCategory> subCategories;
            subCategories = _unitOfWork.SubCategoryRepository.GetAll(x => x.CategoryId == categoryId);
            if (subCategories.Any())
            {
                var list = Mapper.Map<List<SubCategory>, List<SubCategoryModel>>(subCategories.ToList());
                return list;
            }
            return null;
        }


        public int CreateSubCategory(InputSubCategoryModel subCategoryModel)
        {
            using (var scope = new TransactionScope())
            {

                var subcategory = new SubCategory
                {
                    Name = subCategoryModel.Name,
                    CategoryId = subCategoryModel.CategoryId,
                    ImageUrl = subCategoryModel.ImageUrl
                };
                _unitOfWork.SubCategoryRepository.Add(subcategory);
                _unitOfWork.Save();
                scope.Complete();
                return subcategory.Id;
            }
        }

        public bool UpdateSubCategory(int subCategoryId, SubCategoryModel subCategoryModel)
        {
            var success = false;
            if (subCategoryModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var subcategory = _unitOfWork.SubCategoryRepository.GetById(subCategoryId);
                    if (subcategory != null)
                    {
                        subcategory.Name = subCategoryModel.Name;
                        subcategory.ImageUrl = subCategoryModel.ImageUrl;
                        _unitOfWork.SubCategoryRepository.Edit(subcategory);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteSubCategory(int subCategoryId)
        {
            var success = false;
            if (subCategoryId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var subcategory = _unitOfWork.SubCategoryRepository.GetById(subCategoryId);
                    if (subcategory != null)
                    {

                        _unitOfWork.SubCategoryRepository.Delete(subcategory);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }


    }
}
