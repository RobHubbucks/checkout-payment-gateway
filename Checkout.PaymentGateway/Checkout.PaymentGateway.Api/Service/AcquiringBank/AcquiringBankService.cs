using System.Text;
using System.Text.Json;
using Checkout.PaymentGateway.Api.Configuration;
using Checkout.PaymentGateway.Api.Dto.AcquiringBank;

namespace Checkout.PaymentGateway.Api.Service.AcquiringBank
{
    /// <summary>
    /// A wrapper around the acquiring bank API
    /// </summary>
    public class AcquiringBankService : IAcquiringBankService
    {
        private readonly HttpClient _httpClient;
        private readonly string _acquiringBankApiBaseUrl;

        public AcquiringBankService(HttpClient httpClient, IConfigurationService configurationService)
        {
            _httpClient = httpClient;
            _acquiringBankApiBaseUrl = configurationService.AcquiringBankApiUrl;
        }

        public async Task<ProcessPaymentResponseDto?> ProcessPayment(ProcessPaymentRequestDto gatewayRequest)
        {
            var json = JsonSerializer.Serialize(gatewayRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_acquiringBankApiBaseUrl}/payments", content);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new PaymentProcessingException(e);
            }

            var result = await response.Content.ReadFromJsonAsync<ProcessPaymentResponseDto>();

            return result;
        }
    }
}