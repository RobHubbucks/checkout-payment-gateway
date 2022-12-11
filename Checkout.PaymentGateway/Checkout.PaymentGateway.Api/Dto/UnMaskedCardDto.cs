using System.ComponentModel.DataAnnotations;

namespace Checkout.PaymentGateway.Api.Dto
{
    public class UnMaskedCardDto : CardDto
    {
        private const int MinCvv = 3;
        private const int MaxCvv = 4;

        [Required]
        public string Number { get; set; } = null!;

        [Required]
        [StringLength(MaxCvv, ErrorMessage = "Cvv must be 3 or 4 digits", MinimumLength = MinCvv)]
        public string Cvv { get; set; } = null!;
    }
}