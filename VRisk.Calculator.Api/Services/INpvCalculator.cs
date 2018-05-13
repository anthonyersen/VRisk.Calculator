using VRisk.Calculator.Api.Models.Dto;

namespace VRisk.Calculator.Api.Services
{
    public interface INpvCalculator
    {
        CalculateNpvResponse CalculateNpv(CalculateNpvRequest request);
    }
}
