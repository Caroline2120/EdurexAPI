using EdurexEnrollment.Core.DTO;
using EdurexEnrollment.Core.Enums;
using EdurexEnrollment.Core.Interface;
using EdurexEnrollment.Core.Model;
using EdurexEnrollment.Core.ResponseVM;
using EdurexEnrollment.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdurexEntollment.Domain.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly IAddressServices _addressServices;

        public AdminServices(ApplicationDbContext context, UserManager<Users> userManager, IAddressServices addressServices)
        {
            _context = context;
            _userManager = userManager;
            _addressServices = addressServices;
        }
        public async Task<DashboardVM> DashboardRe(string email)
        {

            var existingUser = await _userManager.FindByEmailAsync(email);


            if (existingUser != null)
            {
                var result = new DashboardVM();
                result.fullName = existingUser.FirstName;
                result.rCode = existingUser.ReferralCode;
                //GeneralClass.FullName = result.fullName;
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> Login(LoginDTO dto)
        {

            var existingUser = await _userManager.FindByEmailAsync(dto.email);


            if (existingUser == null)
            {
                return "Not a user.";
            }
            else
            {
                if (existingUser.Role == UserRolesEnums.Admin)
                {
                    return "Successful";
                }

                return "Error";// Enum.GetName(typeof(UserRolesEnums), existingUser.Role) ;
            }
        }
        public async Task<List<EnrollmentDetails>> Enrollment()
        {
            try
            {
                var result = _userManager.Users.ToList();
                var enrollmentList = new List<EnrollmentDetails>();
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        var enrollment = new EnrollmentDetails
                        {
                            DateOfBirth = item.DateOfBirth.ToString("dd/MM/yyyy"),
                            FirstName = item.FirstName,
                            Gender = item.Gender,
                            LastName = item.LastName,
                            MiddleName = item.MiddleName,
                            ReferralCode = item.ReferralCode,
                            RegisteredDate = item.RegisteredDate.ToString("dd/MM/yyyy"),
                            Status = item.Status,
                            Address = item.Address + " " + await _addressServices.GetAddressByCityId(item.CityId),
                            Role = item.Role

                        };

                        enrollmentList.Add(enrollment);
                    }

                }
                return enrollmentList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EnrollmentDetails> SingleEnrollment(string userEmail)
        {
            try
            {
                var result = await _userManager.FindByEmailAsync(userEmail);
                var enrollmentList = new EnrollmentDetails();
                if (result != null)
                {
                    enrollmentList = new EnrollmentDetails
                    {
                        DateOfBirth = result.DateOfBirth.ToString("dd/MM/yyyy"),
                        FirstName = result.FirstName,
                        Gender = result.Gender,
                        LastName = result.LastName,
                        MiddleName = result.MiddleName,
                        ReferralCode = result.ReferralCode,
                        RegisteredDate = result.RegisteredDate.ToString("dd/MM/yyyy"),
                        Status = result.Status,
                        Address = result.Address + " " + await _addressServices.GetAddressByCityId(result.CityId)

                    };

                }
                return enrollmentList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<AllPayments>> Paymemts()
        {
            try
            {
                var result = await _context.UserPaymentHistory.Include(x => x.UserProgramOption).ThenInclude(x => x.User).Include(x => x.UserProgramOption).ThenInclude(x => x.ProgramOption).ThenInclude(x => x.Program).ThenInclude(x => x.Category).Select(x => new AllPayments
                {
                    Amount = x.Amount.ToString("N"),
                    Id = x.Id,
                    Payee = $"{x.UserProgramOption.User.FirstName } {x.UserProgramOption.User.LastName }",
                    PaymentDate = x.PaymentDate.ToShortDateString(),
                    PaymentMethodId = x.PaymentMethodId,
                    PaymentReference = x.PaymentReference,
                    Program = x.UserProgramOption.ProgramOption.Name,
                    StatusId = x.StatusId,
                    TransactionReference = x.TransactionReference
                }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<AllPayments>> PaymentbyUser(string userEmail)
        {
            try
            {
                var result = await _context.UserPaymentHistory.Include(x => x.UserProgramOption).ThenInclude(x => x.User).Include(x => x.UserProgramOption).ThenInclude(x => x.ProgramOption).ThenInclude(x => x.Program).ThenInclude(x => x.Category).Where(x => x.UserProgramOption.User.Email == userEmail).Select(x => new AllPayments
                {
                    Amount = x.Amount.ToString("N"),
                    Id = x.Id,
                    Payee = $"{x.UserProgramOption.User.FirstName } {x.UserProgramOption.User.LastName }",
                    PaymentDate = x.PaymentDate.ToShortDateString(),
                    PaymentMethodId = x.PaymentMethodId,
                    PaymentReference = x.PaymentReference,
                    Program = x.UserProgramOption.ProgramOption.Name,
                    StatusId = x.StatusId,
                    TransactionReference = x.TransactionReference
                }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<AllPayments> SinglePayment(int paymentId)
        {
            try
            {
                var result = await _context.UserPaymentHistory.Include(x => x.UserProgramOption).ThenInclude(x => x.User).Include(x => x.UserProgramOption).ThenInclude(x => x.ProgramOption).ThenInclude(x => x.Program).ThenInclude(x => x.Category).Where(x => x.Id == paymentId).Select(x => new AllPayments
                {
                    Amount = x.Amount.ToString("N"),
                    Id = x.Id,
                    Payee = $"{x.UserProgramOption.User.FirstName } {x.UserProgramOption.User.LastName }",
                    PaymentDate = x.PaymentDate.ToShortDateString(),
                    PaymentMethodId = x.PaymentMethodId,
                    PaymentReference = x.PaymentReference,
                    Program = x.UserProgramOption.ProgramOption.Name,
                    StatusId = x.StatusId,
                    TransactionReference = x.TransactionReference
                }).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> ConfirmSinglePayment(int paymentId)
        {
            try
            {
                var result = await _context.UserPaymentHistory.Where(x => x.Id == paymentId).FirstOrDefaultAsync();

                if (result != null)
                {
                    result.StatusId = PaymentStatusEnums.Paid;
                }

                _context.UserPaymentHistory.Update(result);

                await _context.SaveChangesAsync();

                return "Successful";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ProgramCategory>> ProgramCategories()
        {
            try
            {
                var result = await _context.ProgramCategory.ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Programs>> Programs()
        {
            try
            {
                var result = await _context.Programs.Include(x => x.Category).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ProgramOptions>> ProgramOptions()
        {
            try
            {
                var result = await _context.ProgramOptions.Include(x => x.Program).ThenInclude(x => x.Category).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<programFees>> ProgramFees()
        {
            try
            {
                var result = await _context.ProgramOptions.Select(x => new programFees
                {
                    DepositNGN = x.DepositNGN,
                    Id = x.Id,
                    Name = x.Name,
                    PriceUSD = x.PriceUSD,
                    PriceNGN = x.PriceNGN,
                    DepositUSD = x.DepositUSD
                }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Programs>> AddPrograms(Programs dto)
        {
            try
            {
                var exist = await _context.Programs.Where(x => x.Name == dto.Name && x.CategoryId == dto.CategoryId).CountAsync();
                if (exist <= 0)
                {
                    var newPro = new Programs
                    {
                        // DepositNGN = dto.DepositNGN,
                        CategoryId = dto.CategoryId,
                        //Duration =dto.Duration,
                        Name = dto.Name,
                        // PriceNGN =dto.PriceNGN,
                        Description = dto.Description,
                        // PriceUSD =dto.PriceUSD,
                        // DepositUSD =dto.DepositUSD
                    };

                    await _context.Programs.AddAsync(newPro);

                    await _context.SaveChangesAsync();
                }

                var result = await _context.Programs.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ProgramOptions>> AddProgramOption(ProgramOptions dto)
        {
            try
            {
                var exist = await _context.ProgramOptions.Where(x => x.Name == dto.Name && x.ProgramId == dto.ProgramId).CountAsync();
                if (exist <= 0)
                {
                    var lastId = await _context.ProgramOptions.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync();

                    lastId = lastId + 1;
                    var newPro = new ProgramOptions
                    {
                        DepositNGN = dto.DepositNGN,
                        ProgramId = dto.ProgramId,
                        Duration = dto.Duration,
                        Name = dto.Name,
                        PriceNGN = dto.PriceNGN,
                        Description = dto.Description,
                        PriceUSD = dto.PriceUSD,
                        DepositUSD = dto.DepositUSD,
                        StartDate = dto.StartDate,
                        MaxSubjectSelection = dto.MaxSubjectSelection,
                        ProgramOptionId = $"{lastId:0000}"
                    };

                    await _context.ProgramOptions.AddAsync(newPro);

                    await _context.SaveChangesAsync();
                }

                var result = await _context.ProgramOptions.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProgramCategory>> AddProgramCategory(ProgramCategory dto)
        {
            try
            {
                var exist = await _context.ProgramCategory.Where(x => x.Name == dto.Name).CountAsync();
                if (exist <= 0)
                {
                    var newPro = new ProgramCategory
                    {

                        Name = dto.Name
                    };

                    await _context.ProgramCategory.AddAsync(newPro);

                    await _context.SaveChangesAsync();
                }
                var result = await _context.ProgramCategory.ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

