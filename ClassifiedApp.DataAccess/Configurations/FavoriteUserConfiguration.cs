using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class FavoriteUserConfiguration : EntityBaseConfiguration<FavoriteUser>
    {
        public FavoriteUserConfiguration()
        {
            Property(f => f.FavoriteSenderId).IsRequired();
            Property(f => f.FavoriteReceiverId).IsRequired();

            HasRequired(m => m.FavoriteSender).WithMany(m => m.FavoritesSender).HasForeignKey(m => m.FavoriteSenderId).WillCascadeOnDelete(false);
            HasRequired(m => m.FavoriteReceiver).WithMany(m => m.FavoritesReceiver).HasForeignKey(m => m.FavoriteReceiverId).WillCascadeOnDelete(false);
            Property(l => l.CreationDate).IsRequired();
            //HasRequired(m => m.FavoriteReceiver).WithMany(m => m.FavoritesReceiver);
            //HasRequired(m => m.FavoriteSender).WithMany(m => m.FavoritesSender);
        }
    }
}
