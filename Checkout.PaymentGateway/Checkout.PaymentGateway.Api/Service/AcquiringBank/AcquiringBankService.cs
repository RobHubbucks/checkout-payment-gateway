using Checkout.PaymentGateway.Api.Dto.AcquiringBank;

namespace Checkout.PaymentGateway.Api.Service.AcquiringBank
{
    /// <summary>
    /// A wrapper around the acquiring bank API
    /// </summary>
    public class AcquiringBankService : IAcquiringBankService
    {
        private readonly HttpClient _httpClient;

        public AcquiringBankService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProcessPaymentResponseDto> ProcessPayment(ProcessPaymentRequestDto gatewayRequest)
        {
            return new ProcessPaymentResponseDto { Status = 100 };
        }
    }
}