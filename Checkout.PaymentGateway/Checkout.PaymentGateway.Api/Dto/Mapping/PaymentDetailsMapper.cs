using Checkout.PaymentGateway.Api.Extensions;
using Checkout.PaymentGateway.Api.Model;

namespace Checkout.PaymentGateway.Api.Dto.Mapping
{
    public class PaymentDetailsMapper : IMapper<PaymentDetailsDto, PaymentDetails>
    {
        public PaymentDetails Map(PaymentDetailsDto? input)
        {
            throw new NotImplementedException();
        }

        public PaymentDetailsDto? Map(PaymentDetails? paymentDetails)
        {
            if (paymentDetails == null)
                return null;

            var cardDetails = new MaskedCardDto(paymentDetails.CardDetails.Number.MaskCardNumber(), paymentDetails.CardDetails.ExpiryMonth, paymentDetails.CardDetails.ExpiryYear, paymentDetails.CardDetails.CardholderName);

            return new PaymentDetailsDto(paymentDetails.PaymentId, paymentDetails.MerchantReference, cardDetails,
                paymentDetails.Currency, paymentDetails.Amount, new PaymentStatusDto(paymentDetails.Status.ToString()));
        }

    }
}