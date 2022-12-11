namespace Checkout.PaymentGateway.Api.Dto
{
    public class PaymentDetailsDto
    {
        public PaymentDetailsDto()
        {

        }

        public PaymentDetailsDto(string paymentId, string merchantReference, MaskedCardDto cardDetails, string currency, decimal amount, PaymentStatusDto status)
        {
            PaymentId = paymentId;
            MerchantReference = merchantReference;
            CardDetails = cardDetails;
            Currency = currency;
            Amount = amount;
            Status = status;
        }

        public string PaymentId { get; set; } = null!;

        public string MerchantReference { get; set; } = null!;

        public MaskedCardDto CardDetails { get; set; } = null!;

        public string Currency { get; set; } = null!;

        public decimal Amount { get; set; }

        public PaymentStatusDto Status { get; set; } = null!;
    }
}