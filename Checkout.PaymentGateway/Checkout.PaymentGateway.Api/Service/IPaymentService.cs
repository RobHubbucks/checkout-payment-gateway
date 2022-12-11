﻿using Checkout.PaymentGateway.Api.Model;

namespace Checkout.PaymentGateway.Api.Service
{
    public interface IPaymentService
    {
        Task<PaymentResult> RequestPayment(PaymentRequest paymentRequest);

        Task<PaymentDetails?> GetPayment(string paymentId);
    }
}