using EdurexEnrollment.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdurexEnrollment.Core.DTO
{
    public class PaymentDTO
    {
        public decimal Amount { get; set; }
        public PaymentMethodEnums PaymentMethod { get; set; }
        public string BankConnectPaymenttype { get; set; }
        public int userProgramOptionId { get; set; }
        public string ReferralDiscountCode { get; set; }

    }
}