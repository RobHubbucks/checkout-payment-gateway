namespace Checkout.PaymentGateway.Api.Model
{
    public class PaymentRequest
    {
        public PaymentRequest(string currency, decimal amount, string merchantReference, Card cardDetails, Address billingAddress)
        {
            Currency = currency;
            Amount = amount;
            MerchantReference = merchantReference;
            CardDetails = cardDetails;
            BillingAddress = billingAddress;
        }

        public Card CardDetails { get; set; }

        public Address BillingAddress { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public string MerchantReference { get; set; }
    }
}