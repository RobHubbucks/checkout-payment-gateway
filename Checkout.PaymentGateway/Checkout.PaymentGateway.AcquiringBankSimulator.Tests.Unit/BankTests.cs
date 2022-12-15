using Checkout.PaymentGateway.AcquiringBankSimulator.Dto;
using Checkout.PaymentGateway.AcquiringBankSimulator.Model;
using Checkout.PaymentGateway.AcquiringBankSimulator.Service;

namespace Checkout.PaymentGateway.AcquiringBankSimulator.Tests.Unit
{
    [TestFixture]
    public class BankTests
    {
        private IBank _bank;
        private DateTime _today;

        [OneTimeSetUp]
        public void Setup()
        {
            _today = DateTime.Today;

            _bank = new Bank("My Bank", new List<BankAccount>
            {
                new ("SuccessfulPaymentAccount", new Card("1111222233334444", "333", _today.Month, _today.Year), new Address("1", "2", "3", "4", "5"), 10000),
                new ("UnsuccessfulPaymentAccount", new Card("4444333322221111", "222", 10, _today.Year), new Address("1", "2", "3", "4", "5"), 500),
                new ("ExpiredCardAccount", new Card("4444333322221111", "222", 10, _today.Year - 1), new Address("1", "2", "3", "4", "5"), 5000),
                new ("NoMoneyAccount", new Card("123443211234321", "111", 10, _today.Year), new Address("1", "2", "3", "4", "5"), 0)
            });
        }

        [Test]
        [TestCase("1111222233334444", true)]        
        [TestCase("4234123431231232", false)]

        public void CustomerExists_ReturnsCorrectResponse(string cardNumber, bool expectedResult)
        {
            var customerExists = _bank.CustomerExists(cardNumber);

            Assert.That(customerExists == expectedResult);
        }

        [Test]
        public void MakePayment_ReturnsTrueWhenPaymentMatchesAccountDetails()
        {
            var payment = new PaymentDto("1111222233334444", "333", _today.Month, _today.Year, "GBP", 500,
                "SuccessfulPaymentAccount", null, null, null, null, null);

            var success = _bank.MakePayment(payment);

            Assert.IsTrue(success);
        }

        [Test]
        public void MakePayment_ReturnsFalseWhenPaymentDoesNotMatchAccountDetails()
        {
            // CVV is supplied but incorrect
            var payment = new PaymentDto("435657975646543", "333", 10, _today.Year, "GBP", 500,
                "UnsuccessfulPaymentAccount", null, null, null, null, null);

            var success = _bank.MakePayment(payment);

            Assert.IsFalse(success);
        }

        [Test]
        public void MakePayment_ReturnsFalseWhenCardIsExpired()
        {
            var payment = new PaymentDto("4444333322221111", null, 10, _today.Year - 1, "GBP", 500,
                "UnsuccessfulPaymentAccount", null, null, null, null, null);

            var success = _bank.MakePayment(payment);

            Assert.IsFalse(success);
        }

        [Test]
        public void MakePayment_ReturnsFalseWhenAccountHasNoMoney()
        {
            var payment = new PaymentDto("123443211234321", null, 10, _today.Year, "GBP", 500,
                "NoMoneyAccount", null, null, null, null, null);

            var success = _bank.MakePayment(payment);

            Assert.IsFalse(success);
        }
    }
}