using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class ReportClassifiedTicketConfiguration : EntityBaseConfiguration<ReportClassifiedTicket>
    {
        public ReportClassifiedTicketConfiguration()
        {
            Property(t => t.ClassifiedId).IsRequired();
            Property(t => t.ReporterUserId).IsRequired();
            Property(t => t.ReportReason).IsRequired();
            Property(t => t.IssuedDate).IsRequired();
            HasRequired(m => m.ReporterUser).WithMany(m => m.IssuedReportClassifiedTickets).HasForeignKey(m => m.ReporterUserId).WillCascadeOnDelete(false);
            HasRequired(m => m.ReportedClassified).WithMany(m => m.RelatedReportTickets).HasForeignKey(m => m.ClassifiedId).WillCascadeOnDelete(false);

            Property(t => t.Note).HasMaxLength(500);
            //HasRequired(m => m.FavoriteReceiver).WithMany(m => m.FavoritesReceiver);
            //HasRequired(m => m.FavoriteSender).WithMany(m => m.FavoritesSender);
        }
    }
}
