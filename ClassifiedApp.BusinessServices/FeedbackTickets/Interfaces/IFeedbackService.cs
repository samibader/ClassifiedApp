using ClassifiedApp.BusinessServices.FeedbackTickets.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.FeedbackTickets.Interfaces
{
    public interface IFeedbackService
    {
        int SendFeedback(int userId, InputFeedbackTicketModel model);
    }
}
