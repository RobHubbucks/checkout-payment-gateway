using System.ComponentModel.DataAnnotations;

namespace Checkout.PaymentGateway.Api.Dto
{
    /// <summary>
    /// Represents a customer card
    /// </summary>
    public class UnMaskedCardDto : CardDto
    {
        /// <summary>
        /// Full card number without - or whitespace
        /// </summary>
        [Required]
        [CreditCard]
        public string Number { get; set; } = null!;

        /// <summary>
        /// Card Cvv
        /// </summary>
        public string? Cvv { get; set; } = null!;
    }
}