using System.ComponentModel.DataAnnotations;
using Checkout.PaymentGateway.Api.Dto.Validation;

namespace Checkout.PaymentGateway.Api.Dto
{
    /// <summary>
    /// Represents a card
    /// </summary>
    public abstract class CardDto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expiryMonth"></param>
        /// <param name="expiryYear"></param>
        /// <param name="cardholderName"></param>
        protected CardDto(int expiryMonth, int expiryYear, string? cardholderName = null)
        {
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            CardholderName = cardholderName;
        }

        /// <summary>
        /// 
        /// </summary>
        protected CardDto() { }

        /// <summary>
        /// Expiry month of the card
        /// </summary>
        [Required]
        [PaymentExpiryDateValidator]
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// Expiry year of the card
        /// </summary>
        [Required]
        [PaymentExpiryDateValidator]
        public int ExpiryYear { get; set; }

        /// <summary>
        /// Cardholder name
        /// </summary>
        public string? CardholderName { get; set; }
    }
}