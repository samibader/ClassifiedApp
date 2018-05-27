using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Categories.ViewModels
{
    /// <summary>
    /// Category Model
    /// </summary>
    public class CategoryModel
    {
        public CategoryModel()
        {
            
        }
        /// <summary>
        /// ID of the category
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the category
        /// </summary>
        public string Name { get; set; }

        public string ImageUrl { get; set; }
        public string Language { get; set; }

        //public List<PropertyDefinitionModel> PropertyDefinitions { get; set; }
        public int SubCategoriesCount { get; set; }
        public int PropertyDefinitionsCount { get; set; }
        public int ClassifiedCount { get; set; }
    }
}
