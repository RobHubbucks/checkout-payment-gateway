namespace Checkout.PaymentGateway.Api.Dto
{
    /// <summary>
    /// Details of a prior payment made via this gateway
    /// </summary>
    public class PaymentDetailsDto
    {
        /// <summary>
        /// 
        /// </summary>
        public PaymentDetailsDto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="merchantReference"></param>
        /// <param name="cardDetails"></param>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <param name="status"></param>
        public PaymentDetailsDto(string paymentId, string merchantReference, MaskedCardDto cardDetails, string currency, decimal amount, string status)
        {
            PaymentId = paymentId;
            MerchantReference = merchantReference;
            CardDetails = cardDetails;
            Currency = currency;
            Amount = amount;
            Status = status;
        }

        /// <summary>
        /// The ID of the prior payment
        /// </summary>
        public string PaymentId { get; set; } = null!;

        /// <summary>
        /// The merchant reference of the prior payment
        /// </summary>
        public string MerchantReference { get; set; } = null!;

        /// <summary>
        /// Masked customer card details
        /// </summary>
        public MaskedCardDto CardDetails { get; set; } = null!;

        /// <summary>
        /// Payment currency
        /// </summary>
        public string Currency { get; set; } = null!;

        /// <summary>
        /// Payment amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Status of the payment
        /// </summary>
        public string Status { get; set; } = null!;
    }
}