using ClassifiedApp.BusinessServices.Admin.Interfaces;
using ClassifiedApp.BusinessServices.Users.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassifiedApp.WebApi.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        public DashboardController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        // GET: Admin/Dashboards
        public ActionResult Index()
        {
            List<string> Years = new List<string>();
            for (int i = 2015; i <= DateTime.Now.Year+1; i++)
            {
                Years.Add(i.ToString());
            }
            SelectList YearsList = new SelectList(Years);
            ViewData["YearsList"] = YearsList;

            // Get Totals
            ViewBag.TotalUsers = _statisticsService.GetTotalUsersCount();
            ViewBag.TotalActive = _statisticsService.GetTotalActiveClassifiedCount();
            ViewBag.TotalPending = _statisticsService.GetTotalPendingClassifiedCount();
            ViewBag.TotalRejected = _statisticsService.GetTotalRejectedClassifiedCount();
            return View();
        }
        public ContentResult GetYearlyUserRegistrationChartData(int year)
        {
            //System.Threading.Thread.Sleep(3000);
               
            var users = _statisticsService.GetYearlyUserRegistrationChartData(year);
            return Content(JsonConvert.SerializeObject(users), "application/json");
        }
        public ContentResult GetMonthlyUserRegistrationChartData(int month,int year)
        {
            //System.Threading.Thread.Sleep(3000);
            var users = _statisticsService.GetMonthlyUserRegistrationChartData(year, month);
            return Content(JsonConvert.SerializeObject(users), "application/json");
        }
        public ContentResult GetDailyUserRegistrationChartData(int day,int month, int year)
        {
            //System.Threading.Thread.Sleep(3000);
            var users = _statisticsService.GetDailyUserRegistrationChartData(year, month, day);
            return Content(JsonConvert.SerializeObject(users), "application/json");
        }
        public ContentResult GetYearlyActiveClassifiedChartData(int year)
        {
            //System.Threading.Thread.Sleep(3000);

            var users = _statisticsService.GetYearlyActiveClassifiedChartData(year);
            return Content(JsonConvert.SerializeObject(users), "application/json");
        }
        public ContentResult GetMonthlyActiveClassifiedChartData(int month, int year)
        {
            //System.Threading.Thread.Sleep(3000);
            var users = _statisticsService.GetMonthlyActiveClassifiedChartData(year, month);
            return Content(JsonConvert.SerializeObject(users), "application/json");
        }
        public ContentResult GetDailyActiveClassifiedChartData(int day, int month, int year)
        {
            //System.Threading.Thread.Sleep(3000);
            var users = _statisticsService.GetDailyActiveClassifiedChartData(year, month, day);
            return Content(JsonConvert.SerializeObject(users), "application/json");
        }
        public ActionResult Dashboard_1()
        {
            return View();
        }
    }
}