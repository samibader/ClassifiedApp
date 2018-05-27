using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class PropertyDefinition : IEntityBase
    {
        public PropertyDefinition()
        {
            
        }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public PropertyTypes Type { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<PropertyValue> PropertyValues { get; set; }
    }
}
