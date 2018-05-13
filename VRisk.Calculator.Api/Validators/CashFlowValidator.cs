using FluentValidation;

namespace VRisk.Calculator.Api.Validators
{
    public class CashFlowValidator : AbstractValidator<double>
    {
        public CashFlowValidator()
        {
            RuleFor(x => x)
                .GreaterThanOrEqualTo(0).WithMessage("Cash flow must be greater than or equal to 0");
        }
    }
}
