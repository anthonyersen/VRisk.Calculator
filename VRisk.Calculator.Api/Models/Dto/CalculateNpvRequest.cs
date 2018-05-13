using System.Collections.Generic;

namespace VRisk.Calculator.Api.Models.Dto
{
    public class CalculateNpvRequest
    {
        public double InitialInvestment { get; set; }
        public DiscountRateDetail DiscountRateDetail { get; set; }
        public IList<double> CashFlows { get; set; } = new List<double>();
    }
}
