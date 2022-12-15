using Checkout.PaymentGateway.Api.Dto.AcquiringBank;
using Checkout.PaymentGateway.Api.Model;
using Checkout.PaymentGateway.Api.Service;
using Checkout.PaymentGateway.Api.Service.AcquiringBank;
using Checkout.PaymentGateway.DataAccess;
using Microsoft.Extensions.Logging;
using Moq;

namespace Checkout.PaymentGateway.Api.Tests.Unit.Service
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private Mock<IRepository<string, PaymentDetails>> _repository;
        private Mock<IAcquiringBankService> _acquiringBankService;
        private Mock<ILogger<PaymentService>> _logger;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IRepository<string, PaymentDetails>>();
            _acquiringBankService = new Mock<IAcquiringBankService>();
            _logger = new Mock<ILogger<PaymentService>>();
        }

        [Test]
        public async Task RequestPayment_SuccessfullyRequestsPayment()
        {
            _repository.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<PaymentDetails>()));
            _repository.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<PaymentDetails>()));

            _acquiringBankService.Setup(x => x.ProcessPayment(It.IsAny<ProcessPaymentRequestDto>()))
                .ReturnsAsync(new ProcessPaymentResponseDto { ResponseCode = 100 });

            var logger = new Mock<ILogger<PaymentService>>();

            var paymentService = new PaymentService(_repository.Object, _acquiringBankService.Object, logger.Object);

            var payment = GetPaymentDetails();

            var result = await paymentService.RequestPayment(payment);

            AssertPaymentRequestResult(result, Status.Authorised.ToString());
        }

        [Test]
        public async Task RequestPayment_HandlesProcessingException()
        {
            _repository.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<PaymentDetails>()));
            _repository.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<PaymentDetails>()));

            _acquiringBankService.Setup(x => x.ProcessPayment(It.IsAny<ProcessPaymentRequestDto>()))
                .Throws(new PaymentProcessingException(null));

            var paymentService = new PaymentService(_repository.Object, _acquiringBankService.Object, _logger.Object);

            var payment = GetPaymentDetails();

            var result = await paymentService.RequestPayment(payment);

            AssertPaymentRequestResult(result, Status.Declined.ToString());
        }

        private void AssertPaymentRequestResult(PaymentResult result, string expectedStatus)
        {
            Assert.IsNotNull(result);
            Assert.That(result.Status, Is.EqualTo(expectedStatus));

            _repository.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<PaymentDetails>()), Times.Once);
            _repository.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<PaymentDetails>()), Times.Once);
            _acquiringBankService.Verify(x => x.ProcessPayment(It.IsAny<ProcessPaymentRequestDto>()), Times.Once);
        }

        private PaymentDetails GetPaymentDetails()
        {
            return new PaymentDetails("1", "2", new Card("1234", "321", 10, 2025, "C"), "GBP", 100,
                Status.Pending, null);
        }

        [Test]
        public async Task GetPayment_ReturnsPayment()
        {
            var id = "1";

            _repository.Setup(x => x.GetById(id)).ReturnsAsync(GetPaymentDetails);

            var paymentService = new PaymentService(_repository.Object, _acquiringBankService.Object, _logger.Object);

            var payment = await paymentService.GetPayment(id);

            Assert.IsNotNull(payment);
            Assert.That(payment?.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task GetPayment_ReturnsNullIfRepositoryIsEmpty()
        {
            _repository.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync((PaymentDetails?)null);

            var paymentService = new PaymentService(_repository.Object, _acquiringBankService.Object, _logger.Object);

            var payment = await paymentService.GetPayment("123");

            Assert.IsNull(payment);
        }
    }
}