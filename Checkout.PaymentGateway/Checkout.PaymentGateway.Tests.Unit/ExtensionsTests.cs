using Checkout.PaymentGateway.Api.Extensions;

namespace Checkout.PaymentGateway.Tests.Unit
{
    [TestFixture]
    public class ExtensionsTests
    {
        [Test]
        [TestCase("1111222233334444", "4444")]
        public void MaskCardNumber(string cardNumber, string expectedMask)
        {
            var masked = cardNumber.MaskCardNumber();

            Assert.That(masked, Is.EqualTo(expectedMask));
        }
    }
}