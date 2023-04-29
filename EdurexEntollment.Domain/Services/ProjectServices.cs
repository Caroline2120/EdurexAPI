using EdurexEnrollment.Core.DTO;
using EdurexEnrollment.Core.Enums;
using EdurexEnrollment.Core.Interface;
using EdurexEnrollment.Core.Model;
using EdurexEnrollment.Core.ResponseVM;
using EdurexEnrollment.Core.Utilities;
using EdurexEnrollment.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace EdurexEntollment.Domain.Services
{
    public class ProjectServices : IProjectServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IAddressServices _addressServices;
        private readonly APISettings _apiSettings;
        private readonly UserManager<Users> _userManager;
        private readonly IMessagingService _messagingService;
        private readonly JwtSettings _jwtSettings;

        // private IWebHostEnvironment _env;

        public ProjectServices(ApplicationDbContext context, IOptions<JwtSettings> jwtSettings, IMessagingService messagingService, IAddressServices addressServices, UserManager<Users> userManager, IOptions<APISettings> apiSettings)
        {
            _context = context;
            _addressServices = addressServices;
            _userManager = userManager;
            _apiSettings = apiSettings.Value;
            _messagingService = messagingService;
            _jwtSettings = jwtSettings.Value;

            // _env = env;
        }

        public async Task<GeneralResponse<RegisterVM>> GetRegister()
        {
            //Load Program categories from DB
            //-------------------------------
            IEnumerable<ProgramCategory> programCategoryList = await _context.ProgramCategory.ToListAsync();

            //Load Countries from LocationAPI
            //-------------------------------
            var country = await _addressServices.GetAllCountry();
            var allCountries = new List<CountryDetails>();
            foreach (var item in country.data)
            {
                var countryList = new CountryDetails()
                {
                    Id = item.countryId,
                    Name = item.countryName
                };

                allCountries.Add(countryList);
            }

            //Load Nigeria States from LocationAPI
            //-------------------------------------
            var states = await _addressServices.GetAllStateByCountryId(163);
            var allstates = new List<StateDetails>();
            foreach (var item in states.data.states)
            {
                var stateList = new StateDetails()
                {
                    Id = item.stateId,
                    Name = item.stateName
                };

                allstates.Add(stateList);
            }

            //Load Subjects from DB
            //---------------------
            // IEnumerable<Subjects> subjectList = await _context.Subjects.ToListAsync();

            var viewModel = new RegisterVM
            {
                programCategoryListz = programCategoryList,
                countryListz = allCountries,
                //subjectListz = subjectList,
                nigeriaStatesListz = allstates
            };

            return new GeneralResponse<RegisterVM>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = viewModel
            } ;
        }
        public async Task<GeneralResponse<List<ProgramCategory>>> GetProgramCategories()
        {
            //Load program categories from DB
            //-------------------------------
            var programs = await _context.ProgramCategory.OrderByDescending(x => x.Id).ToListAsync();
            return new GeneralResponse<List<ProgramCategory>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = programs
            };
        }
        public async Task<GeneralResponse<List<Programs>>> GetProgramsByCategoryId(int CategoryId)
        {
            //Load program by program category Id from DB
            //-------------------------------------------
            var programs = await _context.Programs.Where(x => x.CategoryId == CategoryId).OrderByDescending(x => x.Id).ToListAsync();
            return new GeneralResponse<List<Programs>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = programs
            };
        }
        public async Task<GeneralResponse<List<ProgramOptions>>> GetProgramOptionsByProgramId(int ProgramId)
        {
            //Load program options by program Id from DB
            //-------------------------------------------
            var programs = await _context.ProgramOptions.Where(x => x.ProgramId == ProgramId).OrderByDescending(x => x.Id).ToListAsync();
            return new GeneralResponse<List<ProgramOptions>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = programs
            };
        }
        public async Task<GeneralResponse<List<Subjects>>> GetProgramSubjectssByOptionId(int OptionId)
        {
            //Load program subjects by program Id from DB
            //-------------------------------------------
            var programs = await _context.Subjects.Where(x => x.ProgramOptionId == OptionId).OrderByDescending(x => x.Id).ToListAsync();
            return new GeneralResponse<List<Subjects>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = programs
            };
        }
        public async Task<GeneralResponse<List<countriesbyCountry>>> GetCountries()
        {
            //Load States by countryId from LocationAPI
            //---------------------------------------
            var state = await _addressServices.GetAllCountry();
            var allStates = new List<countriesbyCountry>();
            foreach (var item in state.data)
            {
                var stateList = new countriesbyCountry()
                {
                    countryId = item.countryId,
                    countryName = item.countryName,
                    iso = item.iso
                };

                allStates.Add(stateList);
            }
            return new GeneralResponse<List<countriesbyCountry>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = allStates
            };
        }
        public async Task<GeneralResponse<List<states>>> GetStatesByCountryId(int CountryId)
        {
            //Load States by countryId from LocationAPI
            //---------------------------------------
            var state = await _addressServices.GetAllStateByCountryId(CountryId);
            var allStates = new List<states>();
            foreach (var item in state.data.states)
            {
                var stateList = new states()
                {
                    stateId = item.stateId,
                    stateName = item.stateName
                };

                allStates.Add(stateList);
            }
            return new GeneralResponse<List<states>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = allStates
            };
        }
        public async Task<GeneralResponse<List<EdurexEnrollment.Core.Utilities.Cities>>> GetCitiesByStateId(int StateId)
        {
            //Load Cities by stateId from LocationAPI
            //---------------------------------------
            var city = await _addressServices.GetAllCityByStateId(StateId);
            var allCities = new List<EdurexEnrollment.Core.Utilities.Cities>();
            foreach (var item in city.data.cities)
            {
                var cityList = new EdurexEnrollment.Core.Utilities.Cities()
                {
                    cityId = item.cityId,
                    cityName = item.cityName
                };

                allCities.Add(cityList);
            }
            return new GeneralResponse<List<EdurexEnrollment.Core.Utilities.Cities>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = allCities
            };
        }

        public async Task<GeneralResponse<List<Streets>>> GetStreetsByCityId(int CityId)
        {
            //Load Cities by stateId from LocationAPI
            //---------------------------------------
            var street = await _addressServices.GetAllStreetByCityId(CityId);
            var allStreets = new List<Streets>();
            if (street.data != null)
            {
                foreach (var item in street.data.streetName)
                {
                    var streetList = new Streets()
                    {
                        streetId = item.streetId,
                        streetName = item.streetName
                    };

                    allStreets.Add(streetList);
                }
            }

            return new GeneralResponse<List<Streets>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = allStreets
            };
        }

        public async Task<GeneralResponse<List<Institutions>>> GetInstitutions()
        {
            //Load Institutions from DB
            //---------------------
            var institutionList = await _context.Institutions.OrderByDescending(x => x.Id).ToListAsync();
            return new GeneralResponse<List<Institutions>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = institutionList
            };
        }
        public async Task<GeneralResponse<List<Courses>>> GetCourses()
        {
            //Load Subjects from DB
            //---------------------
            var courseList = await _context.Courses.OrderByDescending(x => x.Id).ToListAsync();

            return new GeneralResponse<List<Courses>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = courseList
            };
        }
        //public async Task<GeneralResponse<string> GetPriceByProgramId(int ProgramId)
        //{
        //    //Load Program Price by programId from DB
        //    //---------------------------------------
        //    var price = await _context.Programs.Where(x=>x.Id == ProgramId).Select(x=> new { price = x.PriceNGN, deposit = x.DepositNGN, duration = x.Duration}).FirstOrDefaultAsync();

        //    var result = "";
        //    if(price != null)
        //    {
        //        result = price.price.ToString("N") + "=" + price.deposit.ToString("N") + "=" + price.duration + "="+ price.price + "=" + price.deposit;
        //    }
        //    return result;
        //}
        public async Task<GeneralResponse<ProgramOptionPriceVM>> GetPriceByProgramOptionId(int ProgramOptionId)
        {
            //Load Program option Price by programOptionId from DB
            //----------------------------------------------------
            var price = await _context.ProgramOptions.Where(x => x.Id == ProgramOptionId).Select(x => new { price = x.PriceNGN, deposit = x.DepositNGN, duration = x.Duration, maxSubjects = x.MaxSubjectSelection, USD = x.PriceUSD }).FirstOrDefaultAsync();

            var result = new ProgramOptionPriceVM();
            if (price != null)
            {
                result.PriceNGN = price.price.ToString("N");
                result.DepositNGN = price.deposit.ToString("N");
                result.Duration =price.duration;
                result.USD = price.USD;
                result.maxSubjects = price.maxSubjects;
            }
            return new GeneralResponse<ProgramOptionPriceVM>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = result
            };
        }
        public async Task<GeneralResponse<PaymentStatusResponse>> QueryBankCOnnectPayment(string PaymentRef)
        {
            var result = new PaymentStatusResponse();

            int paymentType = Convert.ToInt32(PaymentRef.Substring(PaymentRef.Length - 1));
            //if (paymentType == 5)
            //{
            //    return "Successful";
            //}

            int payRefCount = PaymentRef.Length;// PaymentRef.Substring(PaymentRef.Length - 1);
            //paymentType = int.Parse(payRef);
            if (paymentType != 5 && payRefCount <= 10)
            {
                result = await FinalizeMonocoPayment(PaymentRef);
            }
            else if (paymentType != 5 && payRefCount >= 10)
            {
                result = await FinalizeMonnifyPayment(PaymentRef);
            }

            return new GeneralResponse<PaymentStatusResponse>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = result
            };
        }
        public async Task<PaymentStatusResponse> FinalizeMonnifyPayment(string paymentReference)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                using (var _apiClient = new HttpClient(httpClientHandler))
                {
                    //Get the transaction details
                    var PaymentDetails = await _context.UserPaymentHistory.Include(x => x.UserProgramOption).ThenInclude(x => x.ProgramOption).Where(x => x.PaymentReference == paymentReference && x.TransactionReference != "Discount").FirstOrDefaultAsync();
                    if (PaymentDetails == null)
                    {
                        return null;// $"Payment Reference {paymentReference} not found";
                    }
                    var result = new PaymentStatusResponse();
                    //Get the access token
                    var authenticationString = $"{_apiSettings.MonnifyKey}:{_apiSettings.MonnifySecret}";
                    var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
                    var authRequest = new HttpRequestMessage(HttpMethod.Post, $"{_apiSettings.MonnifyBaseUrl}/v1/auth/login");
                    authRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                    var authResponse = await _apiClient.SendAsync(authRequest);
                    var jsonAuthResponse = await authResponse.Content.ReadAsStringAsync();
                    MonnifyAuthenticationResponse deserializedAuthResponse = JsonConvert.DeserializeObject<MonnifyAuthenticationResponse>(jsonAuthResponse);

                    if (!deserializedAuthResponse.RequestSuccessful)
                    {
                        throw new Exception($"Status verification access failed. {deserializedAuthResponse.ResponseMessage}");
                    }

                    //Call the status verification endpoint
                    string encodedTransRef = System.Web.HttpUtility.UrlEncode(PaymentDetails.TransactionReference);
                    var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_apiSettings.MonnifyBaseUrl}/v2/transactions/{encodedTransRef}");
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", deserializedAuthResponse.ResponseDetails.AccessToken);
                    var statusResponse = await _apiClient.SendAsync(requestMessage);
                    var jsonStatusResponse = await statusResponse.Content.ReadAsStringAsync();
                    MonnifyPaymentStatusResponse deserializedStatusResponse = JsonConvert.DeserializeObject<MonnifyPaymentStatusResponse>(jsonStatusResponse);

                    //Get program total amount
                    //------------------------
                    var programTotalAmount = await _context.ProgramOptions.Where(x => x.Id == PaymentDetails.UserProgramOption.ProgramOptionId).Select(x => new { programAmount = x.PriceNGN, startDate = x.StartDate }).FirstOrDefaultAsync();
                    var userProgram = await _context.UserProgramOption.Where(x => x.UserId == PaymentDetails.UserProgramOption.UserId && x.ProgramOptionId == PaymentDetails.UserProgramOption.ProgramOptionId).FirstOrDefaultAsync();
                    //Update the transaction table and invoice table if the payment is successful
                    if (deserializedStatusResponse.RequestSuccessful)
                    {
                        if (deserializedStatusResponse.ResponseDetails.PaymentStatus.Equals("PAID"))
                        {
                            //Update the user's total available subscription
                            //----------------------------------------------
                            if (PaymentDetails.StatusId != PaymentStatusEnums.Paid)
                            {
                                PaymentDetails.StatusId = PaymentStatusEnums.Paid;
                                _context.UserPaymentHistory.Update(PaymentDetails);
                                await _context.SaveChangesAsync();

                                //Get user payment history
                                //------------------------
                                var totalAmountPaid = await _context.UserPaymentHistory.Where(x => x.UserProgramOptionId == PaymentDetails.UserProgramOptionId && x.StatusId == PaymentStatusEnums.Paid).Select(x => x.Amount).SumAsync();

                                // //Check for discount
                                // //------------------
                                // var discountApplied = await _context.DiscountUsageHistory.Include(x=>x.UserDiscount).Where(x => x.UsedByProgramOptionId == PaymentDetails.UserProgramOptionId).FirstOrDefaultAsync();
                                //if(discountApplied != null)
                                // {
                                //     totalAmountPaid = totalAmountPaid + (decimal)discountApplied.Earnings;
                                // }

                                if ((float)totalAmountPaid < programTotalAmount.programAmount)
                                {
                                    userProgram.PaymentStatus = UserProgramPaymentStatusEnums.Deposited;
                                }
                                else
                                {
                                    userProgram.PaymentStatus = UserProgramPaymentStatusEnums.Paid;
                                }
                                var refCode = await _context.UserReferred.Include(x => x.Referral).Where(x => x.PaymentRef == paymentReference).FirstOrDefaultAsync();// _context.UserPaymentHistory.Where(x => x.UserProgramOptionId == PaymentDetails.UserProgramOptionId).OrderBy(x => x.Id).Select(x => x.ReferralDiscountCode).FirstOrDefaultAsync();
                                if (refCode != null)
                                {
                                    decimal earni = 0;
                                    if (refCode.Referral.Role == UserRolesEnums.Freelance)
                                    {
                                        earni = (PaymentDetails.Amount * 15) / 100;
                                    }
                                    else if (refCode.Referral.Role == UserRolesEnums.Marketer)
                                    {
                                        earni = (PaymentDetails.Amount * 10) / 100;
                                    }
                                    else if (refCode.Referral.Role == UserRolesEnums.Enrolled)
                                    {
                                        earni = (PaymentDetails.Amount * (15 / 2)) / 100;
                                    }

                                    refCode.Earnings = (float)earni;

                                    _context.UserReferred.Update(refCode);

                                }

                            }

                            result.AmountPaid = PaymentDetails.Amount.ToString();
                            result.PaymentStatus = "Paid";
                            result.paymentRef = paymentReference;
                            result.PaymentMethod = Enum.GetName(typeof(PaymentMethodEnums), PaymentDetails.PaymentMethodId);
                            result.PaymentDate = PaymentDetails.PaymentDate;
                        }
                        if (programTotalAmount.startDate.Date > DateTime.Now.Date)
                        {
                            userProgram.ProgramStatus = UserProgramStatusEnums.Pending;
                        }
                        else if (programTotalAmount.startDate.Date == DateTime.Now.Date)
                        {
                            userProgram.ProgramStatus = UserProgramStatusEnums.InProgress;
                        }
                        else
                        {
                            userProgram.ProgramStatus = UserProgramStatusEnums.Completed;
                        }
                        //userProgram.ProgramStatus = UserProgramStatusEnums.InProgress;
                        userProgram.RegDate = DateTime.Now.Date;
                        _context.UserProgramOption.Update(userProgram);
                        await _context.SaveChangesAsync();
                        return result;
                    }

                    return null;// "";
                }
            }
            //return "";
        }
        public async Task<PaymentStatusResponse> FinalizeMonocoPayment(string PaymentRef)
        {
            //Get the transaction details

            var PaymentDetails = await _context.UserPaymentHistory.Include(x => x.UserProgramOption).ThenInclude(x => x.ProgramOption).Where(x => x.PaymentReference == PaymentRef && x.TransactionReference != "Discount").FirstOrDefaultAsync();
            if (PaymentDetails == null)
            {
                return null;// $"Payment Reference {PaymentRef} not found";
            }
            var result = new PaymentStatusResponse();
            //Get program total amount
            //------------------------
            var programTotalAmount = await _context.ProgramOptions.Where(x => x.Id == PaymentDetails.UserProgramOption.ProgramOptionId).Select(x => new { programAmount = x.PriceNGN, startDate = x.StartDate }).FirstOrDefaultAsync();
            var userProgram = await _context.UserProgramOption.Where(x => x.UserId == PaymentDetails.UserProgramOption.UserId && x.ProgramOptionId == PaymentDetails.UserProgramOption.ProgramOptionId).FirstOrDefaultAsync();

            var client = new RestClient($"{_apiSettings.MonocoBaseUrl }/v1/payments");
            var request = new RestRequest("verify", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("mono-sec-key", $"{_apiSettings.MonocoSecretKey }");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"reference\":\"" + PaymentRef + "\"}", ParameterType.RequestBody);
            RestResponse response = client.Execute(request);

            MonocoPaymentStatusResponse initDeserializeResponse = JsonConvert.DeserializeObject<MonocoPaymentStatusResponse>(response.Content);
            decimal amountPaid = 0;

            if (initDeserializeResponse.data.Status == "successful")
            {
                
                amountPaid = decimal.Parse(initDeserializeResponse.data.Amount) / 100;

                PaymentDetails.StatusId = PaymentStatusEnums.Paid;

                _context.UserPaymentHistory.Update(PaymentDetails);
                await _context.SaveChangesAsync();

                //Get user payment history
                //------------------------
                var totalAmountPaid = await _context.UserPaymentHistory.Where(x => x.UserProgramOptionId == PaymentDetails.UserProgramOptionId && x.StatusId == PaymentStatusEnums.Paid).Select(x => x.Amount).SumAsync();

                if ((float)totalAmountPaid < programTotalAmount.programAmount)
                {
                    userProgram.PaymentStatus = UserProgramPaymentStatusEnums.Deposited;
                }
                else
                {
                    userProgram.PaymentStatus = UserProgramPaymentStatusEnums.Paid;
                }

                var refCode = await _context.UserReferred.Include(x => x.Referral).Where(x => x.PaymentRef == PaymentRef).FirstOrDefaultAsync();// _context.UserPaymentHistory.Where(x => x.UserProgramOptionId == PaymentDetails.UserProgramOptionId).OrderBy(x => x.Id).Select(x => x.ReferralDiscountCode).FirstOrDefaultAsync();
                if (refCode != null)
                {
                    decimal earni = 0;
                    if (refCode.Referral.Role == UserRolesEnums.Freelance)
                    {
                        earni = (PaymentDetails.Amount * 15) / 100;
                    }
                    else if (refCode.Referral.Role == UserRolesEnums.Marketer)
                    {
                        earni = (PaymentDetails.Amount * 10) / 100;
                    }
                    else if (refCode.Referral.Role == UserRolesEnums.Enrolled)
                    {
                        earni = (PaymentDetails.Amount * (15 / 2)) / 100;
                    }

                    refCode.Earnings = (float)earni;

                    _context.UserReferred.Update(refCode);

                }

                result.AmountPaid = amountPaid.ToString();
                result.PaymentStatus = "Paid";
                result.paymentRef = PaymentRef;
                result.PaymentMethod = Enum.GetName(typeof(PaymentMethodEnums), PaymentDetails.PaymentMethodId);
                result.PaymentDate = PaymentDetails.PaymentDate;
            }

            if (programTotalAmount.startDate.Date > DateTime.Now.Date)
            {
                userProgram.ProgramStatus = UserProgramStatusEnums.Pending;
            }
            else if (programTotalAmount.startDate.Date == DateTime.Now.Date)
            {
                userProgram.ProgramStatus = UserProgramStatusEnums.InProgress;
            }
            else
            {
                userProgram.ProgramStatus = UserProgramStatusEnums.Completed;
            }
            //userProgram.ProgramStatus = UserProgramStatusEnums.InProgress;
            userProgram.RegDate = DateTime.Now.Date;
            _context.UserProgramOption.Update(userProgram);
            await _context.SaveChangesAsync();
           
            return result;
        }
        public async Task<GeneralResponse<PaymentInitialiseResponse>> RegisterUser(RegisterDTO dto)
        {

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);


            if (existingUser != null)
            {
                return null;// new PaymentInitialiseResponse() { errorMessage = "Email already exist." };
            }
            else
            {
                var currentuser = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == dto.Phone);
                if (currentuser != null)
                {
                    return null;// new PaymentInitialiseResponse() { errorMessage = "Phone number already exist." };

                }

                //var superLo = await _addressServices.CreateLocation(dto.CityId, dto.StreetName, dto.StreetNumber);

                //var SuperId = superLo.data.superId;
                var lastUser = await _context.users.OrderByDescending(x => x.UserId).Select(x => x.UserId).FirstOrDefaultAsync();
                int count = 0;
                if (lastUser != null && lastUser != "")
                {
                    count = Convert.ToInt32(lastUser);
                }

                count = count + 1;
                var rCode = GenerateReferalCode();
                var newUser = new Users
                {
                    Email = dto.Email.ToLower(),
                    UserName = dto.Email.ToLower(),
                    PhoneNumber = dto.Phone,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Gender = dto.Gender,
                    //StateId = dto.StateId,
                    CityId = dto.CityId,
                    Address = dto.Address,
                    DateOfBirth = dto.DateOfBirth,
                    EmailConfirmed = true,
                    MiddleName = dto.MiddleName,
                    Status = UserStatusEnums.Pending,
                    ReferralCode = rCode,
                    RegisteredDate = DateTime.Now.Date,
                    AlternatePhone = dto.AlternatePhone,
                    UserId = $"{count:0000}",
                    heardAboutUs = dto.heardAboutUs,
                    Role = UserRolesEnums.Enrolled
                };

                var createdUser = await _userManager.CreateAsync(newUser, dto.Password);

                if (createdUser.Succeeded)
                {
                    //Add user program into DB
                    //-------------------------
                    var userPro = new UserProgramOption()
                    {
                        ProgramOptionId = dto.ProgramOptionId,
                        UserId = newUser.Id,
                        RegDate = DateTime.Now
                    };
                    await _context.UserProgramOption.AddAsync(userPro);

                    //Add user subjects into DB
                    //-------------------------                    
                    if (dto.SubjectIds != null && dto.SubjectIds.Count() > 0)
                    {
                        var userSub = new UserSubjects()
                        {
                            SubjectIds = string.Join(",", dto.SubjectIds),
                            UserId = newUser.Id
                        };
                        await _context.UserSubjects.AddAsync(userSub);
                    }

                    if (dto.Choices.FirstInstitution > 0)
                    {
                        //Add user first choice into DB
                        //-------------------------
                        var userFChoi = new UserChoices()
                        {
                            ProgramId = dto.ProgramOptionId,
                            InstitutionId = dto.Choices.FirstInstitution,
                            CourseId = dto.Choices.FirstCourse,
                            UserId = newUser.Id
                        };
                        await _context.UserChoices.AddAsync(userFChoi);
                    }


                    if (dto.Choices.SecondInstitution > 0)
                    {

                        //Add user second choice into DB
                        //-------------------------
                        var userSChoi = new UserChoices()
                        {
                            ProgramId = dto.ProgramOptionId,
                            InstitutionId = dto.Choices.SecondInstitution,
                            CourseId = dto.Choices.SecondCourse,
                            UserId = newUser.Id
                        };
                        await _context.UserChoices.AddAsync(userSChoi);
                    }
                    await _context.SaveChangesAsync();


                    //Send Email
                    //----------
                    var builder = new StringBuilder();
                    await _messagingService.SendEmail(dto.Email, "Edurex Academy registration message", "Edurex Onboarding Successful");

                    //Send SMS
                    //--------
                    await _messagingService.SendSMS(dto.Phone, "Edurex Onboarding Successful");

                    PaymentMethodEnums paymentMethod = (PaymentMethodEnums)dto.PaymentMethod;

                    //var depositValue = await _context.Programs.Where(x => x.Id == dto.ProgramId).Select(x => x.Deposit).FirstOrDefaultAsync();
                    var pDTO = new PaymentDTO()
                    {
                        Amount = (decimal)dto.Amount,
                        BankConnectPaymenttype = "onetime-debit",
                        PaymentMethod = paymentMethod,
                        userProgramOptionId = userPro.Id,
                        ReferralDiscountCode = dto.referralCode
                    };

                    switch (paymentMethod)
                    {
                        case PaymentMethodEnums.Card:
                            var cardResponse = await InitializeCardPayment(pDTO);
                            return cardResponse;//Ok(new Response { IsSuccessful = true, Message = StatusMessage.OK, Code = ResponseCode.OK, Data = cardResponse });

                        case PaymentMethodEnums.AccountTransfer:
                            var transferResponse = await InitializeAccountTransferPayment(pDTO);
                            return transferResponse;//Ok(new Response { IsSuccessful = true, Message = StatusMessage.OK, Code = ResponseCode.OK, Data = transferResponse });

                        case PaymentMethodEnums.BankConnect:
                            var BankConnectResponse = await InitializeBankCOnnectPayment(pDTO);
                            return BankConnectResponse;// Ok(new Response { IsSuccessful = true, Message = StatusMessage.OK, Code = ResponseCode.OK, Data = BankConnectResponse });
                        case PaymentMethodEnums.Offline:
                            var OfflineResponse = await InitializeOfflinePayment(pDTO);
                            return OfflineResponse;// Ok(new Response { IsSuccessful = true, Message = StatusMessage.OK, Code = ResponseCode.OK, Data = BankConnectResponse });

                    }
                    throw new Exception("No subscription method found");
                }
                return null;// new PaymentInitialiseResponse() { errorMessage = "Error creating user." };
            }
        }
        public async Task<GeneralResponse<AuthenticationToken>> Login(LoginDTO dto)
        {

            var existingUser = await _userManager.FindByEmailAsync(dto.email);
          //  var response = new AuthenticationToken();

            if (existingUser == null)
            {
                //response.Token = "Not found";
                return null;
            }
            else
            {
                return new GeneralResponse<AuthenticationToken>()
                {
                    Successful = true,
                    Code = StatusCodes.Status200OK.ToString(),
                    Data = AuthenticationToken(dto.email, existingUser.Id)
                };
            }
        }
        public AuthenticationToken AuthenticationToken(string email, string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var TokenDescriptor = new SecurityTokenDescriptor


            {
                Subject = new ClaimsIdentity(claims: new[]
            {
                    new Claim(type: JwtRegisteredClaimNames.Sub, value: email),
                    new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                    new Claim("id", value:  userId)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), algorithm: SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(TokenDescriptor);
            var response = new AuthenticationToken();
            response.Token = tokenHandler.WriteToken(token);
            response.Email = email;
            return response;
        }
        public async Task<GeneralResponse<DashboardVM>> DashboardRe(string userId)
        {

            var existingUser = await _userManager.FindByIdAsync(userId);


            if (existingUser != null)
            {
                //Program Details
                //---------------
                //var programName = ""; var subjectIds ="";
                //var subjectList = new List<string>();
                //var listSub = new List<string>();

                //var program = await _context.UserSubjects.Include(x => x.Program).ThenInclude(x => x.Category).Where(x => x.UserId == existingUser.Id).FirstOrDefaultAsync();
                //if(program != null)
                //{                    
                //    programName = program.Program.Category.Name + "-" + program.Program.Name;
                //    subjectIds = program.SubjectIds;

                //    listSub = subjectIds.Split(',').ToList();

                //    foreach(var subId in listSub)
                //    {
                //        var subjectName = await _context.Subjects.Where(x => x.Id == Convert.ToInt32(subId)).Select(x => x.Name).FirstOrDefaultAsync();
                //        subjectList.Add(subjectName);
                //    }
                //}

                // Choices History
                //----------------
                var userChoices = await _context.UserChoices.Include(x => x.Institution).Include(x => x.Course).Where(x => x.UserId == userId).Select(x => new
                    ChoicesRecord
                {
                    Course = x.Course.Name,
                    Institution = x.Institution.Name

                }).ToListAsync();

                //Program history
                //---------------
                var userPrograms = await _context.UserProgramOption.Include(x => x.ProgramOption).ThenInclude(x => x.Program).ThenInclude(x => x.Category).Where(x => x.UserId == userId).OrderByDescending(x => x.Id).Select(x => new
                    ProgramRecord
                {
                    ProgramOptionId = x.ProgramOption.ProgramOptionId,
                    userProgramOptionId = x.Id,
                    Name = x.ProgramOption.Program.Name + "/" + x.ProgramOption.Name,
                    StartDate = x.ProgramOption.StartDate.ToString("dd/MM/yyyy"),
                    ProgramStatus = x.ProgramStatus,
                    ProgramPaymentStatus = x.PaymentStatus,
                    programPrice = x.ProgramOption.PriceNGN.ToString("N"),
                    //amountPaid = _context.UserPaymentHistory.Include(m => m.UserProgram).Where(m => m.UserProgram.ProgramId == x.ProgramId && m.StatusId == PaymentStatusEnums.Paid).Select(m => m.Amount).Sum()

                }).Take(5).ToListAsync();

                if (userPrograms.Count() > 0)
                {
                    foreach (var item in userPrograms)
                    {
                        var amountPaidperProg = await _context.UserPaymentHistory.Include(m => m.UserProgramOption).Where(m => m.UserProgramOptionId == item.userProgramOptionId && m.StatusId == PaymentStatusEnums.Paid).Select(m => m.Amount).SumAsync();
                        item.amountPaid = amountPaidperProg.ToString("N");
                    }

                }
                // Payment History
                //----------------
                var userPayment = await _context.UserPaymentHistory.Include(x => x.UserProgramOption).ThenInclude(x => x.ProgramOption).Where(x => x.UserProgramOption.UserId == userId).OrderByDescending(x => x.Id).Select(x => new
                    PaymentRecord
                {
                    Amount = x.Amount.ToString("N"),
                    PaymentDate = x.PaymentDate,
                    PaymentMethod = x.PaymentMethodId,
                    Status = x.StatusId,
                    programName = x.UserProgramOption.ProgramOption.Program.Name + "/" + x.UserProgramOption.ProgramOption.Name,
                    PaymentRef = x.PaymentReference
                }).Take(5).ToListAsync();

                var result = new DashboardVM();
                result.fullName = existingUser.FirstName;
                // result.lastName = existingUser.LastName;
                result.Programs = userPrograms;
                //result.programName = programName;
                result.PaymentRecord = userPayment;
                result.rCode = existingUser.ReferralCode;

                //result.SubjectRecord = subjectList;
                //result.ChoicesRecord = userChoices;
                //GeneralClass.FullName = result.fullName;
                return new GeneralResponse<DashboardVM>()
                {
                    Successful = true,
                    Code = StatusCodes.Status200OK.ToString(),
                    Data = result
                };
            }
            else
            {
                return null;
            }
        }
        private string GenerateReferalCode()
        {
            StringBuilder builder = new StringBuilder();

            Random rstToken = new Random();

            char ch;
            for (int i = 0; i < 6; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rstToken.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public async Task<GeneralResponse<PaymentInitialiseResponse>> InitializeCardPayment(PaymentDTO dto)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    using (var _apiClient = new HttpClient(httpClientHandler))
                    {
                        // Users user = await _userManager.FindByIdAsync(dto.userId);
                        //Add discount code
                        //-----------------------------

                        var subRef = $"{DateTime.Now.Ticks}{(int)dto.PaymentMethod}";

                        if (dto.ReferralDiscountCode != "" && dto.ReferralDiscountCode != null)
                        {
                            //var disCode = await _context.UserDiscount.Where(x => x.Code == dto.ReferralDiscountCode).FirstOrDefaultAsync();

                            var DiscountOwnerId = await _context.UserDiscount.Where(x => x.Code == dto.ReferralDiscountCode).FirstOrDefaultAsync();
                            if (DiscountOwnerId != null)
                            {
                                if (DiscountOwnerId.TotalApplied < DiscountOwnerId.TotalApproved)
                                {
                                    //Add discount details into DB
                                    //----------------------------
                                    var programPrice = await _context.UserProgramOption.Include(x => x.ProgramOption).Where(x => x.Id == dto.userProgramOptionId).Select(x => x.ProgramOption.PriceNGN).FirstOrDefaultAsync();
                                    var discount = (programPrice * DiscountOwnerId.Rate) / 100;
                                    DiscountOwnerId.TotalApplied = DiscountOwnerId.TotalApplied + 1;
                                    _context.UserDiscount.Update(DiscountOwnerId);

                                    if ((float)dto.Amount >= programPrice)
                                    {
                                        dto.Amount = dto.Amount - (decimal)discount;
                                    }

                                    //Add to discount history
                                    //-----------------------
                                    var hist = new DiscountUsageHistory()
                                    {
                                        Earnings = discount,
                                        UsedByProgramOptionId = dto.userProgramOptionId,
                                        UserDiscountId = DiscountOwnerId.Id,
                                    };

                                    await _context.DiscountUsageHistory.AddAsync(hist);

                                    UserPaymentHistory paymentHistroyDis = new UserPaymentHistory
                                    {
                                        UserProgramOptionId = dto.userProgramOptionId,
                                        Amount = (decimal)discount,
                                        PaymentReference = subRef,
                                        TransactionReference = "Discount",
                                        StatusId = PaymentStatusEnums.Paid,
                                        PaymentMethodId = PaymentMethodEnums.Discount,
                                        PaymentDate = DateTime.Now,
                                    };
                                    _context.UserPaymentHistory.Add(paymentHistroyDis);

                                    await _context.SaveChangesAsync();
                                }

                            }
                            else
                            {
                                // Add referral code to DB
                                //------------------------
                                var OwnerId = await _context.users.Where(x => x.ReferralCode == dto.ReferralDiscountCode && x.RegisteredDate.Date.AddMonths(3) >= DateTime.Now.Date).Select(x => x.Id).FirstOrDefaultAsync();
                                if (OwnerId != null && OwnerId != "")
                                {
                                    var userRef = new UserReferred()
                                    {
                                        ReferralId = OwnerId,
                                        ReferredUserProgramOptionId = dto.userProgramOptionId,
                                        PaymentRef = subRef,
                                        Earnings = 0,
                                    };
                                    await _context.UserReferred.AddAsync(userRef);
                                }
                            }

                        }

                        UserPaymentHistory paymentHistroy = new UserPaymentHistory
                        {
                            UserProgramOptionId = dto.userProgramOptionId,
                            Amount = dto.Amount,
                            PaymentReference = subRef,
                            TransactionReference = "",
                            StatusId = PaymentStatusEnums.Initialized,
                            PaymentMethodId = dto.PaymentMethod,
                            PaymentDate = DateTime.Now,
                        };
                        _context.UserPaymentHistory.Add(paymentHistroy);
                        await _context.SaveChangesAsync();

                        string[] paymentMethods = { dto.PaymentMethod.ToDescription().Replace(" ", "_").ToUpper() };
                        var user = await _context.UserProgramOption.Include(x => x.User).Where(x => x.Id == dto.userProgramOptionId).FirstOrDefaultAsync();
                        MonnifyPaymentInitializationRequest request = new MonnifyPaymentInitializationRequest
                        {
                            Amount = paymentHistroy.Amount,
                            CustomerName = $"{user.User.LastName } {user.User.FirstName}",
                            CustomerEmail = user.User.Email,
                            PaymentReference = paymentHistroy.PaymentReference,
                            PaymentDescription = "Payment",
                            CurrencyCode = "NGN",
                            ContractCode = _apiSettings.ContractCode,
                            RedirectUrl = $"{_apiSettings.PaymentRedirectUrl}?paymentRef={subRef}",
                            PaymentMethods = paymentMethods
                        };

                        var authenticationString = $"{_apiSettings.MonnifyKey}:{_apiSettings.MonnifySecret}";
                        var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
                        var initContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_apiSettings.MonnifyBaseUrl}/v1/merchant/transactions/init-transaction");
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                        requestMessage.Content = initContent;
                        var initResponse = await _apiClient.SendAsync(requestMessage);
                        var jsonInitResponse = await initResponse.Content.ReadAsStringAsync();
                        MonnifyPaymentInitializationResponse initDeserializeResponse = JsonConvert.DeserializeObject<MonnifyPaymentInitializationResponse>(jsonInitResponse);

                        if (initDeserializeResponse.RequestSuccessful)
                        {
                            //paymentHistroy.CheckoutUrl = initDeserializeResponse.ResponseDetails.CheckoutUrl;
                            paymentHistroy.TransactionReference = initDeserializeResponse.ResponseDetails.TransactionReference;
                            paymentHistroy.StatusId = PaymentStatusEnums.Pending;
                            await _context.SaveChangesAsync();

                            var response = new PaymentInitialiseResponse()
                            {
                                checkOutURL = initDeserializeResponse.ResponseDetails.CheckoutUrl,
                                paymentRef = subRef,
                                paymentMethod = Enum.GetName(typeof(PaymentMethodEnums), dto.PaymentMethod)
                            };

                            return new GeneralResponse<PaymentInitialiseResponse>()
                            {
                                Successful = true,
                                Code = StatusCodes.Status200OK.ToString(),
                                Data = response
                            };

                        }
                        //initDeserializeResponse.premium = premium;
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GeneralResponse<PaymentInitialiseResponse>> InitializeAccountTransferPayment(PaymentDTO dto)
        {
            try
            {

                using (var httpClientHandler = new HttpClientHandler())
                {
                    using (var _apiClient = new HttpClient(httpClientHandler))
                    {
                        var subRef = $"{DateTime.Now.Ticks}{(int)dto.PaymentMethod}";
                        //Users user = await _userManager.FindByIdAsync(dto.userId);

                        if (dto.ReferralDiscountCode != "" && dto.ReferralDiscountCode != null)
                        {
                            //var disCode = await _context.UserDiscount.Where(x => x.Code == dto.ReferralDiscountCode).FirstOrDefaultAsync();

                            var DiscountOwnerId = await _context.UserDiscount.Where(x => x.Code == dto.ReferralDiscountCode).FirstOrDefaultAsync();
                            if (DiscountOwnerId != null)
                            {
                                if (DiscountOwnerId.TotalApplied < DiscountOwnerId.TotalApproved)
                                {
                                    //Add discount details into DB
                                    //----------------------------
                                    var programPrice = await _context.UserProgramOption.Include(x => x.ProgramOption).Where(x => x.Id == dto.userProgramOptionId).Select(x => x.ProgramOption.PriceNGN).FirstOrDefaultAsync();
                                    var discount = (programPrice * DiscountOwnerId.Rate) / 100;
                                    DiscountOwnerId.TotalApplied = DiscountOwnerId.TotalApplied + 1;
                                    _context.UserDiscount.Update(DiscountOwnerId);

                                    if ((float)dto.Amount >= programPrice)
                                    {
                                        dto.Amount = dto.Amount - (decimal)discount;
                                    }

                                    //Add to discount history
                                    //-----------------------
                                    var hist = new DiscountUsageHistory()
                                    {
                                        Earnings = discount,
                                        UsedByProgramOptionId = dto.userProgramOptionId,
                                        UserDiscountId = DiscountOwnerId.Id,
                                    };

                                    await _context.DiscountUsageHistory.AddAsync(hist);

                                    UserPaymentHistory paymentHistroyDis = new UserPaymentHistory
                                    {
                                        UserProgramOptionId = dto.userProgramOptionId,
                                        Amount = (decimal)discount,
                                        PaymentReference = subRef,
                                        TransactionReference = "Discount",
                                        StatusId = PaymentStatusEnums.Paid,
                                        PaymentMethodId = PaymentMethodEnums.Discount,
                                        PaymentDate = DateTime.Now,
                                    };
                                    _context.UserPaymentHistory.Add(paymentHistroyDis);

                                    await _context.SaveChangesAsync();
                                }

                            }
                            else
                            {
                                // Add referral code to DB
                                //------------------------
                                var OwnerId = await _context.users.Where(x => x.ReferralCode == dto.ReferralDiscountCode && x.RegisteredDate.Date.AddMonths(3) >= DateTime.Now.Date).Select(x => x.Id).FirstOrDefaultAsync();
                                if (OwnerId != null && OwnerId != "")
                                {
                                    var userRef = new UserReferred()
                                    {
                                        ReferralId = OwnerId,
                                        ReferredUserProgramOptionId = dto.userProgramOptionId,
                                        PaymentRef = subRef,
                                        Earnings = 0
                                    };
                                    await _context.UserReferred.AddAsync(userRef);
                                }
                            }
                        }
                        var user = await _context.UserProgramOption.Include(x => x.User).Where(x => x.Id == dto.userProgramOptionId).FirstOrDefaultAsync();

                        UserPaymentHistory paymentHistroy = new UserPaymentHistory
                        {
                            UserProgramOptionId = dto.userProgramOptionId,
                            Amount = dto.Amount,
                            PaymentReference = subRef,
                            TransactionReference = "",
                            StatusId = PaymentStatusEnums.Initialized,
                            PaymentMethodId = dto.PaymentMethod,
                            PaymentDate = DateTime.Now
                        };
                        _context.UserPaymentHistory.Add(paymentHistroy);
                        await _context.SaveChangesAsync();

                        string[] paymentMethods = { dto.PaymentMethod.ToDescription().Replace(" ", "_").ToUpper() };
                        MonnifyPaymentInitializationRequest request = new MonnifyPaymentInitializationRequest
                        {
                            Amount = paymentHistroy.Amount,
                            CustomerName = $"{user.User.LastName } {user.User.FirstName}",
                            CustomerEmail = user.User.Email,
                            PaymentReference = paymentHistroy.PaymentReference,
                            PaymentDescription = "Payment",
                            CurrencyCode = "NGN",
                            ContractCode = _apiSettings.ContractCode,
                            RedirectUrl = $"{_apiSettings.PaymentRedirectUrl}?paymentRef={subRef}",
                            PaymentMethods = paymentMethods
                        };


                        var authenticationString = $"{_apiSettings.MonnifyKey}:{_apiSettings.MonnifySecret}";
                        var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
                        var initContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_apiSettings.MonnifyBaseUrl}/v1/merchant/transactions/init-transaction");
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                        requestMessage.Content = initContent;
                        var initResponse = await _apiClient.SendAsync(requestMessage);
                        var jsonInitResponse = await initResponse.Content.ReadAsStringAsync();
                        MonnifyPaymentInitializationResponse initDeserializeResponse = JsonConvert.DeserializeObject<MonnifyPaymentInitializationResponse>(jsonInitResponse);

                        if (initDeserializeResponse.RequestSuccessful)
                        {
                            paymentHistroy.TransactionReference = initDeserializeResponse.ResponseDetails.TransactionReference;
                            paymentHistroy.StatusId = PaymentStatusEnums.Pending;
                            await _context.SaveChangesAsync();

                            var response = new PaymentInitialiseResponse()
                            {
                                checkOutURL = initDeserializeResponse.ResponseDetails.CheckoutUrl,
                                paymentRef = subRef,
                                paymentMethod = Enum.GetName(typeof(PaymentMethodEnums), dto.PaymentMethod)

                            };

                            return new GeneralResponse<PaymentInitialiseResponse>()
                            {
                                Successful = true,
                                Code = StatusCodes.Status200OK.ToString(),
                                Data = response
                            };

                        }

                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string GenerateMonoPaymentRef()
        {
            return DateTime.UtcNow.Ticks.ToString().Substring(8);
        }
        public async Task<GeneralResponse<PaymentInitialiseResponse>> InitializeBankCOnnectPayment(PaymentDTO dto)
        {
            try
            {

                var user = await _context.UserProgramOption.Include(x => x.User).Where(x => x.Id == dto.userProgramOptionId).FirstOrDefaultAsync();


                string MonoType = "onetime-debit";

                string PaymentRef = GenerateMonoPaymentRef();

                if (dto.ReferralDiscountCode != "" && dto.ReferralDiscountCode != null)
                {
                    //var disCode = await _context.UserDiscount.Where(x => x.Code == dto.ReferralDiscountCode).FirstOrDefaultAsync();

                    var DiscountOwnerId = await _context.UserDiscount.Where(x => x.Code == dto.ReferralDiscountCode).FirstOrDefaultAsync();
                    if (DiscountOwnerId != null)
                    {
                        if (DiscountOwnerId.TotalApplied < DiscountOwnerId.TotalApproved)
                        {
                            //Add discount details into DB
                            //----------------------------
                            var programPrice = await _context.UserProgramOption.Include(x => x.ProgramOption).Where(x => x.Id == dto.userProgramOptionId).Select(x => x.ProgramOption.PriceNGN).FirstOrDefaultAsync();
                            var discount = (programPrice * DiscountOwnerId.Rate) / 100;
                            DiscountOwnerId.TotalApplied = DiscountOwnerId.TotalApplied + 1;
                            _context.UserDiscount.Update(DiscountOwnerId);

                            if ((float)dto.Amount >= programPrice)
                            {
                                dto.Amount = dto.Amount - (decimal)discount;
                            }

                            //Add to discount history
                            //-----------------------
                            var hist = new DiscountUsageHistory()
                            {
                                Earnings = discount,
                                UsedByProgramOptionId = dto.userProgramOptionId,
                                UserDiscountId = DiscountOwnerId.Id,
                            };

                            await _context.DiscountUsageHistory.AddAsync(hist);

                            UserPaymentHistory paymentHistroyDis = new UserPaymentHistory
                            {
                                UserProgramOptionId = dto.userProgramOptionId,
                                Amount = (decimal)discount,
                                PaymentReference = PaymentRef,
                                TransactionReference = "Discount",
                                StatusId = PaymentStatusEnums.Paid,
                                PaymentMethodId = PaymentMethodEnums.Discount,
                                PaymentDate = DateTime.Now
                            };
                            _context.UserPaymentHistory.Add(paymentHistroyDis);

                            await _context.SaveChangesAsync();
                        }

                    }
                    else
                    {
                        // Add referral code to DB
                        //------------------------
                        var OwnerId = await _context.users.Where(x => x.ReferralCode == dto.ReferralDiscountCode && x.RegisteredDate.Date.AddMonths(3) >= DateTime.Now.Date).Select(x => x.Id).FirstOrDefaultAsync();
                        if (OwnerId != null && OwnerId != "")
                        {
                            var userRef = new UserReferred()
                            {
                                ReferralId = OwnerId,
                                ReferredUserProgramOptionId = dto.userProgramOptionId,
                                PaymentRef = PaymentRef,
                                Earnings = 0
                            };
                            await _context.UserReferred.AddAsync(userRef);
                        }
                    }
                }
                UserPaymentHistory SubpaymentHistroy = new UserPaymentHistory
                {
                    UserProgramOptionId = dto.userProgramOptionId,
                    Amount = dto.Amount,
                    PaymentReference = PaymentRef,
                    TransactionReference = "",
                    StatusId = PaymentStatusEnums.Initialized,
                    PaymentMethodId = dto.PaymentMethod,
                    PaymentDate = DateTime.Now
                };
                _context.UserPaymentHistory.Add(SubpaymentHistroy);

                await _context.SaveChangesAsync();

                var AmountKobo = dto.Amount * 100;

                MonocoParametersRequest Monorequest = new MonocoParametersRequest
                {
                    amount = AmountKobo.ToString(),
                    description = "Payment with bank connect",
                    type = MonoType,
                    reference = PaymentRef,
                    redirect_url = $"{_apiSettings.PaymentRedirectUrl }"
                };

                var client = new RestClient($"{_apiSettings.MonocoBaseUrl }/v1/payments");
                var request = new RestRequest("initiate", Method.Post);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("mono-sec-key", $"{_apiSettings.MonocoSecretKey }");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", $"{JsonConvert.SerializeObject(Monorequest)}", ParameterType.RequestBody);
                RestResponse response = client.Execute(request);

                MonoPaymentInitializationResponse initDeserializeResponse = new MonoPaymentInitializationResponse();
                if (response.Content != "")
                {
                    initDeserializeResponse = JsonConvert.DeserializeObject<MonoPaymentInitializationResponse>(response.Content);
                    initDeserializeResponse.amount = (decimal.Parse(initDeserializeResponse.amount) / 100).ToString();

                    var respons = new PaymentInitialiseResponse()
                    {
                        checkOutURL = initDeserializeResponse.payment_link,
                        paymentRef = PaymentRef,
                        paymentMethod = Enum.GetName(typeof(PaymentMethodEnums), dto.PaymentMethod)

                    };

                    return new GeneralResponse<PaymentInitialiseResponse>()
                    {
                        Successful = true,
                        Code = StatusCodes.Status200OK.ToString(),
                        Data = respons
                    };

                }

                return null;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw;
            }
        }
        public async Task<GeneralResponse<PaymentInitialiseResponse>> InitializeOfflinePayment(PaymentDTO dto)
        {
            try
            {

                var user = await _context.UserProgramOption.Include(x => x.User).Where(x => x.Id == dto.userProgramOptionId).FirstOrDefaultAsync();

                var subRef = $"{DateTime.Now.Ticks}{(int)dto.PaymentMethod}";

                if (dto.ReferralDiscountCode != "" && dto.ReferralDiscountCode != null)
                {
                    //var disCode = await _context.UserDiscount.Where(x => x.Code == dto.ReferralDiscountCode).FirstOrDefaultAsync();

                    var DiscountOwnerId = await _context.UserDiscount.Where(x => x.Code == dto.ReferralDiscountCode).FirstOrDefaultAsync();
                    if (DiscountOwnerId != null)
                    {
                        if (DiscountOwnerId.TotalApplied < DiscountOwnerId.TotalApproved)
                        {
                            //Add discount details into DB
                            //----------------------------
                            var programPrice = await _context.UserProgramOption.Include(x => x.ProgramOption).Where(x => x.Id == dto.userProgramOptionId).Select(x => x.ProgramOption.PriceNGN).FirstOrDefaultAsync();
                            var discount = (programPrice * DiscountOwnerId.Rate) / 100;
                            DiscountOwnerId.TotalApplied = DiscountOwnerId.TotalApplied + 1;
                            _context.UserDiscount.Update(DiscountOwnerId);

                            if ((float)dto.Amount >= programPrice)
                            {
                                dto.Amount = dto.Amount - (decimal)discount;
                            }

                            //Add to discount history
                            //-----------------------
                            var hist = new DiscountUsageHistory()
                            {
                                Earnings = discount,
                                UsedByProgramOptionId = dto.userProgramOptionId,
                                UserDiscountId = DiscountOwnerId.Id,
                            };

                            await _context.DiscountUsageHistory.AddAsync(hist);

                            UserPaymentHistory paymentHistroyDis = new UserPaymentHistory
                            {
                                UserProgramOptionId = dto.userProgramOptionId,
                                Amount = (decimal)discount,
                                PaymentReference = subRef,
                                TransactionReference = "Discount",
                                StatusId = PaymentStatusEnums.Paid,
                                PaymentMethodId = PaymentMethodEnums.Discount,
                                PaymentDate = DateTime.Now
                            };
                            _context.UserPaymentHistory.Add(paymentHistroyDis);

                            await _context.SaveChangesAsync();
                        }

                    }
                    else
                    {
                        // Add referral code to DB
                        //------------------------
                        var OwnerId = await _context.users.Where(x => x.ReferralCode == dto.ReferralDiscountCode && x.RegisteredDate.Date.AddMonths(3) >= DateTime.Now.Date).Select(x => x.Id).FirstOrDefaultAsync();
                        if (OwnerId != null && OwnerId != "")
                        {
                            var userRef = new UserReferred()
                            {
                                ReferralId = OwnerId,
                                ReferredUserProgramOptionId = dto.userProgramOptionId,
                                PaymentRef = subRef,
                                Earnings = 0
                            };
                            await _context.UserReferred.AddAsync(userRef);
                        }
                    }
                }
                UserPaymentHistory SubpaymentHistroy = new UserPaymentHistory
                {
                    UserProgramOptionId = dto.userProgramOptionId,
                    Amount = dto.Amount,
                    PaymentReference = subRef,
                    TransactionReference = "Offline",
                    StatusId = PaymentStatusEnums.Pending,
                    PaymentMethodId = dto.PaymentMethod,
                    PaymentDate = DateTime.Now
                };
                _context.UserPaymentHistory.Add(SubpaymentHistroy);

                await _context.SaveChangesAsync();

                var response = new PaymentInitialiseResponse()
                {
                    checkOutURL = "Successful",
                    paymentRef = subRef,
                    paymentMethod = Enum.GetName(typeof(PaymentMethodEnums), dto.PaymentMethod)

                };

                return new GeneralResponse<PaymentInitialiseResponse>()
                {
                    Successful = true,
                    Code = StatusCodes.Status200OK.ToString(),
                    Data = response
                };
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw;
            }
        }

        public async Task<GeneralResponse<List<ProgramRecord>>> UserPrograms(string userId)
        {
            //Load user programs
            //------------------
            var result = await _context.UserProgramOption.Include(x => x.ProgramOption).ThenInclude(x => x.Program).ThenInclude(x => x.Category).Where(x => x.UserId == userId).OrderByDescending(x => x.Id).Select(x => new ProgramRecord
            {
                ProgramOptionId = x.ProgramOption.ProgramOptionId,
                userProgramOptionId = x.Id,
                Name = x.ProgramOption.Program.Name + "/" + x.ProgramOption.Name,
                StartDate = x.ProgramOption.StartDate.ToString("dd/MM/yyyy"),
                ProgramStatus = x.ProgramStatus,
                ProgramPaymentStatus = x.PaymentStatus,
                programPrice = x.ProgramOption.PriceNGN.ToString("N")

            }).ToListAsync();

            if (result.Count() > 0)
            {
                foreach (var item in result)
                {
                    var amountPaidperProg = await _context.UserPaymentHistory.Include(m => m.UserProgramOption).Where(m => m.UserProgramOptionId == item.userProgramOptionId && m.StatusId == PaymentStatusEnums.Paid).Select(m => m.Amount).SumAsync();
                    item.amountPaid = amountPaidperProg.ToString("N");
                }

            }
            return new GeneralResponse<List<ProgramRecord>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = result
            };
        }
        public async Task<GeneralResponse<List<ReferralCodeUsage>>> UserReferralCodeUsage(string userId)
        {
            //Load user programs
            //------------------
            var result = await _context.UserReferred.Include(x => x.ReferredUserProgramOption).ThenInclude(x => x.ProgramOption).ThenInclude(x => x.Program).Include(x => x.ReferredUserProgramOption).ThenInclude(x => x.User).Where(x => x.ReferralId == userId).OrderByDescending(x => x.Id).Select(x => new ReferralCodeUsage
            {
                AmountPaid = ((x.Earnings * 100) / 10).ToString("N"),
                DateRegistered = x.ReferredUserProgramOption.RegDate.ToString("dd/MM/yyyy"),
                Earnings = x.Earnings.ToString("N"),
                Fullname = x.ReferredUserProgramOption.User.FirstName + " " + x.ReferredUserProgramOption.User.LastName,
                Program = x.ReferredUserProgramOption.ProgramOption.Program.Name + "/" + x.ReferredUserProgramOption.ProgramOption.Name
                //UserId = x.ReferredUserProgramOption.UserId,
                //Fullname = x.ReferredUserProgramOption.User.FirstName + " " + x.ReferredUserProgramOption.User.LastName,
                //userProOptionId = x.ReferredUserProgramOptionId,
                //earnings = x.Earnings,
                //rCode = x.Referral.ReferralCode

            }).ToListAsync();

            return new GeneralResponse<List<ReferralCodeUsage>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = result
            };
        }
        public async Task<GeneralResponse<List<UserDiscount>>> UserDiscount(string userId)
        {
           
            var resultDis = await _context.UserDiscount.Where(x => x.ReferralId == userId).OrderByDescending(x => x.Id).ToListAsync();

            return new GeneralResponse<List<UserDiscount>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = resultDis
            };
        }
        public async Task<GeneralResponse<ListDiscountHisto>> DiscountHistory(string code, string userId)
        {
            //Load user programs
            //------------------
            var result = await _context.DiscountUsageHistory.Include(x => x.UsedByProgramOption).ThenInclude(x => x.User).Include(x => x.UsedByProgramOption).ThenInclude(x => x.ProgramOption).ThenInclude(x => x.Program).Include(x => x.UserDiscount).Where(x => x.UserDiscount.Code == code && x.UserDiscount.ReferralId== userId).OrderByDescending(x => x.Id).Select(x => new DiscountHisto
            {
                Fullname = x.UsedByProgramOption.User.FirstName + " " + x.UsedByProgramOption.User.LastName,
                Earnings = x.Earnings.ToString("N"),
                DateRegistered = x.UsedByProgramOption.RegDate.ToString("dd/MM/yyyy"),
                Program = x.UsedByProgramOption.ProgramOption.Program.Name + "/" + x.UsedByProgramOption.ProgramOption.Name,
                ProgramFee = x.UsedByProgramOption.ProgramOption.PriceNGN.ToString("N")

            }).ToListAsync();

            var resultDis = new ListDiscountHisto();
            resultDis.DiscountHisto = result;
            resultDis.code = code;

            return new GeneralResponse<ListDiscountHisto>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = resultDis
            };
        }
        public async Task<GeneralResponse<List<PaymentRecord>>> UserPaymentHistories(string userId)
        {
            //Load user programs
            //------------------
            var result = await _context.UserPaymentHistory.Include(x => x.UserProgramOption).ThenInclude(x => x.User).Include(x => x.UserProgramOption).ThenInclude(x => x.ProgramOption).Where(x => x.UserProgramOption.UserId == userId).Select(x => new PaymentRecord
            {
                Amount = x.Amount.ToString("N"),
                PaymentDate = x.PaymentDate,
                PaymentMethod = x.PaymentMethodId,
                Status = x.StatusId,
                programName = x.UserProgramOption.ProgramOption.Program.Name + "/" + x.UserProgramOption.ProgramOption.Name,
                PaymentRef = x.PaymentReference

            }).ToListAsync();

            return new GeneralResponse<List<PaymentRecord>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = result
            };
        }
        public async Task<GeneralResponse<List<UserCertificates>>> UserCertificates(string email)
        {
            //Load user programs
            //------------------
            var result = await _context.UserProgramOption.Include(x => x.User).Include(x => x.ProgramOption).ThenInclude(x => x.Program).ThenInclude(x => x.Category).Where(x => x.User.Email == email).Select(x => new UserCertificates
            {

            }).ToListAsync();

            return new GeneralResponse<List<UserCertificates>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = result
            };
        }
        public async Task<GeneralResponse<List<ProgramRecord>>> UserNewProgramOption(int pOptionId, string userId)
        {
            //Users user = await _userManager.FindByEmailAsync(email);
            var progExist = await _context.UserProgramOption.Where(x => x.ProgramOptionId == pOptionId && x.UserId == userId).Select(x => x.Id).FirstOrDefaultAsync();
            if (progExist <= 0)
            {
                var userPro = new UserProgramOption()
                {
                    ProgramOptionId = pOptionId,
                    UserId = userId,
                    ProgramStatus = UserProgramStatusEnums.Pending,
                    RegDate = DateTime.Now
                };
                await _context.UserProgramOption.AddAsync(userPro);

            }

            if (await _context.SaveChangesAsync() > 0)
            {
                var result = await _context.UserProgramOption.Include(x => x.ProgramOption).ThenInclude(x => x.Program).ThenInclude(x => x.Category).Where(x => x.UserId == userId).OrderByDescending(x => x.Id).Select(x => new ProgramRecord
                {
                    ProgramOptionId = x.ProgramOption.ProgramOptionId,
                    userProgramOptionId = x.Id,
                    Name = x.ProgramOption.Program.Name + "/" + x.ProgramOption.Name,
                    StartDate = x.ProgramOption.StartDate.ToString("dd/MM/yyyy"),
                    ProgramStatus = x.ProgramStatus,
                    ProgramPaymentStatus = x.PaymentStatus,
                    programPrice = x.ProgramOption.PriceNGN.ToString("N")

                }).ToListAsync();

                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        var amountPaidperProg = await _context.UserPaymentHistory.Include(m => m.UserProgramOption).Where(m => m.UserProgramOptionId == item.userProgramOptionId && m.StatusId == PaymentStatusEnums.Paid).Select(m => m.Amount).SumAsync();
                        item.amountPaid = amountPaidperProg.ToString("N");
                    }

                }

                return new GeneralResponse<List<ProgramRecord>>()
                {
                    Successful = true,
                    Code = StatusCodes.Status200OK.ToString(),
                    Data = result
                };
            }

            return null;
        }
        public async Task<GeneralResponse<List<ProgramCategory>>> GetProgramCat()
        {

            //Load Program categories from DB
            //-------------------------------
            var programCategoryList = await _context.ProgramCategory.ToListAsync();
            return new GeneralResponse<List<ProgramCategory>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = programCategoryList
            };
        }
        public async Task<GeneralResponse<List<ProgramRecord>>> DeleteUserProgramOption(int Id, string userId)
        {
            var result = await _context.UserProgramOption.Where(x => x.Id == Id && x.UserId == userId).FirstOrDefaultAsync();
            if (result != null)
            {
                _context.UserProgramOption.Remove(result);
            }
            await _context.SaveChangesAsync();

            var result1 = await _context.UserProgramOption.Include(x => x.ProgramOption).ThenInclude(x => x.Program).ThenInclude(x => x.Category).Where(x => x.UserId == userId).OrderByDescending(x => x.Id).Select(x => new ProgramRecord
            {
                ProgramOptionId = x.ProgramOption.ProgramOptionId,
                userProgramOptionId = x.Id,
                Name = x.ProgramOption.Program.Name + "/" + x.ProgramOption.Name,
                StartDate = x.ProgramOption.StartDate.ToString("dd/MM/yyyy"),
                ProgramStatus = x.ProgramStatus,
                ProgramPaymentStatus = x.PaymentStatus,
                programPrice = x.ProgramOption.PriceNGN.ToString("N")

            }).ToListAsync();

            if (result1.Count() > 0)
            {
                foreach (var item in result1)
                {
                    var amountPaidperProg = await _context.UserPaymentHistory.Include(m => m.UserProgramOption).Where(m => m.UserProgramOptionId == item.userProgramOptionId && m.StatusId == PaymentStatusEnums.Paid).Select(m => m.Amount).SumAsync();
                    item.amountPaid = amountPaidperProg.ToString("N");
                }

            }

            return new GeneralResponse<List<ProgramRecord>>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = result1
            };
            // return "Done";
        }
        public async Task<GeneralResponse<float>> GetDiscountRate(string code, string userId)
        {
            var discountRate = await _context.UserDiscount.Where(x => x.Code == code && x.ReferralId == userId).Select(x => x.Rate).FirstOrDefaultAsync();

            return new GeneralResponse<float>()
            {
                Successful = true,
                Code = StatusCodes.Status200OK.ToString(),
                Data = discountRate
            };
        }
    }
}