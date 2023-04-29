using System;
using System.Collections.Generic;
using System.Text;

namespace EdurexEnrollment.Core.Utilities
{
  public class PaymentStatusResponse
    {
        public string paymentRef { get; set; }
        public string AmountPaid { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
