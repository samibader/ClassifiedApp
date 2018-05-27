using ClassifiedApp.BusinessServices.Classifieds.Validators;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Classifieds.ViewModels
{
    public class SearchClassifiedModel
    {
        public SearchClassifiedModel()
        {
            
        }
        public string Country { get; set; }
        public string City { get; set; }
        public string Lang { get; set; }
        public AdTypeList? AdType { get; set; }
        public bool? WithImage { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public DateTime? MinPostingDate { get; set; }
        public DateTime? MaxPostingDate { get; set; }
        public string keyword { get; set; }
    }
}
