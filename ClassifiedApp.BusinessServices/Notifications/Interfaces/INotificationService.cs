using ClassifiedApp.BusinessServices.Notifications.ViewModels;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Notifications.Interfaces
{
    public interface INotificationService
    {
        void Notify(InputNotificationModel notification, int userId);
        void Notify(InputNotificationModel notification);
        void Notify(InputNotificationModel notification, List<int> usersIds);
        void NotifyAllUsersWithApplicationUpdate(string message);
        bool RegisterDevice(InputDeviceModel model);
        void BindDeviceToUser(string udid, int userId);
        void TestNotification();
    }
}
