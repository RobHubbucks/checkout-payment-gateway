using System.ComponentModel.DataAnnotations;
using Checkout.PaymentGateway.Api.Dto;
using Checkout.PaymentGateway.Api.Dto.Validation;

namespace Checkout.PaymentGateway.Api.Tests.Unit.Dto.Validation
{
    [TestFixture]
    public class PaymentAmountValidatorTests
    {
        [Test]
        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(-1, false)]
        public void TestPaymentAmount(decimal amount, bool valid)
        {
            var validator = new PaymentAmountValidator();

            var paymentRequest = new PaymentRequestDto { Amount = amount };

            var result = validator.GetValidationResult(amount, new ValidationContext(paymentRequest));

            Assert.IsTrue(valid ? result == ValidationResult.Success : result != ValidationResult.Success);
        }
    }
}