using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class DeviceConfiguration : EntityBaseConfiguration<Device>
    {
        public DeviceConfiguration()
        {
            Property(t => t.IsAndroid).IsRequired();
            Property(t => t.UserId).IsOptional();
            Property(t => t.IsEnabled).IsRequired();
            Property(t => t.Lang).IsRequired().HasMaxLength(2);
            Property(t => t.RegistrationId).IsRequired().HasMaxLength(200);
            Property(t => t.UDID).IsRequired().HasMaxLength(200);
        }
    }
}
