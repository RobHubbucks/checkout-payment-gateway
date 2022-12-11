using System.ComponentModel.DataAnnotations;

namespace Checkout.PaymentGateway.Api.Dto
{
    public class AddressDto
    {
        [Required]
        public string Address1 { get; set; } = null!;

        public string? Address2 { get; set; }

        [Required] 
        public string Postcode { get; set; } = null!;

        public string? City { get; set; }

        [Required]
        public string Country { get; set; } = null!;
    }
}