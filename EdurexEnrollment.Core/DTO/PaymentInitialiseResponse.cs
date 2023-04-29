using System;
using System.Collections.Generic;
using System.Text;

namespace EdurexEnrollment.Core.DTO
{
    public class PaymentInitialiseResponse
    {
        public string paymentRef { get; set; }
        public string checkOutURL { get; set; }
        public string paymentMethod { get; set; }

        // public string errorMessage { get; set; }

    }
}
