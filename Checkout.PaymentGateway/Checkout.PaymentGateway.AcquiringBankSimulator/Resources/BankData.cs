using Checkout.PaymentGateway.AcquiringBankSimulator.Model;

namespace Checkout.PaymentGateway.AcquiringBankSimulator.Resources
{
    public class BankData
    {
        public string Name { get; set; } = null!;

        public IList<BankAccount> Accounts { get; set; } = null!;
    }
}