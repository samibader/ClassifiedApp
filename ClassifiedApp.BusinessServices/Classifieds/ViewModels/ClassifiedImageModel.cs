using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Classifieds.ViewModels
{
    public class ClassifiedImageModel
    {
        public ClassifiedImageModel()
        {

        }
        //public int Id { get; set; }
        //public int ClassifiedId { get; set; }
        //public string ImageGuid { get; set; }
        //public string ImageExtension { get; set; }
        //public bool IsMainImage { get; set; }
        public string ImageUrl { get; set; }
        
        //public override string ToString()
        //{
        //    return string.Join("/", BusinessSettings.BaseUrl, BusinessSettings.SubCategoryImageFolderBase, this.ImageGuid + "." + this.ImageExtension);
        //}
    }
}
