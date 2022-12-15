using Checkout.PaymentGateway.AcquiringBankSimulator.Resources;
using Checkout.PaymentGateway.AcquiringBankSimulator.Service;

namespace Checkout.PaymentGateway.AcquiringBankSimulator.IoC
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddSingleton<IPaymentProcessingService>(_ => new PaymentProcessingService(GetBanks(env.ContentRootPath)));
        }

        private static IList<IBank> GetBanks(string path)
        {
            using StreamReader sr = new StreamReader($"{path}/Resources/Banks.json");
            string json = sr.ReadToEnd();

            var bankData = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<BankData>>(json);

            var banks = new List<IBank>();

            if (bankData == null)
                return banks;

            foreach (var data in bankData)
            {
                banks.Add(new Bank(data.Name, data.Accounts));
            }

            return banks;
        }
    }
}