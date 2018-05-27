using ClassifiedApp.BusinessServices.Users.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Users.ViewModels
{
    public class EditProfileModel : IValidatableObject
    {
        public string NewEmail { get; set; }
        public string NewGSM { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new EditProfileModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
