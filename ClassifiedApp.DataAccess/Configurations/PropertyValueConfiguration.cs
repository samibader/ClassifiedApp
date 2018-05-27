using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using ClassifiedApp.Models;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class PropertyValueConfiguration : EntityBaseConfiguration<PropertyValue>
    {
        public PropertyValueConfiguration()
        {
            Property(u => u.Value).IsRequired().HasMaxLength(200);
            Property(u => u.PropertyDefinitionId).IsRequired();
            Property(u => u.ClassifiedId).IsRequired();
            HasRequired(c => c.Classified).
                WithMany(u => u.PropertyValues).
                HasForeignKey(m => m.ClassifiedId).
                WillCascadeOnDelete(false);
            HasRequired(c => c.PropertyDefinition).
                WithMany(u => u.PropertyValues).
                HasForeignKey(m => m.PropertyDefinitionId).
                WillCascadeOnDelete(false);
        }
    }
}
