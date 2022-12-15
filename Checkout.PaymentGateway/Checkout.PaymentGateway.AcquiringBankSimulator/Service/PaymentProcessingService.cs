using Checkout.PaymentGateway.AcquiringBankSimulator.Dto;

namespace Checkout.PaymentGateway.AcquiringBankSimulator.Service
{
    public class PaymentProcessingService : IPaymentProcessingService
    {
        private readonly IList<IBank> _banks;

        public PaymentProcessingService(IList<IBank> banks)
        {
            _banks = banks;
        }

        public Task<PaymentResponseDto> ProcessPayment(PaymentDto paymentToProcess)
        {
            var customerBank = _banks.FirstOrDefault(x => x.CustomerExists(paymentToProcess.Number));

            if (customerBank == null)
                return Task.FromResult(new PaymentResponseDto { ResponseCode = (int)ResponseCodes.Declined });

            var authorised = customerBank.MakePayment(paymentToProcess);

            return Task.FromResult(new PaymentResponseDto { ResponseCode = authorised ? (int)ResponseCodes.Authorised : (int)ResponseCodes.Declined });
        }
    }

    public enum ResponseCodes
    {
        Authorised = 100,
        Declined = 200
    }
}