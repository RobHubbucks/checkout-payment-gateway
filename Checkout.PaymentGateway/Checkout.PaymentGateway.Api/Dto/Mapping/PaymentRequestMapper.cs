using Checkout.PaymentGateway.Api.Model;

namespace Checkout.PaymentGateway.Api.Dto.Mapping
{
    public class PaymentRequestMapper : IMapper<PaymentRequestDto, PaymentRequest>
    {
        public PaymentRequest? Map(PaymentRequestDto? paymentRequestDto)
        {
            if (paymentRequestDto == null)
                return null;

            var card = new Card(paymentRequestDto.CardDetails.Number, paymentRequestDto.CardDetails.Cvv,
                paymentRequestDto.CardDetails.ExpiryMonth, paymentRequestDto.CardDetails.ExpiryYear,
                paymentRequestDto.CardDetails.CardholderName);

            var address = new Address(paymentRequestDto.BillingAddress.Address1,
                paymentRequestDto.BillingAddress.Address2, paymentRequestDto.BillingAddress.Postcode,
                paymentRequestDto.BillingAddress.City, paymentRequestDto.BillingAddress.Country);

            return new PaymentRequest(paymentRequestDto.Currency, paymentRequestDto.Amount,
                paymentRequestDto.MerchantReference, card, address);
        }

        public PaymentRequestDto? Map(PaymentRequest? input)
        {
            throw new NotImplementedException();
        }
    }
}