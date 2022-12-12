namespace Checkout.PaymentGateway.Api.Model
{
    public class PaymentResult
    {
        public PaymentResult(string merchantReference, string paymentId, string status)
        {
            MerchantReference = merchantReference;
            PaymentId = paymentId;
            Status = status;
        }

        public string MerchantReference { get; set; }

        public string PaymentId { get; set; }

        public string Status { get; set; }
    }
}