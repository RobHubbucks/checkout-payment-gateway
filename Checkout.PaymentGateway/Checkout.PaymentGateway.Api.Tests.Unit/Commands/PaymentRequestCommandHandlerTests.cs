using Checkout.PaymentGateway.Api.Commands;
using Checkout.PaymentGateway.Api.Model;
using Checkout.PaymentGateway.Api.Service;
using Moq;

namespace Checkout.PaymentGateway.Api.Tests.Unit.Commands
{
    [TestFixture]
    public class PaymentRequestCommandHandlerTests
    {
        [Test]
        public async Task Handle_HitsCriticalPath()
        {
            var paymentService = new Mock<IPaymentService>();

            var command = new PaymentRequestCommand("GBP", 100, "123", new Card("1234", "321", 10, 2025, "A"), null);

            paymentService.Setup(x => x.RequestPayment(It.IsAny<PaymentDetails>()))
                .ReturnsAsync(new PaymentResult(string.Empty, string.Empty, string.Empty));

            var commandHandler = new PaymentRequestCommandHandler(paymentService.Object);

            var result = await commandHandler.Handle(command);

            Assert.IsNotNull(result);
            paymentService.Verify(x => x.RequestPayment(It.IsAny<PaymentDetails>()), Times.Once);
        }
    }
}