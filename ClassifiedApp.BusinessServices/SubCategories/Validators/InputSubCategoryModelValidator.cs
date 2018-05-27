using ClassifiedApp.BusinessServices.SubCategories.ViewModels;
using ClassifiedApp.DataAccess.UnitOfWork;
using ClassifiedApp.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.SubCategories.Validators
{
    public class InputSubCategoryModelValidator : AbstractValidator<InputSubCategoryModel>
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public InputSubCategoryModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ErrorMessages.Required);
            RuleFor(x => x.CategoryId).NotNull().WithMessage(ErrorMessages.Required)
                .Must(CategoryIdIsValid).WithMessage(ErrorMessages.InvalidCategory);
        }

        private bool CategoryIdIsValid(int categoryId)
        {
            return _unitOfWork.CategoryRepository.FindBy(x => x.Id == categoryId).Any();

        }
    }
}
