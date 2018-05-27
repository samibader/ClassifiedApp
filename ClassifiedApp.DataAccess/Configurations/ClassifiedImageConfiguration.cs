using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class ClassifiedImageConfiguration : EntityBaseConfiguration<ClassifiedImage>
    {
        public ClassifiedImageConfiguration()
        {
            Property(i => i.ClassifiedId).IsRequired();
            Property(i => i.ImageGuid).IsRequired().HasMaxLength(200);
            Property(i => i.ImageExtension).IsRequired().HasMaxLength(5);
        }
    }
}
