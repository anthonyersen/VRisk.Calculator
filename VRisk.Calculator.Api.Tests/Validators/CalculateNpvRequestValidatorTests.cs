using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using VRisk.Calculator.Api.Models.Dto;
using VRisk.Calculator.Api.Validators;

namespace VRisk.Calculator.Api.Tests.Validators
{
    [TestFixture]
    public class CalculateNpvRequestValidatorTests : TestBase<CalculateNpvRequestValidator>
    {
        private CalculateNpvRequest _validCalculateNpvRequest;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _validCalculateNpvRequest = new CalculateNpvRequest
            {
                CashFlows = new List<double> { 1000, 2000, 3000 },
                DiscountRateDetail = DiscountRateDetailValidatorTests.CreateValidDiscountRateDetail(),
                InitialInvestment = 10000 
            };
        }

        [Test]
        public void Validate_WhenValid_ShouldReturnTrue()
        {
            var target = CreateTarget();
            var result = target.Validate(_validCalculateNpvRequest);

            result.IsValid.Should().BeTrue();
        }

        [Description("Initial investment must be > 0")]
        [TestCase(-1, false)]
        [TestCase(0, false)]
        [TestCase(0.01, true)]
        public void Validate_InitialInvestment_ShouldReturnExpected(double initialInvestment, bool expected)
        {
            _validCalculateNpvRequest.InitialInvestment = initialInvestment;

            var target = CreateTarget();
            var result = target.Validate(_validCalculateNpvRequest);

            result.IsValid.Should().Be(expected);
        }

        [Test]
        public void Validate_WhenCashFlowsIsNullOrEmpty_ShouldReturnFalse()
        {
            var values = new[] { new List<double>(), null };

            foreach (var cashFlowValue in values)
            {
                _validCalculateNpvRequest.CashFlows = cashFlowValue;

                var target = CreateTarget();
                var result = target.Validate(_validCalculateNpvRequest);

                result.IsValid.Should().BeFalse();
            }
        }

        [Test]
        public void Validate_WhenDiscountRateDetailIsNull_ShouldReturnFalse()
        {
            _validCalculateNpvRequest.DiscountRateDetail = null;

            var target = CreateTarget();
            var result = target.Validate(_validCalculateNpvRequest);

            result.IsValid.Should().BeFalse();
        }
    }
}
