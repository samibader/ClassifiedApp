using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class Notification : IEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public bool IsGeneral { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Seen { get; set; }
        public virtual User User { get; set; }
    }
    public enum NotificationType : byte
    {
        NewClassifiedByFavoriteOwner = 0,
        NewClassifiedByFavoriteCategory = 1,
        UserAddedToFavorite = 2,
        ClassifiedAddedToFavorite = 3,
        UserContactedByEmail = 4,
        UserClassifiedLiked = 5,
        UserReportedByAdmin = 6,
        ApplicationNewUpdate = 7,
        ApplicationNewFeature = 8
    }
}
