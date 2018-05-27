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
    public class EditProfileModelValidator : AbstractValidator<EditProfileModel>
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public EditProfileModelValidator()
        {
            //RuleFor(u => u.City).NotEmpty().WithMessage(ErrorMessages.Required).Length(1, 50).WithMessage(String.Format(ErrorMessages.InvalidLength, 1, 50));
            //RuleFor(u => u.Country).NotEmpty().WithMessage(ErrorMessages.Required).Length(1, 50).WithMessage(String.Format(ErrorMessages.InvalidLength, 1, 50));
            When(c => c.Country != null, () =>
            {
                When(c => c.Country.ToLower() == "syria", () =>
                {
                    RuleFor(c => c.City).NotEmpty().WithMessage("مطلوب").Length(1, 50).WithMessage("قيمة غير مقبولة");
                });
                RuleFor(c => c.Country).NotEmpty().WithMessage("مطلوب").Length(1, 50).WithMessage("قيمة غير مقبولة");
            });
            When(c => c.NewEmail != null, () =>
            {
                RuleFor(c => c.NewEmail).NotEmpty().WithMessage("مطلوب").EmailAddress().WithMessage("قيمة غير مقبولة");
            });
            When(c => c.NewGSM != null, () =>
            {
                RuleFor(u => u.NewGSM).Length(1, 12).WithMessage(ErrorMessages.InvalidGSM)
                 .Must(IsUniqueGsm).WithMessage(ErrorMessages.GsmNotAvailable);
            });
        }
        private bool IsUniqueGsm(string gsm)
        {
            return !_unitOfWork.UserRepository.FindBy(u => u.GSM == gsm).Any();

        }
    }
}
