using ClassifiedApp.DataAccess.Configurations;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess
{
    public class ClassifiedAppDbContext : DbContext
    {
        public ClassifiedAppDbContext()
            : base("ClassifiedAppDB")
        {
            //Database.SetInitializer<ClassifiedAppDbContext>(null);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<ActivationCode> ActivationCodes { get; set; }
        public DbSet<PropertyDefinition> PropertyDefinitions { get; set; }
        public DbSet<SubPropertyDefinition> SubPropertyDefinitions { get; set; }
        public DbSet<Classified> Classifieds { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<ClassifiedImage> ClassifiedImages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<FeedbackTicket> FeedbackTickets { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }
        public DbSet<Testing> Testings { get; set; }
        //public DbSet<FavoriteUser> FavoritesSender { get; set; }
        //public DbSet<FavoriteUser> FavoritesReceiver { get; set; }
        public virtual void Save()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new TokenConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new SubCategoryConfiguration());
            modelBuilder.Configurations.Add(new ActivationCodeConfiguration());
            modelBuilder.Configurations.Add(new PropertyDefinitionConfiguration());
            modelBuilder.Configurations.Add(new SubPropertyDefinitionConfiguration());
            modelBuilder.Configurations.Add(new ClassifiedConfiguration());
            modelBuilder.Configurations.Add(new LikeConfiguration());
            modelBuilder.Configurations.Add(new FavoriteUserConfiguration());
            modelBuilder.Configurations.Add(new ClassifiedImageConfiguration());
            modelBuilder.Configurations.Add(new AdminConfiguration());
            modelBuilder.Configurations.Add(new FavoriteClassifiedConfiguration());
            modelBuilder.Configurations.Add(new ReportClassifiedTicketConfiguration());
            modelBuilder.Configurations.Add(new ReportUserTicketConfiguration());
            modelBuilder.Configurations.Add(new NotificationConfiguration());
            modelBuilder.Configurations.Add(new FeedbackTicketConfiguration());
            modelBuilder.Configurations.Add(new DeviceConfiguration());
            modelBuilder.Configurations.Add(new PropertyValueConfiguration());
        }
    }
}
