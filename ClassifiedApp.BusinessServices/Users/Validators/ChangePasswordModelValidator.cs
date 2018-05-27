using ClassifiedApp.BusinessServices.Users.ViewModels;
using ClassifiedApp.DataAccess.UnitOfWork;
using ClassifiedApp.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Users.Validators
{
    public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
    {
        private IEncryptionService _encryptionService = new EncryptionService();
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public ChangePasswordModelValidator()
        {
            RuleFor(u => u.NewPassword).NotEmpty()
                .WithMessage(ErrorMessages.Required)
                         .Must(BeGoodPassword)
                        .WithMessage(ErrorMessages.InvalidPassword);

            RuleFor(u => u.NewPasswordConfirm).NotEmpty()
                .WithMessage(ErrorMessages.Required)
                .Equal(u => u.NewPassword).WithMessage(ErrorMessages.ComparePassword);
        }

        public bool BeGoodPassword(string password)
        {
            
            //Regex regex = new Regex(@"^(?=[^\d_].*?\d)\w(\w|[!@#$%]){7,20}");
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$");
            return regex.IsMatch(password);
        }
    }
}
