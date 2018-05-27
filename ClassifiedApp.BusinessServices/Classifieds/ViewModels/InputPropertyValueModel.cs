using ClassifiedApp.BusinessServices.Classifieds.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Classifieds.ViewModels
{
    public class InputPropertyValueModel : IValidatableObject
    {
        public InputPropertyValueModel()
        {

        }
        public int CategoryId { get; set; }
        public int PropertyDefinitionId { get; set; }
        public string Value { get; set; } 
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new InputPropertyValueModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
