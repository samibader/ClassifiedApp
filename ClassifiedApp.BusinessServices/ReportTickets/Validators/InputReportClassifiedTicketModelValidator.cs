using ClassifiedApp.BusinessServices.ReportTickets.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.ReportTickets.Validators
{
    public class InputReportClassifiedTicketModelValidator : AbstractValidator<InputReportClassifiedTicketModel>
    {
        public InputReportClassifiedTicketModelValidator()
        {
            RuleFor(t => t.ReportedClassifiedId).NotNull().GreaterThan(0).WithMessage("مطلوب");
            When(t => t.ReportReason == Models.ReportClassifiedReason.Other, () =>
            {
                RuleFor(c => c.Note).NotEmpty().WithMessage("مطلوب");
            });
        }
    }
}
