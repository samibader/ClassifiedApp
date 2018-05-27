using ClassifiedApp.BusinessServices.Users.Validators;
using ClassifiedApp.DataAccess.UnitOfWork;
//using ClassifiedApp.BusinessModels.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Users.ViewModels
{
    public class LoginUserModel : IValidatableObject
    {
        
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new LoginUserModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
