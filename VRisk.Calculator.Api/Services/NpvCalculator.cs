using System;
using System.Collections.Generic;
using System.Linq;
using VRisk.Calculator.Api.Models;
using VRisk.Calculator.Api.Models.Dto;

namespace VRisk.Calculator.Api.Services
{
    public class NpvCalculator : INpvCalculator
    {
        public CalculateNpvResponse CalculateNpv(CalculateNpvRequest request)
        {
            var result = new CalculateNpvResponse();
            var discountDetail = request.DiscountRateDetail;

            var noIncrement = discountDetail.IncrementPercentage == 0;

            for (var discountPercentage = discountDetail.LowerBoundPercentage; discountPercentage <= discountDetail.UpperBoundPercentage; discountPercentage += discountDetail.IncrementPercentage)
            {
                var npvsForDiscount = CalculateNpvForDiscount(request.InitialInvestment, request.CashFlows, discountPercentage);
                result.Add(new NpvResult
                {
                    DiscountPercentage = discountPercentage,
                    PeriodNpvs = npvsForDiscount,
                    Npv = npvsForDiscount.LastOrDefault()
                });

                if (noIncrement) break;
            }

            return result;
        }

        private List<double> CalculateNpvForDiscount(double initialInvestment, IList<double> cashFlows, double discountPercentage)
        {
            var result = new List<double>();
            var discount = discountPercentage / 100;
            var npv = -initialInvestment;

            for (int i = 0; i < cashFlows.Count; i++)
            {
                var cashFlow = cashFlows[i];
                var periodNpv = cashFlow / Math.Pow(1 + discount, i + 1);
                npv += periodNpv;
                result.Add(npv);
            }

            return result;
        }
    }
}
