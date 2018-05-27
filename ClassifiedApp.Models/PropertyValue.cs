using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class PropertyValue : IEntityBase
    {
        public PropertyValue()
        {
            
        }
        public int Id { get; set; }
        public int ClassifiedId { get; set; }
        public virtual Classified Classified { get; set; }
        public int PropertyDefinitionId { get; set; }
        public virtual PropertyDefinition PropertyDefinition { get; set; }
        public string Value { get; set; }

    }
}
