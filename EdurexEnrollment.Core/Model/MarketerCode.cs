using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EdurexEnrollment.Core.Model
{
    public class MarketerCode
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}

