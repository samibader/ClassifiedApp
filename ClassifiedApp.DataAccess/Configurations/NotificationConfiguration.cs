using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class NotificationConfiguration : EntityBaseConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            Property(t => t.IsGeneral).IsRequired();
            Property(t => t.UserId).IsOptional();
            Property(t => t.Message).IsRequired().HasMaxLength(100);
            Property(t => t.Seen).IsRequired();
            Property(t => t.Type).IsRequired();
            Property(t => t.CreationDate).IsRequired();
        }
    }
}
