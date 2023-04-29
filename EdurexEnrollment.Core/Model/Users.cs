using EdurexEnrollment.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EdurexEnrollment.Core.Model
{
    public class Users : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth"), Required]
        public DateTime DateOfBirth { get; set; }
        public string ReferralCode { get; set; }
        public int CityId { get; set; }
        // public int City { get; set; }
        public string Address { get; set; }
        public UserStatusEnums Status { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Registered Date"), Required]
        public DateTime RegisteredDate { get; set; }
        public string Gender { get; set; }
        public string PIN { get; set; }
        public string AlternatePhone { get; set; }
        public string UserId { get; set; }
        public string heardAboutUs { get; set; }
        public UserRolesEnums Role { get; set; }

    }
}
