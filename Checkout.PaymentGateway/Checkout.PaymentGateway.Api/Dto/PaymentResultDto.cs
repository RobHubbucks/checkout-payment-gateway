namespace Checkout.PaymentGateway.Api.Dto
{
    public class PaymentResultDto
    {
        public PaymentResultDto() {}

        public PaymentResultDto(string merchantReference, string paymentId, int statusCode, string status)
        {
            MerchantReference = merchantReference;
            PaymentId = paymentId;
            Status = new PaymentStatusDto(statusCode, status);
        }

        public string MerchantReference { get; set; } = null!;

        public string PaymentId { get; set; } = null!;

        public PaymentStatusDto Status { get; set; } = null!;
    }
}