using Checkout.PaymentGateway.Api.Commands;
using Checkout.PaymentGateway.Api.Dto.Mapping;
using Checkout.PaymentGateway.Api.Dto;
using Checkout.PaymentGateway.Api.Model;

namespace Checkout.PaymentGateway.Api.Tests.Unit.Dto.Mapping
{
    [TestFixture]
    public class PaymentResultMapperTests
    {
        private readonly IMapper<PaymentResultDto, PaymentResult> _mapper;

        public PaymentResultMapperTests()
        {
            _mapper = new PaymentResultMapper();
        }

        [Test]
        public void Map_ToDtoNullInputReturnsNull()
        {
            var result = _mapper.Map((PaymentResult?)null);

            Assert.IsNull(result);
        }

        [Test]
        public void Map_ToDtoMapsFieldsCorrectly()
        {
            var input = new PaymentResult("abc", "123", Status.Authorised.ToString());

            var result = _mapper.Map(input);

            Assert.IsNotNull(result);
            Assert.That(result?.PaymentId, Is.EqualTo(input.PaymentId));
            Assert.That(result?.MerchantReference, Is.EqualTo(input.MerchantReference));
            Assert.That(result?.Status.Status, Is.EqualTo(input.Status));
        }
    }
}