using Checkout.PaymentGateway.Api.Model;
using Checkout.PaymentGateway.Api.Service;

namespace Checkout.PaymentGateway.Api.Queries
{
    public class GetPaymentDetailsQueryHandler : IQueryHandler<GetPaymentDetailsQuery, PaymentDetails>
    {
        private readonly IPaymentService _paymentService;

        public GetPaymentDetailsQueryHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<PaymentDetails?> Execute(GetPaymentDetailsQuery query)
        {
            return await _paymentService.GetPayment(query.PaymentId);
        }
    }
}