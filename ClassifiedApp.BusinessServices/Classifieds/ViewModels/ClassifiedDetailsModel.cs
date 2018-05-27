using ClassifiedApp.BusinessServices.Users.ViewModels;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Classifieds.ViewModels
{
    public class ClassifiedDetailsModel
    {
        public ClassifiedDetailsModel()
        {

        }
        
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? PostingDate { get; set; }
        public ClassifiedOwnerModel Owner { get; set; } 
        public int UserId { get; set; }
        public int SubCategoryId { get; set; }
        public AdTypeList AdType { get; set; }
        public Double? AdPrice { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public ClassifiedImageModel MainImage { get; set; }
        public DateTime AddingDate { get; set; }
        public string Notes { get; set; }
        public bool IsFeatured { get; set; }
        public List<ClassifiedImageModel> ClassifiedImageGallery { get; set; }
        public long ViewCount { get; set; }
        public long TotalCountLikes { get; set; }
        public AdStatusList Status { get; set; }
        public int TotalCountFavorited { get; set; }
        public double Rate { get; set; }
        public List<PropertyValueModel> Properties { get; set; }
        //public virtual ICollection<Like> Likes { get; set; }
    }
}
