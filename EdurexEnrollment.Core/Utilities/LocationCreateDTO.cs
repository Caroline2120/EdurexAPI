using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EdurexEnrollment.Core.Utilities
{
    public class LocationCreateDTO
    {
        [Required]
        public int cityId { get; set; }

        [Required]
        public string streetNumber { get; set; }

        public string area { get; set; }

        public string estate { get; set; }

        public string streetName { get; set; }
    }
}
