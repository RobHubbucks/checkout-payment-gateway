using System.Text.RegularExpressions;

namespace Checkout.PaymentGateway.Api.Extensions
{
    public static class StringExtensions
    {
        public static string MaskCardNumber(this string cardNumber)
        {
            return cardNumber.Substring(cardNumber.Length - 4, 4);
        }

        public static string SanitiseCardNumber(this string cardNumber)
        {
            var sanitised = Regex.Replace(cardNumber, "[- ]", "");

            return sanitised;
        }
    }
}