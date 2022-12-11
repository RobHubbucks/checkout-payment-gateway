namespace Checkout.PaymentGateway.Api.Model
{
    public class PaymentDetails
    {
        public PaymentDetails(string paymentId, string merchantReference, Card cardDetails, string currency, decimal amount, int statusCode, string status)
        {
            PaymentId = paymentId;
            MerchantReference = merchantReference;
            CardDetails = cardDetails;
            Currency = currency;
            Amount = amount;
            StatusCode = statusCode;
            Status = status;
        }

        public string PaymentId { get; set; }

        public string MerchantReference { get; set; }

        public Card CardDetails { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public int StatusCode { get; set; }

        public string Status { get; set; }
    }
}