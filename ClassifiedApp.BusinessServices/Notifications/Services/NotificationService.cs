using ClassifiedApp.BusinessServices.Notifications.Interfaces;
using ClassifiedApp.DataAccess.UnitOfWork;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using PushSharp;
using PushSharp.Google;
using Newtonsoft.Json.Linq;

namespace ClassifiedApp.BusinessServices.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly UnitOfWork _unitOfWork;

        public NotificationService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void NotifySingleUser(string message, User user)
        {

        }
        public void NotifyMultipleUsers(string message, List<User> users)
        {

        }
        public void NotifyAllUsersWithApplicationUpdate(string message)
        {
            var notification = new Notification()
            {
                CreationDate = BusinessSettings.ServerNow,
                IsGeneral = true,
                Message = message,
                Seen = false,
                Type = NotificationType.ApplicationNewUpdate
            };

            using (var scope = new TransactionScope())
            {
                _unitOfWork.NotificationRepository.Add(notification);
                _unitOfWork.Save();
                scope.Complete();
            }
        }


        public bool RegisterDevice(ViewModels.InputDeviceModel model)
        {
            //using (var scope = new TransactionScope())
            //{
            var dev = _unitOfWork.DeviceRepository.FindSingleBy(m => m.UDID == model.UDID);
            if (dev != null)
                return false;
                var device = new Device()
                {
                    IsAndroid = model.IsAndroid,
                    IsEnabled = true,
                    Lang = model.Lang,
                    RegistrationId = model.RegistrationId,
                    UDID = model.UDID
                };
                _unitOfWork.DeviceRepository.Add(device);
                _unitOfWork.Save();
                //scope.Complete();
                sendPushToAndroid(device.RegistrationId, "testing");
                return true;
            //}
        }


        public void BindDeviceToUser(string udid, int userId)
        {
            var device = _unitOfWork.DeviceRepository.FindSingleBy(d => d.UDID == udid);
            if (device != null)
            {
                device.UserId = userId;
                _unitOfWork.DeviceRepository.Edit(device);
                _unitOfWork.Save();
            }
        }


        public void TestNotification()
        {
            throw new NotImplementedException();
        }

        private void SendPush(int _LangID, string _Message)
        {
            List<Device> devices = _unitOfWork.DeviceRepository.All.ToList();
            foreach (var d in devices)
            {
                if (d.IsAndroid)
                    sendPushToAndroid(d.UDID, _Message);
                //else
                //    sendPushToiOS(n.UDID, _Message);
            }
        }
        private void sendPushToAndroid(string deviceID, string message)
        {
            var config = new GcmConfiguration("1034425131442", "AIzaSyDlrjgMMYo5Om11e-F5vxZFfif37v0iLlo", null);
            var broker = new GcmServiceBroker(config);
            broker.OnNotificationFailed += new PushSharp.Core.NotificationFailureDelegate<GcmNotification>(_pushBroker_OnNotificationFailed);
            //broker.OnNotificationSucceeded += (notification) =>
            //{
            //    succeeded++;
            //};

            broker.Start();

            foreach (var device in _unitOfWork.DeviceRepository.All.ToList())
            {

                broker.QueueNotification(new GcmNotification
                {
                    RegistrationIds = new List<string> { 
                        device.RegistrationId
                    },
                    Data = JObject.Parse("{ \"message\" : \"" + message + "\" }")
                });
            }

            broker.Stop();


        }

        private void _pushBroker_OnNotificationFailed(GcmNotification notification, Exception exception)
        {
            throw exception;
        }

        public void Notify(ViewModels.InputNotificationModel notification, int userId)
        {
            var notify = new Notification()
            {
                CreationDate = BusinessSettings.ServerNow,
                IsGeneral = false,
                Message = notification.Message,
                Seen = false,
                Type = notification.Type,
                UserId = userId
            };
            using (var scope = new TransactionScope())
            {
                _unitOfWork.NotificationRepository.Add(notify);
                _unitOfWork.Save();
                scope.Complete();
            }
        }

        public void Notify(ViewModels.InputNotificationModel notification)
        {
            var notify = new Notification()
            {
                CreationDate = BusinessSettings.ServerNow,
                IsGeneral = true,
                Message = notification.Message,
                Seen = false,
                Type = notification.Type
            };
            using (var scope = new TransactionScope())
            {
                _unitOfWork.NotificationRepository.Add(notify);
                _unitOfWork.Save();
                scope.Complete();
            }
        }

        public void Notify(ViewModels.InputNotificationModel notification, List<int> usersIds)
        {
            foreach (int userId in usersIds)
            {
                var notify = new Notification()
                {
                    CreationDate = BusinessSettings.ServerNow,
                    IsGeneral = false,
                    Message = notification.Message,
                    Seen = false,
                    Type = notification.Type,
                    UserId=userId
                };
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.NotificationRepository.Add(notify);
                    _unitOfWork.Save();
                    scope.Complete();
                }
            }
        }
    }
}
