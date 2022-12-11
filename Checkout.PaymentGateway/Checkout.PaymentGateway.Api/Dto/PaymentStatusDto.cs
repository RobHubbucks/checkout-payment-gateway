namespace Checkout.PaymentGateway.Api.Dto
{
    public class PaymentStatusDto
    {
        public PaymentStatusDto() { }

        public PaymentStatusDto(int code, string status)
        {
            Code = code;
            Status = status;
        }

        public int Code { get; set; }

        public string Status { get; set; } = null!;
    }
}