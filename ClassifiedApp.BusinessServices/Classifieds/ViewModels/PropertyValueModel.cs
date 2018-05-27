using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Classifieds.ViewModels
{
    public class PropertyValueModel
    {
        public PropertyValueModel()
        {

        }
        public int PropertyValueId { get; set; }
        //public int ClassifiedId { get; set; }
        //public virtual Classified Classified { get; set; }
        public int PropertyDefinitionId { get; set; }
        public PropertyTypes PropertyDefinitionType { get; set; }
        public string PropertyDefinitionTypeString { get; set; }
        public string PropertyDefinitionName { get; set; }

        //public virtual PropertyDefinition PropertyDefinition { get; set; }
        public string Value { get; set; }
    }
}
