using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using ClassifiedApp.Models;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class SubPropertyDefinitionConfiguration : EntityBaseConfiguration<SubPropertyDefinition>
    {
        public SubPropertyDefinitionConfiguration()
        {
            Property(u => u.Name).IsRequired().HasMaxLength(50);
            Property(u => u.Type).IsRequired();
        }
    }
}
