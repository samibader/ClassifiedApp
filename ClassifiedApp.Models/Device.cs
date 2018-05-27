using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class Device:IEntityBase
    {
        public int Id { get; set; }
        public string UDID { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsAndroid { get; set; }
        public string Lang { get; set; }
        public string RegistrationId { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
