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
    public class EditClassifiedModelValidator : AbstractValidator<EditClassifiedModel>
    {
        public EditClassifiedModelValidator()
        {

            When(c => c.AdType != null, () =>
                {
                    RuleFor(c => c.AdType).NotNull().WithMessage("نوع الإعلان مطلوب");
                    When(c => c.AdType == Models.AdTypeList.Fixed, () =>
                    {
                        RuleFor(c => c.AdPrice).NotNull().WithMessage("مطلوب").ExclusiveBetween(0, 99999).WithMessage("تكلفة الإعلان غير مقبولة");
                    });
                });
            When(c => c.Country != null, () =>
                {
                    When(c => c.Country.ToLower() == "syria", () =>
                        {
                            RuleFor(c => c.City).NotEmpty().WithMessage("مطلوب").Length(1, 50).WithMessage("قيمة غير مقبولة");
                        });
                    RuleFor(c => c.Country).NotEmpty().WithMessage("مطلوب").Length(1, 50).WithMessage("قيمة غير مقبولة");
                });

            When(c => c.Description != null, () =>
                {
                    RuleFor(c => c.Description).NotEmpty().WithMessage("مطلوب");
                });
            When(c => c.Title != null, () =>
            {
                RuleFor(c => c.Title).NotEmpty().WithMessage("مطلوب").Length(1, 200).WithMessage("قيمة غير مقبولة");
            });
            When(c => c.SubCategoryId != null, () =>
            {
                RuleFor(c => c.SubCategoryId).NotEmpty().WithMessage("مطلوب");
            });
            RuleFor(c => c.ClassifiedId).NotNull().WithMessage("مطلوب");
        }
    }
}
