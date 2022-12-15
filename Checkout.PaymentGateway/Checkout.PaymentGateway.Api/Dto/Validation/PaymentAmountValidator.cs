using System.ComponentModel.DataAnnotations;

namespace Checkout.PaymentGateway.Api.Dto.Validation
{
    public class PaymentAmountValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is decimal and > 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Payment amount must be greater than 0");
        }
    }
}