using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Classifieds.ViewModels
{
    public class ClassifiedModel
    {
        public ClassifiedModel()
        {

        }
        //public List<ClassifiedImageModel> AdditionalImages { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? PostingDate { get; set; }
        public string ShortDescription { get; set; }
        //public int UserId { get; set; }
        public int SubCategoryId { get; set; }
        //public AdTypeList AdType { get; set; }
        //public Double? AdPrice { get; set; }
        //public string Country { get; set; }
        //public string City { get; set; }
        
       
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public ClassifiedImageModel MainImage { get; set; }
        //public DateTime AddingDate { get; set; }
        
        //public string Notes { get; set; }
        public bool IsFeatured { get; set; }
        
        //public List<string> AdditionalImagesUrl { get; set; }
        //public long ViewCount { get; set; }
        public string Status { get; set; }
        //public virtual ICollection<Like> Likes { get; set; }
        public double AverageRate { get; set; }
    }
}
