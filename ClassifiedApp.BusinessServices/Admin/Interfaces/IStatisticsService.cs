using ClassifiedApp.BusinessServices.Admin.ViewModels;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Admin.Interfaces
{
    public interface IStatisticsService
    {
        List<YearlyStatisticsViewModel> GetYearlyUserRegistrationChartData(int year);
        List<MonthlyStatisticsViewModel> GetMonthlyUserRegistrationChartData(int year,int month);
        List<DailyStatisticsViewModel> GetDailyUserRegistrationChartData(int year, int month, int day);
        List<YearlyStatisticsViewModel> GetYearlyActiveClassifiedChartData(int year);
        List<MonthlyStatisticsViewModel> GetMonthlyActiveClassifiedChartData(int year, int month);
        List<DailyStatisticsViewModel> GetDailyActiveClassifiedChartData(int year, int month, int day);
        int GetTotalUsersCount();
        int GetTotalActiveClassifiedCount();
        int GetTotalPendingClassifiedCount();
        int GetTotalRejectedClassifiedCount();
    }
}
