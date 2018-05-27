using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class FavoriteUser : IEntityBase
    {
        public int Id { get; set; }
        public int FavoriteSenderId { get; set; }
        public int FavoriteReceiverId { get; set; }
        public virtual User FavoriteSender { get; set; }
        public virtual User FavoriteReceiver { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
