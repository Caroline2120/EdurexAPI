using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EdurexEnrollment.Core.Model
{
    public class DiscountUsageHistory
    {
        [Key]
        public int Id { get; set; }
        public int UserDiscountId { get; set; }
        public UserDiscount UserDiscount { get; set; }
        public int UsedByProgramOptionId { get; set; }
        public UserProgramOption UsedByProgramOption { get; set; }
        public float Earnings { get; set; }

    }
}

