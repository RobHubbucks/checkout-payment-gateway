using System.ComponentModel.DataAnnotations;

namespace Checkout.PaymentGateway.Api.Dto.Validation
{
    public class PaymentExpiryDateValidator : ValidationAttribute
    {
        private const string Error = "Expiry date must be in the future";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var cardDetails = (CardDto)validationContext.ObjectInstance;

            var today = DateTime.Today;

            if (cardDetails.ExpiryYear < today.Year)
                return new ValidationResult(Error);

            if (cardDetails.ExpiryMonth < 1 || cardDetails.ExpiryMonth > 12)
                return new ValidationResult(Error);

            if (cardDetails.ExpiryYear == today.Year &&
                cardDetails.ExpiryMonth < today.Month)
                return new ValidationResult(Error);

            return ValidationResult.Success;
        }
    }
}