using Checkout.PaymentGateway.Api.Commands;
using Checkout.PaymentGateway.Api.Configuration;
using Checkout.PaymentGateway.Api.Dto.Mapping;
using Checkout.PaymentGateway.Api.Dto;
using Checkout.PaymentGateway.Api.Model;
using Checkout.PaymentGateway.Api.Service;
using Checkout.PaymentGateway.Api.Service.AcquiringBank;
using Checkout.PaymentGateway.DataAccess;
using Checkout.PaymentGateway.Api.Queries;

namespace Checkout.PaymentGateway.Api.IoC
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var configurationService = new ConfigurationService(configurationManager);

            services.AddScoped<IConfigurationService>(_ => configurationService);
            services.AddScoped<IMapper<PaymentDetailsDto, PaymentDetails>, PaymentDetailsMapper>();
            services.AddScoped<IMapper<PaymentRequestDto, PaymentRequestCommand>, PaymentRequestMapper>();
            services.AddScoped<IMapper<PaymentResultDto, PaymentResult>, PaymentResultMapper>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICommandHandler<PaymentRequestCommand, PaymentResult>, PaymentRequestCommandHandler>();
            services.AddScoped<IQueryHandler<GetPaymentDetailsQuery, PaymentDetails>, GetPaymentDetailsQueryHandler>();
            services.AddScoped<IAcquiringBankService, AcquiringBankService>();

            services.AddSingleton<IRepository<string, PaymentDetails>, InMemoryRepository<string, PaymentDetails>>();
        }
    }
}