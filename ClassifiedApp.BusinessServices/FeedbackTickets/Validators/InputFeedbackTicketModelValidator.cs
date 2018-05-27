using ClassifiedApp.BusinessServices.FeedbackTickets.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.FeedbackTickets.Validators
{
    public class InputFeedbackTicketModelValidator : AbstractValidator<InputFeedbackTicketModel>
    {
        public InputFeedbackTicketModelValidator()
        {
            RuleFor(c => c.Note).NotEmpty().WithMessage("مطلوب");
            RuleFor(t => t.UserId).NotNull().GreaterThan(0).WithMessage("مطلوب");
        }
    }
}
