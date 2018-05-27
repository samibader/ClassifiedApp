using ClassifiedApp.BusinessServices.ReportTickets.Validators;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.ReportTickets.ViewModels
{
    public class InputReportClassifiedTicketModel : IValidatableObject
    {
        public InputReportClassifiedTicketModel()
        {

        }
        public int ReportedClassifiedId { get; set; }
        public ReportClassifiedReason ReportReason { get; set; }
        public string Note { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new InputReportClassifiedTicketModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
