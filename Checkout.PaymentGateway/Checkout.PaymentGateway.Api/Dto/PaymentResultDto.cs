namespace Checkout.PaymentGateway.Api.Dto
{
    /// <summary>
    /// Represents the result of a payment made via the payment gateway
    /// </summary>
    public class PaymentResultDto
    {
        /// <summary>
        /// 
        /// </summary>
        public PaymentResultDto() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantReference"></param>
        /// <param name="paymentId"></param>
        /// <param name="status"></param>
        public PaymentResultDto(string merchantReference, string paymentId, string status)
        {
            MerchantReference = merchantReference;
            PaymentId = paymentId;
            Status = new PaymentStatusDto(status);
        }

        /// <summary>
        /// Merchant reference of this payment
        /// </summary>
        public string MerchantReference { get; set; } = null!;

        /// <summary>
        /// Id of this payment
        /// </summary>
        public string PaymentId { get; set; } = null!;

        /// <summary>
        /// Status of this payment
        /// </summary>
        public PaymentStatusDto Status { get; set; } = null!;
    }
}