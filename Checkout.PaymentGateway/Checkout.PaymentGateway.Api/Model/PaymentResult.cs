namespace Checkout.PaymentGateway.Api.Model
{
    public class PaymentResult
    {
        public PaymentResult(string merchantReference, string paymentId, int statusCode, string status)
        {
            MerchantReference = merchantReference;
            PaymentId = paymentId;
            StatusCode = statusCode;
            Status = status;
        }

        public string MerchantReference { get; set; }

        public string PaymentId { get; set; }

        public int StatusCode { get; set; }

        public string Status { get; set; }
    }
}