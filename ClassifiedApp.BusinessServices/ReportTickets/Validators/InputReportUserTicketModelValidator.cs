using ClassifiedApp.BusinessServices.ReportTickets.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.ReportTickets.Validators
{
    public class InputReportUserTicketModelValidator : AbstractValidator<InputReportUserTicketModel>
    {
        public InputReportUserTicketModelValidator()
        {
            RuleFor(t => t.ReportedUserId).NotNull().GreaterThan(0).WithMessage("مطلوب");
            When(t => t.ReportReason == Models.ReportUserReason.Other, () =>
            {
                RuleFor(c => c.Note).NotEmpty().WithMessage("مطلوب");
            });
        }
    }
}
