using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class SubCategory : IEntityBase
    {
        public SubCategory()
        {
            SubPropertyDefinitions = new List<SubPropertyDefinition>();
            ImageUrl = "placeholder.jpg";
            Classifieds = new List<Classified>();
        }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<SubPropertyDefinition> SubPropertyDefinitions { get; set; }
        public virtual ICollection<Classified> Classifieds { get; set; }
    }
}
