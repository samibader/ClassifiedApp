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
using ClassifiedApp.BusinessServices.Categories.ViewModels;

namespace ClassifiedApp.BusinessServices.Categories.ViewModels.Validators
{
    public class InputCategoryModelValidator : AbstractValidator<InputCategoryModel>
    {
     
        public InputCategoryModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Required.");
            When(x => x.Language!=null, () =>
            {
                RuleFor(x => x.Language).Must(IsLanguageAcceptible).WithMessage("Unacceptable Language");
            });
            //RuleFor(x => x.Language).Must(IsLanguageAcceptible).WithMessage("Unacceptable Language");
        }
        private bool IsLanguageAcceptible(string lang)
        {
            return lang.ToLower() == "en" || lang.ToLower() == "ar";

        }
    }
}
