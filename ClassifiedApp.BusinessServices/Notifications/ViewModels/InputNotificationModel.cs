using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Notifications.ViewModels
{
    public class InputNotificationModel
    {
        public int UserId { get; set; }
        public NotificationType Type { get; set; }
        public string Message { get; set; }
    }
}
