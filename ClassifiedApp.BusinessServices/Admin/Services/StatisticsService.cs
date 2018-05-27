using ClassifiedApp.BusinessServices.Admin.Interfaces;
using ClassifiedApp.BusinessServices.Admin.ViewModels;
using ClassifiedApp.DataAccess.UnitOfWork;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Admin.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly UnitOfWork _unitOfWork;
        public StatisticsService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<YearlyStatisticsViewModel> GetYearlyUserRegistrationChartData(int year)
        {
            List<YearlyStatisticsViewModel> list = new List<YearlyStatisticsViewModel>();
            
            for (int i = 1; i <= 12; i++)
            {
                YearlyStatisticsViewModel model = new YearlyStatisticsViewModel();
                model.Month = i;
                model.MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                model.Year = year;

                var createdUser = _unitOfWork.UserRepository.FindBy(u => u.CreationDate.Month == i && u.CreationDate.Year == year);

                model.Count = createdUser.Count();
                list.Add(model);
            }
            return list;
        }

        public List<MonthlyStatisticsViewModel> GetMonthlyUserRegistrationChartData(int year, int month)
        {
            List<MonthlyStatisticsViewModel> list = new List<MonthlyStatisticsViewModel>();
            
            for (int i = 1; i <= DateTime.DaysInMonth(year,month); i++)
            {
                MonthlyStatisticsViewModel model = new MonthlyStatisticsViewModel();
                model.Month = month;
                model.Day = i;
                model.Year = year;

                var createdUser = _unitOfWork.UserRepository.FindBy(u => u.CreationDate.Day==i && u.CreationDate.Month == month && u.CreationDate.Year == year);

                model.Count = createdUser.Count();
                list.Add(model);
            }
            return list;
        }


        public List<DailyStatisticsViewModel> GetDailyUserRegistrationChartData(int year, int month, int day)
        {
            List<DailyStatisticsViewModel> list = new List<DailyStatisticsViewModel>();

            for (int i = 0; i <= 23; i++)
            {
                DailyStatisticsViewModel model = new DailyStatisticsViewModel();
                model.Hour = i;
                if (i <= 9)
                    model.HourString = String.Format("0{0}:00", i);
                else
                    model.HourString = String.Format("{0}:00", i);
                var createdUser = _unitOfWork.UserRepository.FindBy(u => u.CreationDate.Day == day && u.CreationDate.Month == month && u.CreationDate.Year == year && u.CreationDate.Hour == i);

                model.Count = createdUser.Count();
                list.Add(model);
            }
            return list;
        }


        public int GetTotalUsersCount()
        {
            return _unitOfWork.UserRepository.GetAll().Count();
        }


        public int GetTotalActiveClassifiedCount()
        {
            return _unitOfWork.ClassifiedRepository.FindBy(c => c.Status == Models.AdStatusList.Active).Count();
        }

        public int GetTotalPendingClassifiedCount()
        {
            return _unitOfWork.ClassifiedRepository.FindBy(c => c.Status == Models.AdStatusList.Pending).Count();
        }

        public int GetTotalRejectedClassifiedCount()
        {
            return _unitOfWork.ClassifiedRepository.FindBy(c => c.Status == Models.AdStatusList.Rejected).Count();
        }


        public List<YearlyStatisticsViewModel> GetYearlyActiveClassifiedChartData(int year)
        {
            List<YearlyStatisticsViewModel> list = new List<YearlyStatisticsViewModel>();

            for (int i = 1; i <= 12; i++)
            {
                YearlyStatisticsViewModel model = new YearlyStatisticsViewModel();
                model.Month = i;
                model.MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                model.Year = year;

                var activeClassifieds = _unitOfWork.ClassifiedRepository.FindBy(u => u.Status== AdStatusList.Active && u.PostingDate.Value.Month == i && u.PostingDate.Value.Year == year);
                
                model.Count = activeClassifieds.Count();
                list.Add(model);
            }
            return list;
        }

        public List<MonthlyStatisticsViewModel> GetMonthlyActiveClassifiedChartData(int year, int month)
        {
            List<MonthlyStatisticsViewModel> list = new List<MonthlyStatisticsViewModel>();

            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                MonthlyStatisticsViewModel model = new MonthlyStatisticsViewModel();
                model.Month = month;
                model.Day = i;
                model.Year = year;

                var activeClassifieds = _unitOfWork.ClassifiedRepository.FindBy(u => u.Status == AdStatusList.Active && u.PostingDate.Value.Day == i && u.PostingDate.Value.Month == month && u.PostingDate.Value.Year == year);

                model.Count = activeClassifieds.Count();
                list.Add(model);
            }
            return list;
        }

        public List<DailyStatisticsViewModel> GetDailyActiveClassifiedChartData(int year, int month, int day)
        {
            List<DailyStatisticsViewModel> list = new List<DailyStatisticsViewModel>();

            for (int i = 0; i <= 23; i++)
            {
                DailyStatisticsViewModel model = new DailyStatisticsViewModel();
                model.Hour = i;
                if (i <= 9)
                    model.HourString = String.Format("0{0}:00", i);
                else
                    model.HourString = String.Format("{0}:00", i);
                var activeClassifieds = _unitOfWork.ClassifiedRepository.FindBy(u => u.Status == AdStatusList.Active && u.PostingDate.Value.Day == day && u.PostingDate.Value.Month == month && u.PostingDate.Value.Year == year && u.PostingDate.Value.Hour == i);

                model.Count = activeClassifieds.Count();
                list.Add(model);
            }
            return list;
        }
    }
}
