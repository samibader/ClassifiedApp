using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class Classified : IEntityBase
    {
        public Classified()
        {
            IsFeatured = false;
            ViewCount = 0;
            Likes = new List<Like>();
            FavoriteUsers = new List<FavoriteClassified>();
            RelatedReportTickets = new List<ReportClassifiedTicket>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public AdTypeList AdType { get; set; }
        public Double? AdPrice { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AddingDate { get; set; }
        public DateTime? PostingDate { get; set; }
        public string Notes { get; set; }
        public bool IsFeatured { get; set; }
        public virtual ICollection<ClassifiedImage> ClassifiedImages { get; set; }
        public long ViewCount { get; set; }
        public AdStatusList Status { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<FavoriteClassified> FavoriteUsers { get; set; }
        public virtual ICollection<ReportClassifiedTicket> RelatedReportTickets { get; set; }
        public virtual ICollection<PropertyValue> PropertyValues { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual double AverageRate { get {
            if (!Rates.Any())
                return 0;
            else
                return (double)Rates.Sum(r => r.Value) / Rates.Count;
        } }
    }

    public enum AdTypeList : byte
    {
        Fixed = 0,
        Free = 1,
        PleaseToContact = 2
    }
    public enum AdStatusList : byte
    {
        Pending = 0,
        Active = 1,
        Expired = 2,
        Rejected = 3
    }
}
