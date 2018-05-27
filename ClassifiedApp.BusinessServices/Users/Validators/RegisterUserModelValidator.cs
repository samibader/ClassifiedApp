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
    public class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public RegisterUserModelValidator()
        {
            
            RuleFor(u => u.Username).NotEmpty().WithMessage(ErrorMessages.Required).Length(3, 50)
                .WithMessage(ErrorMessages.UsernameLength).Matches(@"^\w+$").WithMessage(ErrorMessages.InvalidUsername);
            RuleFor(u => u.Username).Must(IsUniqueUsername).WithMessage(ErrorMessages.UsernameNotAvailable);

            RuleFor(u => u.Password).NotEmpty()
                .WithMessage(ErrorMessages.Required)
                         .Must(BeGoodPassword)
                        .WithMessage(ErrorMessages.InvalidPassword);

            RuleFor(u => u.ConfirmPassword).NotEmpty()
                .WithMessage(ErrorMessages.Required)
                .Equal(u => u.Password).WithMessage(ErrorMessages.ComparePassword);

            RuleFor(u => u.GSM).NotEmpty().WithMessage(ErrorMessages.Required).Length(1, 12).WithMessage(ErrorMessages.InvalidGSM)
                .Must(IsUniqueGsm).WithMessage(ErrorMessages.GsmNotAvailable);
                ;
            RuleFor(u => u.IsGpsEnabled).NotNull().WithMessage(ErrorMessages.Required);
            //RuleFor(u => u.City).NotEmpty().WithMessage(ErrorMessages.Required).Length(1, 50).WithMessage(String.Format(ErrorMessages.InvalidLength, 1, 50));
            RuleFor(u => u.Country).NotEmpty().WithMessage(ErrorMessages.Required).Length(1, 50).WithMessage(String.Format(ErrorMessages.InvalidLength, 1, 50));
        }

        private bool IsUniqueUsername(string username)
        {
            return  !_unitOfWork.UserRepository.FindBy(u => u.Username == username).Any();
           
        }

        private bool IsUniqueGsm(string gsm)
        {
            return !_unitOfWork.UserRepository.FindBy(u => u.GSM == gsm).Any();

        }

        public bool BeGoodPassword(string password) {
            //Regex regex = new Regex(@"^(?=[^\d_].*?\d)\w(\w|[!@#$%]){7,20}");
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$");
                return regex.IsMatch(password);
            }
    }
}
