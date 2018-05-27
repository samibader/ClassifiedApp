using ClassifiedApp.BusinessServices.Classifieds.ViewModels;
using ClassifiedApp.DataAccess.UnitOfWork;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Classifieds.Validators
{
    public class InputClassifiedModelValidator : AbstractValidator<InputClassifiedModel>
    {
        public InputClassifiedModelValidator()
        {
            RuleFor(c => c.AdType).NotNull().WithMessage("نوع الإعلان مطلوب");
            When(c => c.AdType == Models.AdTypeList.Fixed, () =>
            {
                RuleFor(c => c.AdPrice).NotNull().WithMessage("مطلوب").ExclusiveBetween(0, 99999).WithMessage("تكلفة الإعلان غير مقبولة");
            });
            When(c => c.Country.ToLower() == "syria", () =>
                {
                    RuleFor(c => c.City).NotEmpty().WithMessage("مطلوب").Length(1, 50).WithMessage("قيمة غير مقبولة");
                });
            RuleFor(c => c.Country).NotEmpty().WithMessage("مطلوب").Length(1, 50).WithMessage("قيمة غير مقبولة");
            RuleFor(c => c.Description).NotEmpty().WithMessage("مطلوب");
            RuleFor(c => c.SubCategoryId).NotNull().WithMessage("مطلوب");
            RuleFor(c => c.Title).NotEmpty().WithMessage("مطلوب").Length(1, 200).WithMessage("قيمة غير مقبولة");
            RuleFor(c => c.UserId).NotNull().WithMessage("مطلوب");
        }
    }
}
