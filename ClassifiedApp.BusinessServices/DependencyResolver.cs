using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using ClassifiedApp.Resolver;
using ClassifiedApp.BusinessServices.Users.Interfaces;
using ClassifiedApp.BusinessServices.Users.Services;
using ClassifiedApp.BusinessServices.Categories.Interfaces;
using ClassifiedApp.BusinessServices.Categories.Services;
using ClassifiedApp.BusinessServices.SubCategories.Interfaces;
using ClassifiedApp.BusinessServices.SubCategories.Services;
using ClassifiedApp.BusinessServices.Classifieds.Interfaces;
using ClassifiedApp.BusinessServices.Classifieds.Services;
using ClassifiedApp.BusinessServices.ReportTickets.Interfaces;
using ClassifiedApp.BusinessServices.ReportTickets.Services;
using ClassifiedApp.BusinessServices.Admin.Interfaces;
using ClassifiedApp.BusinessServices.Admin.Services;
using ClassifiedApp.DataAccess.UnitOfWork;
using ClassifiedApp.BusinessServices.FeedbackTickets.Interfaces;
using ClassifiedApp.BusinessServices.FeedbackTickets.Services;
using ClassifiedApp.BusinessServices.Notifications.Interfaces;
using ClassifiedApp.BusinessServices.Notifications.Services;

namespace ClassifiedApp.BusinessServices
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<ICategoryService, CategoryService>();
            registerComponent.RegisterType<ISubCategoryService, SubCategoryService>();
            registerComponent.RegisterType<ITokenService, TokenService>();
            registerComponent.RegisterType<IUserService, UserService>();
            registerComponent.RegisterType<IEncryptionService, EncryptionService>();
            registerComponent.RegisterType<IActivationCodeService, ActivationCodeService>();
            registerComponent.RegisterType<IClassifiedService, ClassifiedService>();
            registerComponent.RegisterType<IReportService, ReportService>();
            registerComponent.RegisterType<IStatisticsService, StatisticsService>();
            registerComponent.RegisterType<IFeedbackService, FeedbackService>();
            registerComponent.RegisterType<INotificationService, NotificationService>();
            DtoMappings.Map();
        }
    }
}
