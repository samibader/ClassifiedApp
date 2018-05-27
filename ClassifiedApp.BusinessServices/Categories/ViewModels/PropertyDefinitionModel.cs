using ClassifiedApp.Models;
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
    public class PropertyDefinitionModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public PropertyTypes Type { get; set; }
        public string TypeString { get; set; }
        public string CategoryName { get; set; }
    }
}
