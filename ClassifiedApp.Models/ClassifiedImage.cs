using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class ClassifiedImage : IEntityBase
    {
        public ClassifiedImage()
        {

        }
        public int Id { get; set; }
        public int ClassifiedId { get; set; }
        public virtual Classified Classified { get; set; }
        public string ImageGuid { get; set; }
        public string ImageExtension { get; set; }
        public bool IsMainImage { get; set; }
    }
}
