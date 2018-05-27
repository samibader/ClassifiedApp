using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class ReportUserTicket : IEntityBase
    {
        public int Id { get; set; }
        public int ReporterUserId { get; set; }
        public int ReportedUserId { get; set; }
        public ReportUserReason ReportReason { get; set; }
        public string Note { get; set; }
        public virtual User ReporterUser { get; set; }
        public virtual User ReportedUser { get; set; }
        public DateTime IssuedDate { get; set; }
    }
    public enum ReportUserReason : byte
    {
        Fake = 0,
        InfoNotAccurate = 1,
        Other = 2
    }
}
