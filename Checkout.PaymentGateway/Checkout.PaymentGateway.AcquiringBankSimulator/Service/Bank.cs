using Checkout.PaymentGateway.AcquiringBankSimulator.Dto;
using Checkout.PaymentGateway.AcquiringBankSimulator.Model;

namespace Checkout.PaymentGateway.AcquiringBankSimulator.Service
{
    /// <summary>
    /// Represents a remote customer bank
    /// </summary>
    public class Bank : IBank
    {
        private readonly IList<BankAccount> _accounts;

        public Bank(string name, IList<BankAccount> accounts)
        {
            Name = name;
            _accounts = accounts;
        }

        public string Name { get; set; }

        public bool CustomerExists(string cardNumber)
        {
            return GetAccountByCardNumber(cardNumber) != null;
        }

        public bool MakePayment(PaymentDto payment)
        {
            if (!CanProcessPayment(payment))
                return false;

            var account = GetAccountByCardNumber(payment.Number);

            if (account == null)
                return false;

            var balance = account.Balance;

            var newBalance = balance - payment.Amount;

            if (newBalance > 0)
                account.Balance = newBalance;

            return true;
        }

        private bool CanProcessPayment(PaymentDto payment)
        {
            var account = GetAccountByCardNumber(payment.Number);

            if (account == null)
                return false;

            if (!ValidateCard(payment, account.Card))
                return false;

            if(!string.IsNullOrWhiteSpace(payment.CardholderName) &&
               !payment.CardholderName.Equals(account.Name))
                return false;

            if (!ValidateBillingAddress(payment.BillingAddress, account.BillingAddress))
                return false;

            return true;
        }

        private BankAccount? GetAccountByCardNumber(string cardNumber)
        {
            return _accounts.FirstOrDefault(x => x.Card.Number.Equals(cardNumber));
        }

        private bool ValidateCard(PaymentDto payment, Card accountCard)
        {
            var equals = payment.Number.Equals(accountCard.Number) &&
                         payment.ExpiryYear.Equals(accountCard.ExpiryYear) &&
                         payment.ExpiryMonth.Equals(accountCard.ExpiryMonth);
            
            if (!equals)
                return false;

            if (!ValidateExpiryDate(payment.ExpiryYear, payment.ExpiryMonth))
                return false;

            if (!string.IsNullOrWhiteSpace(payment.Cvv))
                return payment.Cvv.Equals(accountCard.Cvv);

            return true;
        }

        private bool ValidateExpiryDate(int expiryYear, int expiryMonth)
        {
            var today = DateTime.Today;

            if (expiryYear < today.Year)
                return false;

            if (expiryMonth < 1 || expiryMonth > 12)
                return false;

            if (expiryYear == today.Year &&
                expiryMonth < today.Month)
                return false;

            return true;
        }

        private bool ValidateBillingAddress(AddressDto? address, Address accountAddress)
        {
            // Address is optional for a payment
            if (address == null)
                return true;

            // But if it exists we should validate it
            return address.Address1 == accountAddress.Address1 &&
                   address.Address2 == accountAddress.Address2 &&
                   address.Postcode == accountAddress.Postcode &&
                   address.City == accountAddress.City &&
                   address.Country == accountAddress.Country;
        }
    }
}