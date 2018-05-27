using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.SubCategories.ViewModels
{
    public class SubCategoryModel
    {
        public SubCategoryModel()
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

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public int SubPropertyDefinitionsCount { get; set; }
        public int ClassifiedCount { get; set; }
    }
}
