namespace Checkout.PaymentGateway.Api.Model
{
    public class Card
    {
        public Card(string number, string? cvv, int expiryMonth, int expiryYear, string? cardholderName)
        {
            Number = number;
            Cvv = cvv;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            CardholderName = cardholderName;
        }

        public string Number { get; set; }

        public string? Cvv { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string? CardholderName { get; set; }
    }
}