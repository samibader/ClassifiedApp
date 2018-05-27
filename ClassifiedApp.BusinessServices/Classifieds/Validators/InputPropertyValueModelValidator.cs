using ClassifiedApp.BusinessServices.Classifieds.ViewModels;
using ClassifiedApp.DataAccess.UnitOfWork;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Classifieds.Validators
{
    public class InputPropertyValueModelValidator : AbstractValidator<InputPropertyValueModel>
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public InputPropertyValueModelValidator()
        {
            RuleFor(p => p.CategoryId).NotNull().WithMessage("مطلوب");
            RuleFor(p=>p.PropertyDefinitionId).NotNull().WithMessage("مطلوب")
                .Must(ValidPropertyId).WithMessage("رقم الواصفة غير موجود ضمن الفئة المختارة");
            RuleFor(p => p.Value).NotNull().WithMessage("مطلوب")
                .Must(ValidValue).WithMessage("القيمة غير متوافقة مع نوع الواصفة");
        }

        private bool ValidValue(InputPropertyValueModel instance, string value)
        {
            var prop = _unitOfWork.PropertyDefinitionRepository.FindSingleBy(p => p.Id == instance.PropertyDefinitionId);
            if(prop!=null)
            {
                bool isOk=true;
                switch (prop.Type)
                {
                    case ClassifiedApp.Models.PropertyTypes.StringType:
                        break;
                    case ClassifiedApp.Models.PropertyTypes.IntegerType:
                        int n;
                        isOk = int.TryParse(value, out n);
                        break;
                    case ClassifiedApp.Models.PropertyTypes.DecimalType:
                        decimal d;
                        isOk = decimal.TryParse(value, out d);
                        break;
                    case ClassifiedApp.Models.PropertyTypes.DateTimeType:
                        DateTime dt;
                        isOk = DateTime.TryParseExact(value, "d-m-yyyy", CultureInfo.InvariantCulture,
        DateTimeStyles.None, out dt);
                        break;
                    default:
                        isOk = false;
                        break;
                }
                return isOk;
            }
            return false;
        }

        private bool ValidPropertyId(InputPropertyValueModel instance, int propId)
        {
            return _unitOfWork.PropertyDefinitionRepository.FindSingleBy(p => p.CategoryId == instance.CategoryId && p.Id == propId) != null;
        }
    }
}
