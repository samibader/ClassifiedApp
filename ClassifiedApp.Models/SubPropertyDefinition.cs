using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassifiedApp.Models
{
    public class SubPropertyDefinition : IEntityBase
    {
        public SubPropertyDefinition()
        {

        }
        public int Id { get; set; }
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public PropertyTypes Type { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
