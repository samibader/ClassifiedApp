using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using ClassifiedApp.Models;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class LikeConfiguration : EntityBaseConfiguration<Like>
    {
        public LikeConfiguration()
        {
            Property(l => l.UserId).IsRequired();
            Property(l => l.ClassifiedId).IsRequired();
            Property(l => l.CreationDate).IsRequired();
        }
    }
}
