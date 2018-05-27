using ClassifiedApp.BusinessServices.Classifieds.Validators;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Classifieds.ViewModels
{
    public class InputClassifiedModel : IValidatableObject
    {
        public InputClassifiedModel()
        {
            AdPrice = 0;
        }
        public int UserId { get; set; }
        public int SubCategoryId { get; set; }
        public AdTypeList AdType { get; set; }
        public Double AdPrice { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<InputPropertyValueModel> PropertyValues { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new InputClassifiedModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
