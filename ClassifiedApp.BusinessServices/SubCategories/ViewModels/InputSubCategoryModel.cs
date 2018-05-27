using ClassifiedApp.BusinessServices.SubCategories.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.SubCategories.ViewModels
{
    public class InputSubCategoryModel : IValidatableObject
    {
        public InputSubCategoryModel()
        {
            ImageUrl = "placeholder.jpg";
        }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new InputSubCategoryModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
