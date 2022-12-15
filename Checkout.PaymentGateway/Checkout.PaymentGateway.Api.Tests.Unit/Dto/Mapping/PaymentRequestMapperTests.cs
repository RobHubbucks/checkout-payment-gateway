using Checkout.PaymentGateway.Api.Dto.Mapping;
using Checkout.PaymentGateway.Api.Dto;
using Checkout.PaymentGateway.Api.Commands;

namespace Checkout.PaymentGateway.Api.Tests.Unit.Dto.Mapping
{
    public class PaymentRequestMapperTests
    {
        private readonly IMapper<PaymentRequestDto, PaymentRequestCommand> _mapper;

        public PaymentRequestMapperTests()
        {
            _mapper = new PaymentRequestMapper();
        }

        [Test]
        public void Map_ToDtoNullInputReturnsNull()
        {
            var result = _mapper.Map((PaymentRequestDto?)null);

            Assert.IsNull(result);
        }

        [Test]
        public void Map_ToDtoMapsFieldsCorrectly()
        {
            var input = new PaymentRequestDto
            {
                Amount = 100,
                Currency = "GBP",
                MerchantReference = "abc123",
                CardDetails = new UnMaskedCardDto
                {
                    CardholderName = "AAA",
                    Cvv = "123",
                    ExpiryMonth = 5,
                    ExpiryYear = 2023,
                    Number = "1234567898765432"
                },
                BillingAddress = new AddressDto("A", "B", "C", "D", "E")
            };

            var result = _mapper.Map(input);

            Assert.IsNotNull(result);
            Assert.That(result?.Amount, Is.EqualTo(input.Amount));
            Assert.That(result?.Currency, Is.EqualTo(input.Currency));
            Assert.That(result?.MerchantReference, Is.EqualTo(input.MerchantReference));
            Assert.That(result?.CardDetails.Number, Is.EqualTo(input.CardDetails.Number));
            Assert.That(result?.CardDetails.Cvv, Is.EqualTo(input.CardDetails.Cvv));
            Assert.That(result?.CardDetails.CardholderName, Is.EqualTo(input.CardDetails.CardholderName));
            Assert.That(result?.CardDetails.ExpiryMonth, Is.EqualTo(input.CardDetails.ExpiryMonth));
            Assert.That(result?.CardDetails.ExpiryYear, Is.EqualTo(input.CardDetails.ExpiryYear));
            Assert.That(result?.BillingAddress?.Address1, Is.EqualTo(input.BillingAddress.Address1));
            Assert.That(result?.BillingAddress?.Address2, Is.EqualTo(input.BillingAddress.Address2));
            Assert.That(result?.BillingAddress?.Postcode, Is.EqualTo(input.BillingAddress.Postcode));
            Assert.That(result?.BillingAddress?.City, Is.EqualTo(input.BillingAddress.City));
            Assert.That(result?.BillingAddress?.Country, Is.EqualTo(input.BillingAddress.Country));
        }
    }
}
