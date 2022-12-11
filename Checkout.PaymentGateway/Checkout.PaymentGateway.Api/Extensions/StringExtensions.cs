namespace Checkout.PaymentGateway.Api.Extensions
{
    public static class StringExtensions
    {
        public static string MaskCardNumber(this string cardNumber)
        {
            return cardNumber.Substring(cardNumber.Length - 4, 4);
        }
    }
}