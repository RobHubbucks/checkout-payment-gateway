﻿using System.ComponentModel.DataAnnotations;
using Checkout.PaymentGateway.Api.Dto.Validation;

namespace Checkout.PaymentGateway.Api.Dto
{
    public abstract class CardDto
    {
        protected CardDto(int expiryMonth, int expiryYear, string cardholderName)
        {
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            CardholderName = cardholderName;
        }

        protected CardDto() { }

        [Required]
        [PaymentExpiryDateValidator]
        public int ExpiryMonth { get; set; }

        [Required]
        [PaymentExpiryDateValidator]
        public int ExpiryYear { get; set; }

        [Required]
        public string CardholderName { get; set; } = null!;
    }
}