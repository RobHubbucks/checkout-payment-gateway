namespace Checkout.PaymentGateway.Api.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        public ConfigurationService(ConfigurationManager configurationManager)
        {
            AcquiringBankApiUrl = configurationManager["AcquiringBankApiUrl"];
        }

        public string AcquiringBankApiUrl { get; set; }
    }
}