namespace Checkout.PaymentGateway.Api.Dto
{
    public class MaskedCardDto : CardDto
    {
        public MaskedCardDto() { }

        public MaskedCardDto(string last4Digits, int expiryMonth, int expiryYear, string? cardholderName)
        :base(expiryMonth, expiryYear, cardholderName)
        {
            Last4Digits = last4Digits;
        }

        public string Last4Digits { get; set; } = null!;
    }
}