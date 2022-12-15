namespace Checkout.PaymentGateway.Api.Dto
{
    /// <summary>
    /// Masked card details
    /// </summary>
    public class MaskedCardDto : CardDto
    {
        /// <summary>
        /// 
        /// </summary>
        public MaskedCardDto() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="last4Digits"></param>
        /// <param name="expiryMonth"></param>
        /// <param name="expiryYear"></param>
        /// <param name="cardholderName"></param>
        public MaskedCardDto(string last4Digits, int expiryMonth, int expiryYear, string? cardholderName)
        :base(expiryMonth, expiryYear, cardholderName)
        {
            Last4Digits = last4Digits;
        }

        /// <summary>
        /// Last 4 digits of a card number
        /// </summary>
        public string Last4Digits { get; set; } = null!;
    }
}