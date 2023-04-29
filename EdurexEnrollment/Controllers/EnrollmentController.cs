using EdurexEnrollment.Core.DTO;
using EdurexEnrollment.Core.Enums;
using EdurexEnrollment.Core.Interface;
using EdurexEnrollment.Core.Model;
using EdurexEnrollment.Core.ResponseVM;
using EdurexEnrollment.Core.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EdurexEnrollment.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/[controller]")]
    public class EnrollmentController : Controller
    {
        private readonly ILogger<EnrollmentController> _logger;
        private readonly IProjectServices _projectServices;

        public EnrollmentController(ILogger<EnrollmentController> logger, IProjectServices projectServices)
        {
            _logger = logger;
            _projectServices = projectServices;
        }

        /// <summary>
        /// Get all program categories
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<ProgramCategory>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("programCategories")]
        [AllowAnonymous]
        public async Task<IActionResult> AllProgramCategories()
        {
            try
            {
                var result = await _projectServices.GetProgramCategories();
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all program by categoryId
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<Programs>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [AllowAnonymous]
        [Route("program/{categoryId}")]
        public async Task<IActionResult> AllProgramsbyCategoryId(int categoryId)
        {
            try
            {
                var result = await _projectServices.GetProgramsByCategoryId(categoryId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all program options by programId
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<ProgramOptions>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("programOptions/{programId}")]
        [AllowAnonymous]

        public async Task<IActionResult> programOptions(int programId)
        {
            try
            {
                var result = await _projectServices.GetProgramOptionsByProgramId(programId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all program options price details by programOptionId
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<ProgramOptionPriceVM>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("programOptionPriceDetails/{programOptionId}")]
        [AllowAnonymous]

        public async Task<IActionResult> programOptionPriceDetails(int programOptionId)
        {
            try
            {
                var result = await _projectServices.GetPriceByProgramOptionId(programOptionId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all subjects by programOptionId
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<Subjects>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("Subjects/{programOptionId}")]
        [AllowAnonymous]

        public async Task<IActionResult> Subjects(int programOptionId)
        {
            try
            {
                var result = await _projectServices.GetProgramSubjectssByOptionId(programOptionId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all countries
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<countriesbyCountry>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("Countries")]
        [AllowAnonymous]

        public async Task<IActionResult> Countries()
        {
            try
            {
                var result = await _projectServices.GetCountries();
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all states by countryId
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<states>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("States/{countryId}")]
        [AllowAnonymous]

        public async Task<IActionResult> States(int countryId)
        {
            try
            {
                var result = await _projectServices.GetStatesByCountryId(countryId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all cities by stateId
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<Core.Utilities.Cities>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("Cities/{stateId}")]
        [AllowAnonymous]

        public async Task<IActionResult> Cities(int stateId)
        {
            try
            {
                var result = await _projectServices.GetCitiesByStateId(stateId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all streets by cityId
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<Core.Utilities.Cities>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("Streets/{cityId}")]
        [AllowAnonymous]

        public async Task<IActionResult> Streets(int cityId)
        {
            try
            {
                var result = await _projectServices.GetStreetsByCityId(cityId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all institutions
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<Institutions>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("Institutions")]
        [AllowAnonymous]

        public async Task<IActionResult> Institutions()
        {
            try
            {
                var result = await _projectServices.GetInstitutions();
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all courses
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<Courses>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("Courses")]
        [AllowAnonymous]

        public async Task<IActionResult> Courses()
        {
            try
            {
                var result = await _projectServices.GetCourses();
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// All user's program options
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<ProgramRecord>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("userProgramOptions")]
        public async Task<IActionResult> userProgramOptions()
        {
            try
            {
                string userId = User.FindFirst("id").Value;
                if (userId == null)
                {
                    return Unauthorized();
                }
                var result = await _projectServices.UserPrograms(userId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Add new program to user's program
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <param name="programOptionId"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<ProgramRecord>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("newUserProgramOption/{programOptionId}")]
        public async Task<IActionResult> newUserProgramOption([FromBody] int programOptionId)
        {
            try
            {
                string userId = User.FindFirst("id").Value;
                if (userId == null)
                {
                    return Unauthorized();
                }
                var result = await _projectServices.UserNewProgramOption(programOptionId, userId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Delete user program option
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<ProgramRecord>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("DeleteProgramOption/{id}")]
        public async Task<IActionResult> DeleteProgramOption(int id)
        {
            try
            {
                string userId = User.FindFirst("id").Value;
                if (userId == null)
                {
                    return Unauthorized();
                }
                var result = await _projectServices.DeleteUserProgramOption(id, userId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// User's referral code usage
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<ReferralCodeUsage>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("userReferralCodeUsage")]
        public async Task<IActionResult> userReferralCodeUsage()
        {
            try
            {
                string userId = User.FindFirst("id").Value;
                if (userId == null)
                {
                    return Unauthorized();
                }
                var result = await _projectServices.UserReferralCodeUsage(userId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// User's discount
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<UserDiscount>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("UserDiscount")]
        public async Task<IActionResult> UserDiscount()
        {
            try
            {
                string userId = User.FindFirst("id").Value;
                if (userId == null)
                {
                    return Unauthorized();
                }
                var result = await _projectServices.UserDiscount(userId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// User's discount usage
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <param name="code"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<ListDiscountHisto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("UserDiscountUsage/{code}")]
        public async Task<IActionResult> UserDiscountUsage(string code)
        {
            try
            {
                string userId = User.FindFirst("id").Value;
                if (userId == null)
                {
                    return Unauthorized();
                }
                var result = await _projectServices.DiscountHistory(code,userId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Getting discount rate by discount code
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<float>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("DiscountRate/{discountCode}")]
        public async Task<IActionResult> DiscountRate(string discountCode)
        {
            try
            {
                string userId = User.FindFirst("id").Value;
                if (userId == null)
                {
                    return Unauthorized();
                }
                var result = await _projectServices.GetDiscountRate(discountCode, userId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// User's payment history
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<List<PaymentRecord>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("UserPaymentHistory")]
        public async Task<IActionResult> UserPaymentHistory()
        {
            try
            {
                string userId = User.FindFirst("id").Value;
                if (userId == null)
                {
                    return Unauthorized();
                }
                var result = await _projectServices.UserPaymentHistories(userId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Make Payment
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<PaymentInitialiseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("Make Payment")]
        public async Task<IActionResult> MakePayment([FromBody]PaymentDTO dto)
        {
            try
            {
                string userId = User.FindFirst("id").Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                //var paymentResponse = new PaymentInitialiseResponse();
                PaymentMethodEnums paymentMethod = (PaymentMethodEnums)dto.PaymentMethod;
                switch (paymentMethod)
                {
                    case PaymentMethodEnums.Card:
                        var cardResponse = await _projectServices.InitializeCardPayment(dto);
                        return Ok(cardResponse);//Ok(new Response { IsSuccessful = true, Message = StatusMessage.OK, Code = ResponseCode.OK, Data = cardResponse });

                    case PaymentMethodEnums.AccountTransfer:
                        var transferResponse = await _projectServices.InitializeAccountTransferPayment(dto);
                        return Ok(transferResponse);//Ok(new Response { IsSuccessful = true, Message = StatusMessage.OK, Code = ResponseCode.OK, Data = transferResponse });

                    case PaymentMethodEnums.BankConnect:
                        var BankConnectResponse = await _projectServices.InitializeBankCOnnectPayment(dto);
                        return Ok(BankConnectResponse);// Ok(new Response { IsSuccessful = true, Message = StatusMessage.OK, Code = ResponseCode.OK, Data = BankConnectResponse });
                    case PaymentMethodEnums.Offline:
                        var OfflineResponse = await _projectServices.InitializeOfflinePayment(dto);
                        return Ok(OfflineResponse);// Ok(new Response { IsSuccessful = true, Message = StatusMessage.OK, Code = ResponseCode.OK, Data = BankConnectResponse });

                }
                throw new Exception("No payment method found");                
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Finalise payment
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <param name="paymentRef"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<PaymentStatusResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("FinalisePayment/{paymentRef}")]

        public async Task<IActionResult> FinalisePayment(string paymentRef)
        {
            try
            {
                string userId = User.FindFirst("id").Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _projectServices.QueryBankCOnnectPayment(paymentRef);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// SignUp
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<PaymentInitialiseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("SignUp")]
        [AllowAnonymous]

        public async Task<IActionResult> SignUp([FromBody] RegisterDTO dto)
        {
            try
            {

                var result = await _projectServices.RegisterUser(dto);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<AuthenticationToken>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]

        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {

                var result = await _projectServices.Login(dto);
                if(result != null)
                {
                    return Ok(result);
                }

                return BadRequest(new ErrorResponseDTO(HttpStatusCode.NotFound,
                                                              new string[] { "No record found"}));
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Get dashboard details
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Not Found</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(GeneralResponse<DashboardVM>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            try
            {
                string userId = User.FindFirst("id").Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _projectServices.DashboardRe(userId);
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { argex.Message }));
            }
            catch (UnauthorizedAccessException argex)
            {
                return BadRequest(new ErrorResponseDTO(HttpStatusCode.Unauthorized,
                                                              new string[] { argex.Message }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
