using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class FeedbackTicketConfiguration : EntityBaseConfiguration<FeedbackTicket>
    {
        public FeedbackTicketConfiguration()
        {
            Property(t => t.UserId).IsRequired();
            Property(t => t.IssuedDate).IsRequired();
            Property(t => t.Note).HasMaxLength(500);
        }
    }
}
