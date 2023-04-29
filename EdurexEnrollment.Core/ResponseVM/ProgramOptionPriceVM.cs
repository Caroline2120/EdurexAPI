using System;
using System.Collections.Generic;
using System.Text;

namespace EdurexEnrollment.Core.ResponseVM
{
   public class ProgramOptionPriceVM
    {
        public string PriceNGN { get; set; }
        public string DepositNGN { get; set; }
        public string Duration { get; set; }
        public float USD { get; set; }
        public string DepositUSD { get; set; }
        public int? maxSubjects { get; set; }
    }
}
