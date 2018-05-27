using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using ClassifiedApp.Models;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class UserConfiguration : EntityBaseConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Username).IsRequired().HasMaxLength(50);
            Property(u => u.HashedPassword).IsRequired().HasMaxLength(200);
            Property(u => u.Salt).IsRequired().HasMaxLength(200);
            Property(u => u.GSM).IsRequired().HasMaxLength(50);
            Property(u => u.Email).HasMaxLength(100).IsOptional();
            Property(u => u.Country).HasMaxLength(50).IsOptional();
            Property(u => u.City).HasMaxLength(50).IsOptional();
            Property(u => u.IsEmailApproved).IsRequired();
            Property(u => u.IsGsmApproved).IsRequired();
            Property(u => u.IsGpsEnabled).IsRequired();
            Property(u => u.IsBlocked).IsRequired();
            Property(u => u.UserGuid).IsRequired();
            Property(u => u.CreationDate).IsRequired();

            
        }
    }
}
