//using ClassifiedApp.BusinessModels;
using ClassifiedApp.DataAccess.UnitOfWork;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClassifiedApp.Resources;
using ClassifiedApp.BusinessServices.Users.ViewModels;

namespace ClassifiedApp.BusinessServices.Users.Validators
{
    public class LoginUserModelValidator : AbstractValidator<LoginUserModel>
    {

        public LoginUserModelValidator()
        {

            RuleFor(u => u.Username).NotEmpty().WithMessage(ErrorMessages.Required).Length(3, 50)
                .WithMessage(ErrorMessages.UsernameLength).Matches(@"^\w+$").WithMessage(ErrorMessages.InvalidUsername);

            RuleFor(u => u.Password).NotEmpty()
                .WithMessage(ErrorMessages.Required)
                         ;


        }

        
    }
}
