using Checkout.PaymentGateway.Api.Model;
using Checkout.PaymentGateway.Api.Service;

namespace Checkout.PaymentGateway.Api.Commands
{
    public class PaymentRequestCommandHandler : ICommandHandler<PaymentRequestCommand, PaymentResult>
    {
        private readonly IPaymentService _paymentService;

        public PaymentRequestCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<PaymentResult> Handle(PaymentRequestCommand command)
        {
            var paymentId = Guid.NewGuid().ToString();

            var payment = new PaymentDetails(paymentId, command.MerchantReference,
                command.CardDetails,
                command.Currency, command.Amount, Status.Pending,
                command.BillingAddress);

            return await _paymentService.RequestPayment(payment);
        }
    }
}