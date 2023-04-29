using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EdurexEnrollment.Core.Model
{
    public class UserReferred
    {
        [Key]
        public int Id { get; set; }
        public string ReferralId { get; set; }
        public Users Referral { get; set; }
        public string PaymentRef { get; set; }
        public int ReferredUserProgramOptionId { get; set; }
        public UserProgramOption ReferredUserProgramOption { get; set; }
        public float Earnings { get; set; }
    }
}