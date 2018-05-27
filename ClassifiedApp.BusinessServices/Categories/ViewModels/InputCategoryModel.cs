using ClassifiedApp.BusinessServices.Categories.ViewModels.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Categories.ViewModels
{
    public class InputCategoryModel : IValidatableObject
    {
        public InputCategoryModel()
        {
            ImageUrl = "placeholder.jpg";
        }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Language { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new InputCategoryModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
