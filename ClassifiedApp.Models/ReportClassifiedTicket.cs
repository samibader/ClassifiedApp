using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class ReportClassifiedTicket : IEntityBase
    {
        public int Id { get; set; }
        public int ReporterUserId { get; set; }
        public int ClassifiedId { get; set; }
        public ReportClassifiedReason ReportReason { get; set; }
        public string Note { get; set; }
        public virtual User ReporterUser { get; set; }
        public virtual Classified ReportedClassified { get; set; }
        public DateTime IssuedDate { get; set; }
    }
    public enum ReportClassifiedReason : byte
    {
        Fake = 0,
        InfoNotAccurate = 1,
        Other = 2,
        Expired = 3
    }
}
