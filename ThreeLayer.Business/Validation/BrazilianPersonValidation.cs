using FluentValidation;
using ThreeLayer.Business.Models;

namespace ThreeLayer.Business.Validation
{
    public class BrazilianPersonValidation : AbstractValidator<BrazilianPerson>
    {
        public BrazilianPersonValidation()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("The field {PropertyName} need to be filled")
                .Length(2, 200).WithMessage("The field {PropertyName} needs to have between {MinLength} and {MaxLength}");

            RuleFor(c => c.SecondName)
                .NotEmpty().WithMessage("The field {PropertyName} need to be filled")
                .Length(2, 200).WithMessage("The field {PropertyName} needs to have between {MinLength} and {MaxLength}");

            RuleFor(c => c.BirthDate)
                .NotEmpty().WithMessage("The field {PropertyName} need to be filled")
                .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("The field {PropertyName} needs to be less than {PropertyValue}");
        }
    }
}
