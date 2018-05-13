using FluentAssertions;
using NUnit.Framework;
using VRisk.Calculator.Api.Validators;

namespace VRisk.Calculator.Api.Tests.Validators
{
    [TestFixture(Category = "Unit")]
    public class CashFlowValidatorTests : TestBase<CashFlowValidator>
    {
        [TestCase(-1, false)]
        [TestCase(0, true)]
        [TestCase(1, true)]
        public void Validate_ShouldReturnExpected(double cashFlow, bool expectedValid)
        {
            var target = CreateTarget();
            var result = target.Validate(cashFlow);

            result.IsValid.Should().Be(expectedValid);
        }
    }
}
