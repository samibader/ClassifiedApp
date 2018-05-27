using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class FavoriteClassifiedConfiguration : EntityBaseConfiguration<FavoriteClassified>
    {
        public FavoriteClassifiedConfiguration()
        {
            Property(f => f.ClassifiedId).IsRequired();
            Property(f => f.UserId).IsRequired();

            HasRequired(m => m.User).WithMany(m => m.FavoriteClassifieds).HasForeignKey(m => m.UserId).WillCascadeOnDelete(false);
            HasRequired(m => m.Classified).WithMany(m => m.FavoriteUsers).HasForeignKey(m => m.ClassifiedId).WillCascadeOnDelete(false);
            Property(l => l.CreationDate).IsRequired();
            //HasRequired(m => m.FavoriteReceiver).WithMany(m => m.FavoritesReceiver);
            //HasRequired(m => m.FavoriteSender).WithMany(m => m.FavoritesSender);
        }
    }
}
