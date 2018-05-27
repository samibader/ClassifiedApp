using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class ReportUserTicketConfiguration : EntityBaseConfiguration<ReportUserTicket>
    {
        public ReportUserTicketConfiguration()
        {
            Property(t => t.ReportedUserId).IsRequired();
            Property(t => t.ReporterUserId).IsRequired();
            Property(t => t.ReportReason).IsRequired();
            Property(t => t.IssuedDate).IsRequired();
            HasRequired(m => m.ReporterUser).WithMany(m => m.IssuedReportUserTickets).HasForeignKey(m => m.ReporterUserId).WillCascadeOnDelete(false);
            HasRequired(m => m.ReportedUser).WithMany(m => m.RelatedReportTickets).HasForeignKey(m => m.ReportedUserId).WillCascadeOnDelete(false);
            Property(t => t.Note).HasMaxLength(500);

            //HasRequired(m => m.FavoriteReceiver).WithMany(m => m.FavoritesReceiver);
            //HasRequired(m => m.FavoriteSender).WithMany(m => m.FavoritesSender);
        }
    }
}
