using System.ComponentModel.DataAnnotations;
using Checkout.PaymentGateway.Api.Dto;
using Checkout.PaymentGateway.Api.Dto.Validation;

namespace Checkout.PaymentGateway.Api.Tests.Unit.Dto.Validation
{
    [TestFixture]
    public class PaymentExpiryDateValidatorTests
    {
        [Test]
        [TestCase(1, 1, true)]
        [TestCase(-1, 0, false)]
        [TestCase(0, 0, true)]
        [TestCase(0, 1, true)]
        [TestCase(0, -1, false)]
        public void TestExpiryDateValid(int expiryYearOffset, int expiryMonthOffset, bool valid)
        {
            var offsetDate = DateTime.Today.AddYears(expiryYearOffset).AddMonths(expiryMonthOffset);

            var validator = new PaymentExpiryDateValidator();

            var card = new UnMaskedCardDto { ExpiryMonth = offsetDate.Month, ExpiryYear = offsetDate.Year };

            var result = validator.GetValidationResult(card, new ValidationContext(card));

            Assert.IsTrue(valid ? result == ValidationResult.Success : result != ValidationResult.Success);
        }
    }
}