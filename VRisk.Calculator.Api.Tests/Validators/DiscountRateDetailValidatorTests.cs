using FluentAssertions;
using NUnit.Framework;
using VRisk.Calculator.Api.Models;
using VRisk.Calculator.Api.Validators;

namespace VRisk.Calculator.Api.Tests.Validators
{
    [TestFixture(Category = "Unit")]
    public class DiscountRateDetailValidatorTests : TestBase<DiscountRateDetailValidator>
    {
        private DiscountRateDetail _validDiscountRateDetail;

        public static DiscountRateDetail CreateValidDiscountRateDetail()
        {
            return new DiscountRateDetail
            {
                LowerBoundPercentage = 1,
                UpperBoundPercentage = 15,
                IncrementPercentage = 0.25
            };
        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _validDiscountRateDetail = CreateValidDiscountRateDetail();
        }

        [Test]
        public void Validate_WhenValid_ShouldReturnTrue()
        {
            var target = CreateTarget();
            var result = target.Validate(_validDiscountRateDetail);

            result.IsValid.Should().BeTrue();
        }

        [Description("Lower bound must be >= upper bound")]
        [TestCase(3.0, 3.01, true)]
        [TestCase(3.0, 3.0, true)]
        [TestCase(3.1, 3.0, false)]
        public void Validate_LowerAndUpperBoundRelationship_ShouldReturnExpected(double lowerBound, double upperBound, bool expectedResult)
        {
            _validDiscountRateDetail.LowerBoundPercentage = lowerBound;
            _validDiscountRateDetail.UpperBoundPercentage = upperBound;

            var target = CreateTarget();
            var result = target.Validate(_validDiscountRateDetail);

            result.IsValid.Should().Be(expectedResult);
        }

        [Description("Increment percentage must be >= 0")]
        [TestCase(0, true)]
        [TestCase(2.25, true)]
        [TestCase(-0.01, false)]
        public void Validate_IncrementPercentage_ShouldReturnExpected(double increment, bool expectedResult)
        {
            _validDiscountRateDetail.IncrementPercentage = increment;

            var target = CreateTarget();
            var result = target.Validate(_validDiscountRateDetail);

            result.IsValid.Should().Be(expectedResult);
        }
    }
}
