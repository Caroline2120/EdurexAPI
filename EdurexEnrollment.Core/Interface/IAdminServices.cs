using EdurexEnrollment.Core.DTO;
using EdurexEnrollment.Core.Model;
using EdurexEnrollment.Core.ResponseVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdurexEnrollment.Core.Interface
{
    public interface IAdminServices
    {
        Task<DashboardVM> DashboardRe(string email);
        Task<List<EnrollmentDetails>> Enrollment();
        Task<EnrollmentDetails> SingleEnrollment(string userEmail);
        Task<List<AllPayments>> Paymemts();
        Task<List<AllPayments>> PaymentbyUser(string userEmail);
        Task<AllPayments> SinglePayment(int paymentId);
        Task<string> ConfirmSinglePayment(int paymentId);
        Task<string> Login(LoginDTO dto);
        Task<List<ProgramCategory>> ProgramCategories();
        Task<List<ProgramCategory>> AddProgramCategory(ProgramCategory dto);
        Task<List<Programs>> Programs();
        Task<List<Programs>> AddPrograms(Programs dto);
        Task<List<ProgramOptions>> ProgramOptions();
        Task<List<ProgramOptions>> AddProgramOption(ProgramOptions dto);
        Task<List<programFees>> ProgramFees();
        //Task<programFees> ProgramFees(int programId);
    }
}
