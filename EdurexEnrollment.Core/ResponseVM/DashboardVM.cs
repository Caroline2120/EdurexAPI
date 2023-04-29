using EdurexEnrollment.Core.DTO;
using EdurexEnrollment.Core.Enums;
using EdurexEnrollment.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdurexEnrollment.Core.ResponseVM
{
    public class DashboardVM
    {
        public string fullName { get; set; }
        //public string programName { get; set; }
        public List<ProgramRecord> Programs { get; set; }
        public List<PaymentRecord> PaymentRecord { get; set; }
       // public List<string> SubjectRecord { get; set; }
       // public List<ChoicesRecord> ChoicesRecord { get; set; }
       // public PaymentDTO paymentDTO { get; set; }
        //public string SourceView { get; set; }
        //public IEnumerable<ProgramCategory> programCategoryListz { get; set; }
        //public int programOptionId { get; set; }
        public string rCode { get; set; }
       // public List<int> SubjectIds { get; set; }
        //public string deleteSource { get; set; }
    }
    public class PaymentRecord
    {
        public string programName { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public PaymentStatusEnums Status { get; set; }
        public PaymentMethodEnums PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentRef { get; set; }

    }
    public class ChoicesRecord
    {
        public string Institution { get; set; }
        public string Course { get; set; }

    }
    public class ProgramRecord
    {
        public string ProgramOptionId { get; set; }
        public int userProgramOptionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string programPrice { get; set; }
        public string amountPaid { get; set; }
        public UserProgramStatusEnums ProgramStatus { get; set; }
        public UserProgramPaymentStatusEnums ProgramPaymentStatus { get; set; }

    }
    public class ReferralUsage
    {
        //public string rCode { get; set; }
        public List<ReferralCodeUsage> ReferralUsage2 { get; set; }

        public List<UserDiscount> Discount { get; set; }
    }
    public class ReferralCodeUsage
    {
        public string Fullname { get; set; }
        public string Program { get; set; }
        public string AmountPaid { get; set; }
        public string Earnings { get; set; }
        public string DateRegistered { get; set; }
    }
    public class DiscountHisto
    {
        public string Fullname { get; set; }
        public string Program { get; set; }
        public string ProgramFee { get; set; }
        public string Earnings { get; set; }
        public string DateRegistered { get; set; }
    }
    public class ListDiscountHisto
    {
        public string code { get; set; }
        public List<DiscountHisto> DiscountHisto { get; set; }

    }
}
