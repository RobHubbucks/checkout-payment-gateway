namespace Checkout.PaymentGateway.Api.Model
{
    public class Address
    {
        public Address(string? address1, string? address2, string? postcode, string? city, string? country)
        {
            Address1 = address1;
            Address2 = address2;
            Postcode = postcode;
            City = city;
            Country = country;
        }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }
        
        public string? Postcode { get; set; }

        public string? City { get; set; }
        
        public string? Country { get; set; }
    }
}