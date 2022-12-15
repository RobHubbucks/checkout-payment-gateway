using System.ComponentModel.DataAnnotations;

namespace Checkout.PaymentGateway.AcquiringBankSimulator.Dto
{
    public class AddressDto
    {
        public AddressDto() { }

        public AddressDto(string? address1, string? address2, string? postcode, string? city, string? country)
        {
            Address1 = address1;
            Address2 = address2;
            Postcode = postcode;
            City = city;
            Country = country;
        }

        [Required]
        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        [Required] 
        public string? Postcode { get; set; }

        public string? City { get; set; }

        [Required]
        public string? Country { get; set; }
    }
}