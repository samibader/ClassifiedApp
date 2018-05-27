using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessModels.Validators
{
    public class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
    {
        public RegisterUserModelValidator()
        {
            RuleFor(u => u.Username).NotEmpty().WithMessage("Username required.").Length(3, 50)
                .WithMessage("Username must be between 3-50 characters.").Matches(@"^\w+$").WithMessage("Username can only contains alphanumeric characters and underscore.");
            RuleFor(u => u.Username).Must(IsUniqueUsername).WithMessage("username already exists");

            RuleFor(u => u.Password).NotEmpty()
                .WithMessage("Password required.")
                         .Must(BeGoodPassword)
                        .WithMessage("Password must be at least 8 characters long, and contain numbers and letters.");

            RuleFor(u => u.ConfirmPassword).NotEmpty()
                .WithMessage("Confirm Password required.")
                .Equal(u => u.Password).WithMessage("Password and Confirm don't match.");

            RuleFor(u => u.GSM).NotEmpty().WithMessage("GSM required.").Length(12).WithMessage("Invalid mobile number");
            RuleFor(u => u.IsGpsEnabled).NotEmpty().WithMessage("GPS status required.");
            RuleFor(u => u.City).NotEmpty().WithMessage("City required.").Length(1, 50).WithMessage("City must be between 1-50 characters");
            RuleFor(u => u.Country).NotEmpty().WithMessage("Country required.").Length(1, 50).WithMessage("Country must be between 1-50 characters");
        }

        private bool IsUniqueUsername(string username)
        {
            return true;
            
        }

        public bool BeGoodPassword(string password) {
            Regex regex = new Regex(@"^(?=[^\d_].*?\d)\w(\w|[!@#$%]){7,20}");
                return regex.IsMatch(password);
            }
    }
}
