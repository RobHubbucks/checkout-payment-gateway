namespace Checkout.PaymentGateway.AcquiringBankSimulator.Model
{
    public class BankAccount
    {
        public BankAccount(string name, Card card, Address address, decimal balance)
        {
            Name = name;
            Card = card;
            BillingAddress = address;
            Balance = balance;
        }

        public BankAccount() { }

        public string Name { get; set; } = null!;

        public Card Card { get; set; } = null!;

        public Address BillingAddress { get; set; } = null!;

        public decimal Balance { get; set; }
    }
}