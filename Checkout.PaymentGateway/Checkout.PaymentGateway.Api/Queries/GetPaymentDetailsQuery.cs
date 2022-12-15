namespace Checkout.PaymentGateway.Api.Queries
{
    public class GetPaymentDetailsQuery
    {
        public GetPaymentDetailsQuery(string paymentId)
        {
            PaymentId = paymentId;
        }

        public string PaymentId { get; set; }
    }
}