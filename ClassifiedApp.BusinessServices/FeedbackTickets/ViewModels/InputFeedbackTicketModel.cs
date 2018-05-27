using ClassifiedApp.BusinessServices.FeedbackTickets.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.FeedbackTickets.ViewModels
{
    public class InputFeedbackTicketModel : IValidatableObject
    {
        public InputFeedbackTicketModel()
        {

        }
        public int UserId { get; set; }
        public string Note { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new InputFeedbackTicketModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
