using System.Web.Mvc;
using System.Web.Optimization;

namespace ClassifiedApp.WebApi.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        //public override void RegisterArea(AreaRegistrationContext context)
        //{
        //    context.MapRoute(
        //        "Admin_default",
        //        "Admin/{controller}/{action}/{id}",
        //        new { controller = "Dashboards", action = "Dashboard_1", id = UrlParameter.Optional }
        //    );


        //}
        public override void RegisterArea(AreaRegistrationContext context)
        {
            RegisterRoutes(context);
            RegisterBundles();
        }

        private void RegisterRoutes(AreaRegistrationContext context)
        {
            ClassifiedApp.WebApi.Areas.Admin.App_Start.RouteConfig.RegisterRoutes(context);
        }

        private void RegisterBundles()
        {
            ClassifiedApp.WebApi.Areas.Admin.App_Start.BundleConfig.RegisterBundles(BundleTable.Bundles);
        } 
    }
}