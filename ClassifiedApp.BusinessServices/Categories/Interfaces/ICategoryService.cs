using ClassifiedApp.BusinessServices.Categories.ViewModels;
using ClassifiedApp.BusinessServices.Classifieds.ViewModels;
using ClassifiedApp.BusinessServices.SubCategories.ViewModels;
//using ClassifiedApp.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Categories.Interfaces
{
    /// <summary>
    /// Product Service Contract
    /// </summary>
    public interface ICategoryService
    {
        CategoryModel GetCategoryById(int categoryId);
        List<CategoryModel> GetAllCategories(string lang);
        int CreateCategory(InputCategoryModel categoryModel);
        bool UpdateCategory(int categoryId, CategoryModel categoryModel);
        bool DeleteCategory(int categoryId);
        List<PropertyDefinitionModel> GetCategoryPropertyDefinitions(int categoryId);
        List<SubCategoryModel> GetCategorySubs(int categoryId);
    }
}
