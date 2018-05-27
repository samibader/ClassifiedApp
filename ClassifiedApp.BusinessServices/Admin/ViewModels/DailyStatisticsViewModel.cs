using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Admin.ViewModels
{
    public class DailyStatisticsViewModel
    {
        public string HourString { get; set; }
        public int Hour { get; set; }
        public int Count { get; set; }
    }
}
