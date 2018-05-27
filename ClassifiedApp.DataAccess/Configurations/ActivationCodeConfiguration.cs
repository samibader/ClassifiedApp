using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class ActivationCodeConfiguration : EntityBaseConfiguration<ActivationCode>
    {
        public ActivationCodeConfiguration()
        {
            Property(t => t.UserId).IsRequired();
            Property(t => t.ExpiresOn).IsRequired();
            Property(t => t.IssuedOn).IsRequired();
            Property(t => t.Code).IsRequired().HasMaxLength(6);
            Property(t => t.Reason).IsRequired().HasMaxLength(10);
        }
    }
}
