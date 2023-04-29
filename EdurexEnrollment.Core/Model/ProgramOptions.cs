using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EdurexEnrollment.Core.Model
{
    public class ProgramOptions
    {
        [Key]
        public int Id { get; set; }
        public string ProgramOptionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float PriceNGN { get; set; }
        public float DepositNGN { get; set; }
        public float PriceUSD { get; set; }
        public float DepositUSD { get; set; }
        public int ProgramId { get; set; }
        public Programs Program { get; set; }
        public string Duration { get; set; }
        public DateTime StartDate { get; set; }
        public int? MaxSubjectSelection { get; set; }
    }
}
