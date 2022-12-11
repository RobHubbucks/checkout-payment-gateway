using Checkout.PaymentGateway.Api.Model;
using Checkout.PaymentGateway.DataAccess;

namespace Checkout.PaymentGateway.Api.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<string, PaymentDetails> _repository;

        public PaymentService(IRepository<string, PaymentDetails> repository)
        {
            _repository = repository;
        }

        public async Task<PaymentResult> RequestPayment(PaymentRequest paymentRequest)
        {
            var payment = new PaymentDetails(Guid.NewGuid().ToString(), paymentRequest.MerchantReference,
                new Card(paymentRequest.CardDetails.Number, paymentRequest.CardDetails.Cvv,
                    paymentRequest.CardDetails.ExpiryMonth, paymentRequest.CardDetails.ExpiryYear,
                    paymentRequest.CardDetails.CardholderName), paymentRequest.Currency, paymentRequest.Amount, 100,
                "Success");

            await _repository.Add(payment.PaymentId, payment);

            return new PaymentResult(payment.MerchantReference, payment.PaymentId, payment.StatusCode, payment.Status);
        }

        public async Task<PaymentDetails?> GetPayment(string paymentId)
        {
            var payment = await _repository.GetById(paymentId);

            return payment;
        }
    }
}