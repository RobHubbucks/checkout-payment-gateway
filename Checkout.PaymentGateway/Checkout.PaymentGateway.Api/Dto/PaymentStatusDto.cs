namespace Checkout.PaymentGateway.Api.Dto
{
    /// <summary>
    /// Represents the status of a payment
    /// </summary>
    public class PaymentStatusDto
    {
        /// <summary>
        /// 
        /// </summary>
        public PaymentStatusDto() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        public PaymentStatusDto(string status)
        {
            Status = status;
        }

        /// <summary>
        /// Authorised, Declined or Pending
        /// </summary>
        public string Status { get; set; } = null!;
    }
}