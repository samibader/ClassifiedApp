using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class User : IEntityBase
    {
        public User()
        {
            Tokens = new List<Token>();
            ActivationCodes = new List<ActivationCode>();
            Classifieds = new List<Classified>();
            Likes = new List<Like>();
            FavoriteClassifieds = new List<FavoriteClassified>();
            IssuedReportUserTickets = new List<ReportUserTicket>();
            RelatedReportTickets = new List<ReportUserTicket>();
            IssuedReportClassifiedTickets = new List<ReportClassifiedTicket>();
            Notifications = new List<Notification>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public string GSM { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public bool IsEmailApproved { get; set; }
        public bool IsGsmApproved { get; set; }
        public bool IsBlocked { get; set; }
        public Guid UserGuid { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsGpsEnabled { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
        public virtual ICollection<ActivationCode> ActivationCodes { get; set; }
        public virtual ICollection<Classified> Classifieds { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<FavoriteUser> FavoritesSender { get; set; }
        public virtual ICollection<FavoriteUser> FavoritesReceiver { get; set; }
        public virtual ICollection<FavoriteClassified> FavoriteClassifieds { get; set; }
        public virtual ICollection<ReportUserTicket> IssuedReportUserTickets { get; set; }
        public virtual ICollection<ReportUserTicket> RelatedReportTickets { get; set; }
        public virtual ICollection<ReportClassifiedTicket> IssuedReportClassifiedTickets { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<FeedbackTicket> FeedbackTickets { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
