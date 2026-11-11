using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SmartHome.Web.Infrastructure
{
    public static class ModelStateExtensions
    {
        public static void AddErrors(this ModelStateDictionary modelState, IDictionary<string, string> errors)
        {
            foreach (var (key, message) in errors)
            {
                modelState.AddModelError(key, message);
            }
        }
    }
}