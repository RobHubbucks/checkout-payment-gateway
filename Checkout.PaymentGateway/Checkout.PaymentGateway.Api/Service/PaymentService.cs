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

        public async Task<PaymentResult> RequestPayment(PaymentDetails paymentDetails)
        {
            await _repository.Add(paymentDetails.Id, paymentDetails);

            try
            {
                var bankResult = await _acquiringBankService.ProcessPayment(GetBankPaymentRequestDto(paymentDetails));

                paymentDetails.Status = bankResult == null ? Status.Declined : BankStatusToStatus(bankResult.ResponseCode);
            }
            catch (PaymentProcessingException e)
            {
                _logger.LogError(exception: e, message: "Acquiring bank payment processing error for payment ID {paymentId}", paymentDetails.Id);
                paymentDetails.Status = Status.Declined;
            }

            await _repository.Update(paymentDetails.Id, paymentDetails);

            return new PaymentResult(paymentDetails.MerchantReference, paymentDetails.Id, paymentDetails.Status.ToString());
        }

        public async Task<PaymentDetails?> GetPayment(string paymentId)
        {
            var payment = await _repository.GetById(paymentId);

            return payment;
        }

        private ProcessPaymentRequestDto GetBankPaymentRequestDto(PaymentDetails paymentDetails)
        {
            return new ProcessPaymentRequestDto(
                paymentDetails.CardDetails.Number, paymentDetails.CardDetails.Cvv, paymentDetails.CardDetails.ExpiryMonth, 
                paymentDetails.CardDetails.ExpiryYear, paymentDetails.Currency, paymentDetails.Amount, paymentDetails.CardDetails.CardholderName,
                paymentDetails.BillingAddress?.Address1, paymentDetails.BillingAddress?.Address2, paymentDetails.BillingAddress?.Postcode, paymentDetails.BillingAddress?.City, paymentDetails.BillingAddress?.Country);
        }

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