using System.ComponentModel.DataAnnotations;

namespace Checkout.PaymentGateway.Api.Dto
{
    /// <summary>
    /// Represents a billing address
    /// </summary>
    public class AddressDto
    {
        /// <summary>
        /// </summary>
        public AddressDto() { }

        /// <summary>
        /// </summary>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="postcode"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        public AddressDto(string? address1, string? address2, string? postcode, string? city, string? country)
        {
            Address1 = address1;
            Address2 = address2;
            Postcode = postcode;
            City = city;
            Country = country;
        }

        /// <summary>
        /// Address line 1
        /// </summary>
        [Required]
        public string? Address1 { get; set; }

        /// <summary>
        /// Address line 2
        /// </summary>
        public string? Address2 { get; set; }

        /// <summary>
        /// Postcode
        /// </summary>
        [Required] 
        public string? Postcode { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Two character ISO country code
        /// </summary>
        [Required]
        public string? Country { get; set; }
    }
}