using FluentValidation;
using VRisk.Calculator.Api.Models;

namespace VRisk.Calculator.Api.Validators
{
    public class DiscountRateDetailValidator : AbstractValidator<DiscountRateDetail>
    {
        public DiscountRateDetailValidator()
        {
            RuleFor(x => x.LowerBoundPercentage)
                .LessThanOrEqualTo(x => x.UpperBoundPercentage)
                .WithMessage(x => "The lower bound discount rate must be less than or equal to the higher bound");

            RuleFor(x => x.IncrementPercentage)
                .GreaterThanOrEqualTo(0)
                .WithMessage(x => "The discount rate increment must be greater than or equal to 0");
        }
    }
}
