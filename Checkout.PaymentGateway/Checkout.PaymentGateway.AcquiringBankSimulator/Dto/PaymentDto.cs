using System.ComponentModel.DataAnnotations;

namespace Checkout.PaymentGateway.AcquiringBankSimulator.Dto
{
    public class PaymentDto
    {
        public PaymentDto(
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

        public PaymentDto()
        {
            Number = string.Empty;
            Currency = string.Empty;
        }

        [Required]
        public string Number { get; set; }

        public string? Cvv { get; set; }

        [Required]
        public int ExpiryMonth { get; set; }

        [Required]
        public int ExpiryYear { get; set; }

        public string? CardholderName { get; set; }

        public AddressDto? BillingAddress { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}