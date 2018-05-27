using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class AdminConfiguration : EntityBaseConfiguration<Admin>
    {
        public AdminConfiguration()
        {
            Property(u => u.Username).IsRequired().HasMaxLength(50);
            Property(u => u.HashedPassword).IsRequired().HasMaxLength(200);
            Property(u => u.Salt).IsRequired().HasMaxLength(200);
            Property(u => u.Email).HasMaxLength(100).IsOptional();
        }
    }
}
