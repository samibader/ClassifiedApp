using ClassifiedApp.BusinessServices.ReportTickets.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.ReportTickets.Interfaces
{
    public interface IReportService
    {
        int ReportUser(int userId,InputReportUserTicketModel model);
        int ReportClassified(int userId, InputReportClassifiedTicketModel model);
    }
}
