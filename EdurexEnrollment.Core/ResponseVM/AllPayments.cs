using EdurexEnrollment.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdurexEnrollment.Core.ResponseVM
{
    public class AllPayments
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public PaymentStatusEnums StatusId { get; set; }
        public string PaymentReference { get; set; }
        public string TransactionReference { get; set; }
        public PaymentMethodEnums PaymentMethodId { get; set; }
        public string PaymentDate { get; set; }
        public string Program { get; set; }
        public string Payee { get; set; }
    }
}

