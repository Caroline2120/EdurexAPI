using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using EdurexEnrollment.Core.Enums;


namespace EdurexEnrollment.Core.Model
{
    public class UserPaymentHistory
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatusEnums StatusId { get; set; }
        public string PaymentReference { get; set; }
        public string TransactionReference { get; set; }
        public PaymentMethodEnums PaymentMethodId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int UserProgramOptionId { get; set; }
        public UserProgramOption UserProgramOption { get; set; }
        // public string ReferralDiscountCode { get; set; }

    }
}