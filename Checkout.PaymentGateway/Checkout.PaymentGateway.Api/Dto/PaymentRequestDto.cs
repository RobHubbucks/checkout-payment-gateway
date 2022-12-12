using System.ComponentModel.DataAnnotations;
using Checkout.PaymentGateway.Api.Dto.Validation;

namespace Checkout.PaymentGateway.Api.Dto
{
    public class PaymentRequestDto
    {
        private const int CurrencyCodeLength = 3;

        [Required]
        public UnMaskedCardDto CardDetails { get; set; } = null!;

        public AddressDto BillingAddress { get; set; } = null!;

        [Required]
        [StringLength(CurrencyCodeLength, ErrorMessage = "Currency code must be 3 characters", MinimumLength = CurrencyCodeLength)]
        public string Currency { get; set; } = null!;

        [Required]
        [PaymentAmountValidator]
        public decimal Amount { get; set; }

        [Required]
        public string MerchantReference { get; set; } = null!;
    }
}