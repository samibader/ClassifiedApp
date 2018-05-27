using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Classifieds.ViewModels
{
    public class InputClassifiedImageModel
    {
        public int ClassifiedId { get; set; }
        public string ImageGuid { get; set; }
        public string ImageExtension { get; set; }
        public bool IsMainImage { get; set; }
    }
}
