namespace Checkout.PaymentGateway.Api.Model
{
    public class PaymentDetails
    {
        public PaymentDetails(string id, string merchantReference, Card cardDetails, string currency, decimal amount, Status status, Address? billingAddress)
        {
            Id = id;
            MerchantReference = merchantReference;
            CardDetails = cardDetails;
            Currency = currency;
            Amount = amount;
            Status = status;
            BillingAddress = billingAddress;
        }

        public string Id { get; set; }

        public string MerchantReference { get; set; }

        public Card CardDetails { get; set; }

        public Address? BillingAddress { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public Status Status{ get; set; }
    }
}