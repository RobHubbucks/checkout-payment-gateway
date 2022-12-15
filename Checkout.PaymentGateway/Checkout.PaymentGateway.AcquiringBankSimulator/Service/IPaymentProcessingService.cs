using Checkout.PaymentGateway.AcquiringBankSimulator.Dto;

namespace Checkout.PaymentGateway.AcquiringBankSimulator.Service
{
    public interface IPaymentProcessingService
    {
        Task<PaymentResponseDto> ProcessPayment(PaymentDto paymentToProcess);
    }
}