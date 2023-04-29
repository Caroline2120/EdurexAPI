using EdurexEnrollment.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdurexEnrollment.Core.DTO
{
    public class EnrollmentDetails
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string ReferralCode { get; set; }
        public string Address { get; set; }
        public UserStatusEnums Status { get; set; }
        public string RegisteredDate { get; set; }
        public string Gender { get; set; }
        public UserRolesEnums Role { get; set; }
    }
}
