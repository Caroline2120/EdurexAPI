using System;
using System.Collections.Generic;
using System.Text;

namespace EdurexEnrollment.Core.DTO
{
    public class programFees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float PriceNGN { get; set; }
        public float DepositNGN { get; set; }
        public float PriceUSD { get; set; }
        public float DepositUSD { get; set; }
    }
}