using Checkout.PaymentGateway.Api.Dto.AcquiringBank;

namespace Checkout.PaymentGateway.Api.Service.AcquiringBank
{
    public interface IAcquiringBankService
    {
        Task<ProcessPaymentResponseDto> ProcessPayment(ProcessPaymentRequestDto gatewayRequest);
    }
}