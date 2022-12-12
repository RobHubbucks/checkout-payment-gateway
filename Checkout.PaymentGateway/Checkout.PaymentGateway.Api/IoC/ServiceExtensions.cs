using Checkout.PaymentGateway.Api.Dto.Mapping;
using Checkout.PaymentGateway.Api.Dto;
using Checkout.PaymentGateway.Api.Model;
using Checkout.PaymentGateway.Api.Service;
using Checkout.PaymentGateway.Api.Service.AcquiringBank;
using Checkout.PaymentGateway.DataAccess;

namespace Checkout.PaymentGateway.Api.IoC
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMapper<PaymentDetailsDto, PaymentDetails>, PaymentDetailsMapper>();
            services.AddScoped<IMapper<PaymentRequestDto, PaymentRequest>, PaymentRequestMapper>();
            services.AddScoped<IMapper<PaymentResultDto, PaymentResult>, PaymentResultMapper>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IAcquiringBankService, AcquiringBankService>();

            services.AddSingleton<IRepository<string, PaymentDetails>, InMemoryRepository<string, PaymentDetails>>();
        }
    }
}