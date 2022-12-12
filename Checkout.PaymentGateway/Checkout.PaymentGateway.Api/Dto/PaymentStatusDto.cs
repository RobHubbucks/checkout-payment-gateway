namespace Checkout.PaymentGateway.Api.Dto
{
    public class PaymentStatusDto
    {
        public PaymentStatusDto() { }

        public PaymentStatusDto(string status)
        {
            Status = status;
        }

        public string Status { get; set; } = null!;
    }
}