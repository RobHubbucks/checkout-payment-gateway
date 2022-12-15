using Checkout.PaymentGateway.Api.Commands;
using Checkout.PaymentGateway.Api.Extensions;
using Checkout.PaymentGateway.Api.Model;

namespace Checkout.PaymentGateway.Api.Dto.Mapping
{
    public class PaymentRequestMapper : IMapper<PaymentRequestDto, PaymentRequestCommand>
    {
        public PaymentRequestCommand? Map(PaymentRequestDto? paymentRequestDto)
        {
            if (paymentRequestDto == null)
                return null;

            var card = new Card(paymentRequestDto.CardDetails.Number.SanitiseCardNumber(), paymentRequestDto.CardDetails.Cvv,
                paymentRequestDto.CardDetails.ExpiryMonth, paymentRequestDto.CardDetails.ExpiryYear,
                paymentRequestDto.CardDetails.CardholderName);

            var address = GetAddress(paymentRequestDto.BillingAddress);

            return new PaymentRequestCommand(paymentRequestDto.Currency, paymentRequestDto.Amount,
                paymentRequestDto.MerchantReference, card, address);
        }

        private Address? GetAddress(AddressDto? addressDto)
        {
            if (addressDto == null)
                return null;

            return new Address(addressDto.Address1, addressDto.Address2,
                addressDto.Postcode, addressDto.City,
                addressDto.Country);
        }

        public PaymentRequestDto Map(PaymentRequestCommand? input)
        {
            throw new NotImplementedException();
        }
    }
}