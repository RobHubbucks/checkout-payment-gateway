using Checkout.PaymentGateway.Api.Model;

namespace Checkout.PaymentGateway.Api.Commands
{
    public class PaymentRequestCommand
    {
        public PaymentRequestCommand(string currency, decimal amount, string merchantReference, Card cardDetails, Address? billingAddress)
        {
            Currency = currency;
            Amount = amount;
            MerchantReference = merchantReference;
            CardDetails = cardDetails;
            BillingAddress = billingAddress;
        }

        public Card CardDetails { get; set; }

        public Address? BillingAddress { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public string MerchantReference { get; set; }
    }
}