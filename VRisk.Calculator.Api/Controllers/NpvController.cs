using Microsoft.AspNetCore.Mvc;
using VRisk.Calculator.Api.Extensions;
using VRisk.Calculator.Api.Models.Dto;
using VRisk.Calculator.Api.Services;

namespace VRisk.Calculator.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/npv")]
    public class NpvController : Controller
    {
        public const string GenericErrorMessage = "An error has occurred! Please validate your inputs.";

        private readonly INpvCalculator _npvCalculator;

        public NpvController(INpvCalculator npvCalculator)
        {
            _npvCalculator = npvCalculator;
        }

        [HttpPost]
        [Route("calculate")]
        [ProducesResponseType(typeof(CalculateNpvResponse), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult CalculateNpv([FromBody] CalculateNpvRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState.GetFirstError() ?? GenericErrorMessage);
            }

            try
            {
                var result = _npvCalculator.CalculateNpv(request);
                return Ok(result);
            }
            catch
            {
                return BadRequest(GenericErrorMessage);
            }
        }
    }
}