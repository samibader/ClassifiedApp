using ClassifiedApp.BusinessServices.ReportTickets.Interfaces;
using ClassifiedApp.BusinessServices.ReportTickets.ViewModels;
using ClassifiedApp.DataAccess.UnitOfWork;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ClassifiedApp.BusinessServices.ReportTickets.Services
{
    public class ReportService : IReportService
    {
        private readonly UnitOfWork _unitOfWork;

        public ReportService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int ReportUser(int userId,InputReportUserTicketModel model)
        {
            using (var scope = new TransactionScope())
            {
                var ticket = new ReportUserTicket()
                {
                    IssuedDate = BusinessSettings.ServerNow,
                    Note=model.Note,
                    ReportedUserId=model.ReportedUserId,
                    ReporterUserId=userId,
                    ReportReason=model.ReportReason
                };
                
                _unitOfWork.ReportUserTicketRepository.Add(ticket);
                _unitOfWork.Save();
                scope.Complete();
                return ticket.Id;
            }
        }


        public int ReportClassified(int userId, InputReportClassifiedTicketModel model)
        {
            using (var scope = new TransactionScope())
            {
                var ticket = new ReportClassifiedTicket()
                {
                    IssuedDate = BusinessSettings.ServerNow,
                    Note = model.Note,
                    ClassifiedId  = model.ReportedClassifiedId,
                    ReporterUserId = userId,
                    ReportReason = model.ReportReason
                };

                _unitOfWork.ReportClassifiedTicketRepository.Add(ticket);
                _unitOfWork.Save();
                scope.Complete();
                return ticket.Id;
            }
        }
    }
}
