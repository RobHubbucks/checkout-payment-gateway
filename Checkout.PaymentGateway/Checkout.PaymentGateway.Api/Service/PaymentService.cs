using Checkout.PaymentGateway.Api.Dto.AcquiringBank;
using Checkout.PaymentGateway.Api.Model;
using Checkout.PaymentGateway.Api.Service.AcquiringBank;
using Checkout.PaymentGateway.DataAccess;

namespace Checkout.PaymentGateway.Api.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<string, PaymentDetails> _repository;
        private readonly IAcquiringBankService _acquiringBankService;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IRepository<string, PaymentDetails> repository, IAcquiringBankService acquiringBankService, ILogger<PaymentService> logger)
        {
            _repository = repository;
            _acquiringBankService = acquiringBankService;
            _logger = logger;
        }

        public async Task<PaymentResult> RequestPayment(PaymentRequest paymentRequest)
        {
            var paymentId = Guid.NewGuid().ToString();

            var payment = new PaymentDetails(paymentId, paymentRequest.MerchantReference,
                new Card(paymentRequest.CardDetails.Number, paymentRequest.CardDetails.Cvv,
                    paymentRequest.CardDetails.ExpiryMonth, paymentRequest.CardDetails.ExpiryYear,
                    paymentRequest.CardDetails.CardholderName), paymentRequest.Currency, paymentRequest.Amount, Status.Pending);

            await _repository.Add(paymentId, payment);

            try
            {
                var bankResult = await _acquiringBankService.ProcessPayment(GetBankPaymentRequestDto(paymentRequest));

                payment.Status = BankStatusToStatus(bankResult.Status);
            }
            catch (PaymentProcessingException e)
            {
                _logger.LogError(exception: e, message: "Acquiring bank payment processing error for payment ID {paymentId}", paymentId);
                payment.Status = Status.Declined;
            }

            await _repository.Update(paymentId, payment);

            return new PaymentResult(payment.MerchantReference, paymentId, payment.Status.ToString());
        }

        public async Task<PaymentDetails?> GetPayment(string paymentId)
        {
            var payment = await _repository.GetById(paymentId);

            return payment;
        }

        private ProcessPaymentRequestDto GetBankPaymentRequestDto(PaymentRequest paymentRequest)
        {
            return new ProcessPaymentRequestDto(
                paymentRequest.CardDetails.Number, paymentRequest.CardDetails.Cvv, paymentRequest.CardDetails.ExpiryMonth, 
                paymentRequest.CardDetails.ExpiryYear, paymentRequest.Currency, paymentRequest.Amount, paymentRequest.CardDetails.CardholderName,
                paymentRequest.BillingAddress?.Address1, paymentRequest.BillingAddress?.Address2, paymentRequest.BillingAddress?.Postcode, paymentRequest.BillingAddress?.City, paymentRequest.BillingAddress?.Country);
        }

        // This is just to simulate the acquiring bank having a different set of status codes to the payment gateway
        private Status BankStatusToStatus(int bankStatus)
        {
            switch (bankStatus)
            {
                case 100:
                    return Status.Authorised;
                case 200:
                    return Status.Declined;
                case 300:
                    return Status.Pending;
                default:
                    return Status.Declined;
            }
        }
    }
}