using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class ClassifiedConfiguration : EntityBaseConfiguration<Classified>
    {
        public ClassifiedConfiguration()
        {
            Property(c => c.AdType).IsRequired();
            Property(c => c.Country).IsRequired().HasMaxLength(50);
            Property(c => c.City).IsOptional().HasMaxLength(50);
            Property(c => c.Description).IsUnicode(true).IsRequired().IsVariableLength();
            //Property(c => c.ImageUrl).HasMaxLength(200);
            Property(c => c.Notes).IsOptional().HasMaxLength(500);
            Property(c => c.SubCategoryId).IsRequired();
            Property(c => c.Title).IsRequired().HasMaxLength(200);
            Property(c => c.UserId).IsRequired();
            Property(c => c.PostingDate).IsOptional();
            Property(c => c.AdPrice).IsOptional();
            HasRequired(c => c.User).
                WithMany(u => u.Classifieds).
                HasForeignKey(m => m.UserId).
                WillCascadeOnDelete(false);
            Ignore(c => c.AverageRate);
        }
    }
}
