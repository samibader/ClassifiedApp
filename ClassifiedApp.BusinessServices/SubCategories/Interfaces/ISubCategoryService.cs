using ClassifiedApp.BusinessServices.SubCategories.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.SubCategories.Interfaces
{
    public interface ISubCategoryService
    {
        SubCategoryModel GetSubCategoryById(int subCategoryId);
        List<SubCategoryModel> GetSubCategories(int categoryId);
        int CreateSubCategory(InputSubCategoryModel subCategoryModel);
        bool UpdateSubCategory(int subCategoryId, SubCategoryModel subCategoryModel);
        bool DeleteSubCategory(int subCategoryId);
        //List<PropertyDefinitionModel> GetCategoryPropertyDefinitions(int categoryId);
    }
}
