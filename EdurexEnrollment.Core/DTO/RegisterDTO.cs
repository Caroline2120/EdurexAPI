using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EdurexEnrollment.Core.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string AlternatePhone { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public string Address { get; set; }
        //public int StateId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int ProgramId { get; set; }

        public int PaymentMethod { get; set; }
        public List<int> SubjectIds { get; set; }
        public Choices Choices { get; set; }

        public string paymentDeposit { get; set; }

        public string Gender { get; set; }
        public string referralCode { get; set; }
        public float Amount { get; set; }
        public int ProgramOptionId { get; set; }
        [Required]
        public string heardAboutUs { get; set; }
    }
    public class Choices
    {
        public int FirstInstitution { get; set; }
        public int SecondInstitution { get; set; }
        public int FirstCourse { get; set; }
        public int SecondCourse { get; set; }

    }

}
