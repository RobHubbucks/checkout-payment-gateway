using Checkout.PaymentGateway.Api.Model;
using Checkout.PaymentGateway.Api.Queries;
using Checkout.PaymentGateway.Api.Service;
using Moq;

namespace Checkout.PaymentGateway.Api.Tests.Unit.Queries
{
    [TestFixture]
    public class GetPaymentDetailsQueryHandlerTests
    {
        [Test]
        public async Task Execute_HitsCriticalPath()
        {
            var paymentService = new Mock<IPaymentService>();

            var query = new GetPaymentDetailsQuery("123");

            paymentService.Setup(x => x.GetPayment(query.PaymentId)).ReturnsAsync(new PaymentDetails(query.PaymentId,
                "", new Card("", "", 1, 2023, ""), "", 1, Status.Authorised, null));

            var queryHandler = new GetPaymentDetailsQueryHandler(paymentService.Object);

            var result = await queryHandler.Execute(query);

            paymentService.Verify(x => x.GetPayment(query.PaymentId), Times.Once);
            Assert.IsNotNull(result);
            Assert.That(result?.Id, Is.EqualTo(query.PaymentId));
        }
    }
}