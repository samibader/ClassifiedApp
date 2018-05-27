using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Notifications.ViewModels
{
    public class InputDeviceModel
    {
        public string UDID { get; set; }
        public string Lang { get; set; }
        public string RegistrationId { get; set; }
        public bool IsAndroid { get; set; }
        public InputDeviceModel()
        {
            
        }
        
    }
}
