using System.ComponentModel.DataAnnotations;
using Checkout.PaymentGateway.Api.Dto.Validation;

namespace Checkout.PaymentGateway.Api.Dto
{
    /// <summary>
    /// Request body used to make a payment request
    /// </summary>
    public class PaymentRequestDto
    {
        private const int CurrencyCodeLength = 3;

        /// <summary>
        /// Customer card details
        /// </summary>
        [Required]
        public UnMaskedCardDto CardDetails { get; set; } = null!;

        /// <summary>
        /// Optional customer billing address
        /// </summary>
        public AddressDto? BillingAddress { get; set; }

        /// <summary>
        /// ISO currency code
        /// </summary>
        [Required]
        [CurrencyCodeValidator]
        public string Currency { get; set; } = null!;

        /// <summary>
        /// Transaction amount
        /// </summary>
        [Required]
        [PaymentAmountValidator]
        public decimal Amount { get; set; }

        /// <summary>
        /// Merchant reference for this transaction
        /// </summary>
        [Required]
        public string MerchantReference { get; set; } = null!;
    }
}