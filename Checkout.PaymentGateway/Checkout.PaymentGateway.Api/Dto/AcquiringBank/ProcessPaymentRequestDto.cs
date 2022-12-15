namespace Checkout.PaymentGateway.Api.Dto.AcquiringBank
{
    public class ProcessPaymentRequestDto
    {
        public ProcessPaymentRequestDto(
            string number, string? cvv, int expiryMonth, int expiryYear, string currency, decimal amount,
            string? cardholderName, string? address1, string? address2, string? postcode,
            string? city, string? country)
        {
            Number = number;
            Cvv = cvv;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            CardholderName = cardholderName;
            Currency = currency;
            Amount = amount;
            BillingAddress = address1 != null || address2 != null || postcode != null || city != null || country != null ? new AddressDto(address1, address2, postcode, city, country) : null;
        }

        public string Number { get; set; }

        public string? Cvv { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string? CardholderName { get; set; }

        public AddressDto? BillingAddress { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }
    }
}