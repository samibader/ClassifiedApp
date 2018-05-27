using ClassifiedApp.BusinessServices.FeedbackTickets.Interfaces;
using ClassifiedApp.DataAccess.UnitOfWork;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ClassifiedApp.BusinessServices.FeedbackTickets.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly UnitOfWork _unitOfWork;

        public FeedbackService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public int SendFeedback(int userId, ViewModels.InputFeedbackTicketModel model)
        {
            using (var scope = new TransactionScope())
            {
                var ticket = new FeedbackTicket()
                {
                    IssuedDate = BusinessSettings.ServerNow,
                    Note = model.Note,
                    UserId = model.UserId
                };

                _unitOfWork.FeedbackTicketRepository.Add(ticket);
                _unitOfWork.Save();
                scope.Complete();
                return ticket.Id;
            }
        }
    }
}
