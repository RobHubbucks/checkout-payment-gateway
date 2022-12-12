namespace Checkout.PaymentGateway.Api.Model
{
    public class PaymentDetails
    {
        public PaymentDetails(string paymentId, string merchantReference, Card cardDetails, string currency, decimal amount, Status status)
        {
            PaymentId = paymentId;
            MerchantReference = merchantReference;
            CardDetails = cardDetails;
            Currency = currency;
            Amount = amount;
            Status = status;
        }

        public string PaymentId { get; set; }

        public string MerchantReference { get; set; }

        public Card CardDetails { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public Status Status{ get; set; }
    }
}