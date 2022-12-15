using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Checkout.PaymentGateway.Api.Dto.Validation
{
    public class CurrencyCodeValidator : ValidationAttribute
    {
        private const string Error = "Currency must be a valid ISO currency code";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currencyCode = (string?)value;

            if (string.IsNullOrWhiteSpace(currencyCode))
                return new ValidationResult(Error);

            var valid = CurrencySymbolExists(currencyCode);

            if (!valid)
                return new ValidationResult(Error);

            return ValidationResult.Success;
        }

        public bool CurrencySymbolExists(string currencySymbol)
        {
            var symbol = CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .Where(c => !c.IsNeutralCulture)
                .Select(culture =>
                {
                    try
                    {
                        return new RegionInfo(culture.Name);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(ri => ri != null && ri.ISOCurrencySymbol.Equals(currencySymbol))
                .Select(ri => ri?.CurrencySymbol)
                .FirstOrDefault();

            return !string.IsNullOrWhiteSpace(symbol);
        }
    }
}