using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using VRisk.Calculator.Api.Models;
using VRisk.Calculator.Api.Models.Dto;
using VRisk.Calculator.Api.Services;
using System.Linq;

namespace VRisk.Calculator.Api.Tests.Services
{
    [TestFixture(Category = "Unit")]
    public class NpvCalculatorTests : TestBase<NpvCalculator>
    {
        [Test]
        public void CalculateNpv_ShouldCalculateNpvForDifferentDiscounts()
        {
            var request = new CalculateNpvRequest
            {
                InitialInvestment = 12000,
                CashFlows = new List<double> { 2500, 4000, 5000, 3000, 1000 },
                DiscountRateDetail = new DiscountRateDetail
                {
                    LowerBoundPercentage = 1,
                    UpperBoundPercentage = 15,
                    IncrementPercentage = 0.25 
                }
            };

            var target = CreateTarget();
            var result = target.CalculateNpv(request);

            result.Should().HaveCount(57);
            result.All(r => r.PeriodNpvs.Count == 5).Should().BeTrue();
            var tolerance = 0.0001;

            var firstResult = result.First();
            firstResult.DiscountPercentage.Should().Be(1);
            firstResult.PeriodNpvs.Last().Should().Be(firstResult.Npv);
            firstResult.Npv.Should().BeApproximately(3083.7891, tolerance);

            var midResult = result[23];
            midResult.DiscountPercentage.Should().Be(6.75);
            midResult.PeriodNpvs.Last().Should().Be(midResult.Npv);
            midResult.Npv.Should().BeApproximately(993.8657, tolerance);

            var lastResult = result.Last();
            lastResult.DiscountPercentage.Should().Be(15);
            lastResult.PeriodNpvs.Last().Should().Be(lastResult.Npv);
            lastResult.Npv.Should().BeApproximately(-1301.4947, tolerance);
        }
    }
}
