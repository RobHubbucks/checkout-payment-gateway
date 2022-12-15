using Checkout.PaymentGateway.AcquiringBankSimulator.Dto;

namespace Checkout.PaymentGateway.AcquiringBankSimulator.Service
{
    public interface IBank
    {
        string Name { get; set; }

        bool MakePayment(PaymentDto payment);

        bool CustomerExists(string cardNumber);
    }
}