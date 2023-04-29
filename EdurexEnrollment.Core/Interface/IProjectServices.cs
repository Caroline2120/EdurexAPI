using EdurexEnrollment.Core.DTO;
using EdurexEnrollment.Core.Enums;
using EdurexEnrollment.Core.Model;
using EdurexEnrollment.Core.ResponseVM;
using EdurexEnrollment.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdurexEnrollment.Core.Interface
{
    public interface IProjectServices
    {
        Task<GeneralResponse<RegisterVM>> GetRegister();
        Task<GeneralResponse<List<ProgramCategory>>> GetProgramCategories();
        Task<GeneralResponse<List<Programs>>> GetProgramsByCategoryId(int CategoryId);
        Task<GeneralResponse<List<ProgramOptions>>> GetProgramOptionsByProgramId(int ProgramId);
        Task<GeneralResponse<List<Subjects>>> GetProgramSubjectssByOptionId(int OptionId);
        Task<GeneralResponse<List<countriesbyCountry>>> GetCountries();
        Task<GeneralResponse<List<states>>> GetStatesByCountryId(int CountryId);
        Task<GeneralResponse<List<Utilities.Cities>>> GetCitiesByStateId(int StateId);
        Task<GeneralResponse<List<Streets>>> GetStreetsByCityId(int CityId);
        Task<GeneralResponse<List<Institutions>>> GetInstitutions();
        Task<GeneralResponse<List<Courses>>> GetCourses();
        //Task<GeneralResponse<string> GetPriceByProgramId(int ProgramId);
        Task<GeneralResponse<ProgramOptionPriceVM>> GetPriceByProgramOptionId(int ProgramOptionId);
        Task<GeneralResponse<PaymentInitialiseResponse>> InitializeCardPayment(PaymentDTO dto);
        Task<GeneralResponse<PaymentInitialiseResponse>> InitializeAccountTransferPayment(PaymentDTO dto);
        Task<GeneralResponse<PaymentInitialiseResponse>> InitializeBankCOnnectPayment(PaymentDTO dto);
        Task<GeneralResponse<PaymentInitialiseResponse>> InitializeOfflinePayment(PaymentDTO dto);

        Task<GeneralResponse<PaymentInitialiseResponse>> RegisterUser(RegisterDTO dto);
        Task<GeneralResponse<AuthenticationToken>> Login(LoginDTO dto);
        Task<GeneralResponse<DashboardVM>> DashboardRe(string userId);

        Task<GeneralResponse<PaymentStatusResponse>> QueryBankCOnnectPayment(string PaymentRef);
        Task<GeneralResponse<List<ProgramRecord>>> UserPrograms(string userId);
        Task<GeneralResponse<List<PaymentRecord>>> UserPaymentHistories(string userId);
        Task<GeneralResponse<List<UserCertificates>>> UserCertificates(string userId);
        Task<GeneralResponse<List<ProgramRecord>>> UserNewProgramOption(int pOptionId, string userId);
       // Task<GeneralResponse<List<ProgramCategory>>> GetProgramCat();
        Task<GeneralResponse<List<ProgramRecord>>> DeleteUserProgramOption(int Id, string userId);
        Task<GeneralResponse<List<ReferralCodeUsage>>> UserReferralCodeUsage(string userId);
        Task<GeneralResponse<List<UserDiscount>>> UserDiscount(string userId);

        Task<GeneralResponse<ListDiscountHisto>> DiscountHistory(string code, string userId);
        Task<GeneralResponse<float>> GetDiscountRate(string code, string userId);

    }
}