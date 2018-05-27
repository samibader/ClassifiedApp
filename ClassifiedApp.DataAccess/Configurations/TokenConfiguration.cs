using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using ClassifiedApp.Models;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class TokenConfiguration : EntityBaseConfiguration<Token>
    {
        public TokenConfiguration()
        {
            Property(t => t.UserId).IsRequired();
            Property(t => t.AuthToken).IsRequired();
            Property(t => t.IssuedOn).IsRequired();
            //Property(t => t.ExpiresOn).IsRequired();
        }
    }
}
