using System.Collections.Generic;

namespace VRisk.Calculator.Api.Models
{
    public class NpvResult
    {
        public double DiscountPercentage { get; set; }
        public IList<double> PeriodNpvs { get; set; } = new List<double>();
        public double Npv { get; set; }
    }
}
