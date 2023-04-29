using System;
using System.Collections.Generic;
using System.Text;

namespace EdurexEnrollment.Core.Utilities
{
   public class JwtSettings
    {
        public string Secret { get; set; }

        public string HmacKey { get; set; }
    }
}
