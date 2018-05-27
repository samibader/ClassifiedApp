using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Notifications.ViewModels
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public NotificationType Type { get; set; }
        public string TypeDesc { get; set; }
        public string Message { get; set; }
        public bool IsGeneral { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Seen { get; set; }
    }
}
