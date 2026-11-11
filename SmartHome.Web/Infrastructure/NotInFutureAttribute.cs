using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SmartHome.Web.Common;

namespace SmartHome.Web.Infrastructure
{
    // Собствен атрибут: проверява датата и на сървъра (IsValid), и в браузъра (AddValidation)
    [AttributeUsage(AttributeTargets.Property)]
    public class NotInFutureAttribute : ValidationAttribute, IClientModelValidator
    {
        public NotInFutureAttribute()
            : base(ErrorMessages.DateInFuture)
        {
        }

        public override bool IsValid(object? value)
        {
            if (value is not DateTime date)
            {
                return true;
            }

            return date.Date <= DateTime.Today;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            string message = FormatErrorMessage(context.ModelMetadata.GetDisplayName());

            context.Attributes.TryAdd("data-val", "true");
            context.Attributes.TryAdd("data-val-notinfuture", message);
        }
    }
}