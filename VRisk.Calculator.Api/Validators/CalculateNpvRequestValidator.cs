using FluentValidation;
using VRisk.Calculator.Api.Models.Dto;
using System.Linq;

namespace VRisk.Calculator.Api.Validators
{
    public class CalculateNpvRequestValidator : AbstractValidator<CalculateNpvRequest>
    {
        public CalculateNpvRequestValidator()
        {
            RuleFor(x => x.InitialInvestment)
                .GreaterThan(0)
                .WithMessage(x => "The initial investment must be a positive value");

            RuleFor(x => x.CashFlows)
                .NotEmpty()
                .WithMessage(x => "Cash flows must have at least one item");
            RuleFor(x => x.CashFlows)
                .SetCollectionValidator(new CashFlowValidator());

            RuleFor(x => x.DiscountRateDetail)
                .NotNull()
                .WithMessage(x => "Discount rate details is required");
            RuleFor(x => x.DiscountRateDetail)
                .SetValidator(new DiscountRateDetailValidator());
        }
    }
}
