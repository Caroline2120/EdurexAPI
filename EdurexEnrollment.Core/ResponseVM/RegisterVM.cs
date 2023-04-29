using EdurexEnrollment.Core.DTO;
using EdurexEnrollment.Core.Model;
using EdurexEnrollment.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdurexEnrollment.Core.ResponseVM
{
    public class RegisterVM
    {
       // public string refCode { get; set; }
        public RegisterDTO registerz { get; set; }
        public IEnumerable<ProgramCategory> programCategoryListz { get; set; }
        public IEnumerable<CountryDetails> countryListz { get; set; }
        public IEnumerable<StateDetails> nigeriaStatesListz { get; set; }
        public string successJSCall { get; set; }
    }
}