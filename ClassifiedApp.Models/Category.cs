using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class Category : IEntityBase
    {
        public Category()
        {
            PropertyDefinitions = new List<PropertyDefinition>();
            SubCategories = new List<SubCategory>();
            Language = "ar";
            ImageUrl = "placeholder.jpg";
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Language { get; set; }
        public virtual ICollection<PropertyDefinition> PropertyDefinitions { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
