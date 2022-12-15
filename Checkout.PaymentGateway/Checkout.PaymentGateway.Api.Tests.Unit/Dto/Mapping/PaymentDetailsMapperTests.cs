using Checkout.PaymentGateway.Api.Dto;
using Checkout.PaymentGateway.Api.Dto.Mapping;
using Checkout.PaymentGateway.Api.Model;

namespace Checkout.PaymentGateway.Api.Tests.Unit.Dto.Mapping
{
    [TestFixture]
    public class PaymentDetailsMapperTests
    {
        private readonly IMapper<PaymentDetailsDto, PaymentDetails> _mapper;

        public PaymentDetailsMapperTests()
        {
            _mapper = new PaymentDetailsMapper();
        }

        [Test]
        public void Map_ToDtoNullInputReturnsNull()
        {
            var result = _mapper.Map((PaymentDetails?)null);

            Assert.IsNull(result);
        }

        [Test]
        public void Map_ToDtoMapsFieldsCorrectly()
        {
            var input = new PaymentDetails("1", "2", new Card("43211234", "321", 1, 2025, "Rob"), "GBP", 100, Status.Authorised, null);

            var result = _mapper.Map(input);

            Assert.IsNotNull(result);
            Assert.That(result?.Amount, Is.EqualTo(input.Amount));
            Assert.That(result?.Currency, Is.EqualTo(input.Currency));
            Assert.That(result?.MerchantReference, Is.EqualTo(input.MerchantReference));
            Assert.That(result?.PaymentId, Is.EqualTo(input.Id));
            Assert.That(result?.CardDetails.Last4Digits, Is.EqualTo("1234"));
            Assert.That(result?.CardDetails.CardholderName, Is.EqualTo(input.CardDetails.CardholderName));
            Assert.That(result?.CardDetails.ExpiryMonth, Is.EqualTo(input.CardDetails.ExpiryMonth));
            Assert.That(result?.CardDetails.ExpiryYear, Is.EqualTo(input.CardDetails.ExpiryYear));
            Assert.That(result?.Status, Is.EqualTo(input.Status.ToString()));
        }
    }
}
