namespace Checkout.PaymentGateway.AcquiringBankSimulator.Model
{
    public class Card
    {
        public Card(string number, string? cvv, int expiryMonth, int expiryYear)
        {
            Number = number;
            Cvv = cvv;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
        }

        public Card() { }

        public string Number { get; set; } = null!;

        public string? Cvv { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }
    }
}