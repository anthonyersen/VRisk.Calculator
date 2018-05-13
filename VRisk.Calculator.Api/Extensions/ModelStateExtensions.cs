using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace VRisk.Calculator.Api.Extensions
{
    public static class ModelStateExtensions
    {
        public static string GetFirstError(this ModelStateDictionary modelState)
        {
            return modelState.Values
                .SelectMany(e => e.Errors)
                .Select(e => e.ErrorMessage)
                .FirstOrDefault();
        }
    }
}
