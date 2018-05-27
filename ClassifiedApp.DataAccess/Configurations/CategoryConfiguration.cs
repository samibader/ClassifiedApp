﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using ClassifiedApp.Models;

namespace ClassifiedApp.DataAccess.Configurations
{
    public class CategoryConfiguration : EntityBaseConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            Property(c => c.Name).IsRequired().HasMaxLength(50);
            Property(c => c.ImageUrl).IsRequired().HasMaxLength(50);
            Property(c => c.Language).IsRequired().HasMaxLength(2);
        }
    }
}
