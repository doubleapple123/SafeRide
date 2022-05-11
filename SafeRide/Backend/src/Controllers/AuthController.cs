using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Models;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web.Http;
using SafeRide.src.Interfaces;
using SafeRide.src.Security.Interfaces;
using System.Web.Http;
using AuthorizeAttribute = Backend.Attributes.AuthorizeAttribute.AuthorizeAttribute;
using SafeRide.src.Security.UserSecurity;
using Backend.src.Services.Security.UserSecurity;

namespace SafeRide.src.Services
{
    [Microsoft.AspNetCore.Mvc.Route("api")]
    [Controller]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;
        private readonly IOTPService otpService;
        private readonly IEmailService emailService;
        private static OTP _otp;
        private static UserSecurityModel _user;


        private string generatedToken = null;

        private readonly string SECRET_KEY = "this is my custom Secret key for authnetication"; //needs many characters
        private readonly string ISSUER = "www.saferide.net";

        public AuthController(ITokenService tokenService, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
            this.otpService = new OTPService();
            this.emailService = new EmailService();
            
            //this.otpService = otpService;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("verifyOTP")]
        public IActionResult VerifyOTP([FromUri] string otpPassphrase)  {

            IActionResult response = Unauthorized();
            //string otpPassphrase = otp.Passphrase;
            if (otpService.ValidateOTP(_otp, otpPassphrase))
            {
                try
                {
                    generatedToken = tokenService.BuildToken(SECRET_KEY, ISSUER, _user);
                    if (generatedToken != null)
                    {
                        response = Ok(new { token = generatedToken });
                    }
                }
                catch (Exception ex)
                {
                    response = Unauthorized();
                }
            }
            return response;
        }



        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("login")]
        public IActionResult Login([Microsoft.AspNetCore.Mvc.FromBody] UserSecurityModel user)
        {
            _user = user;
            IActionResult response = Unauthorized();
            var valid = true;
            UserSecurityModel validUser = null;

            //try
            //{
            //    validUser = this.userRepository.GetUser(user);
            //    _user = validUser;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    valid = false;
            //}

            //if (valid && validUser != null)
            //{

            try
            {
                otpService.GenerateOTP();
                _otp = otpService.GetOTP();
                bool success = emailService.SendOTP(user.Email, _otp);

                if (success)
                {
                    string message = "A temporary One-Time password has been sent to your email. Please verify the your account by entering the OTP that was sent to: ";
                    Console.WriteLine(message);
                    response = Ok(new { message });
                }
            }

            catch (Exception ex)
            {
                response = Unauthorized();
            }
            //  }
            return response;
        }


        [AuthorizeAttribute.ClaimRequirementAttribute("role", "admin")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("verifyToken")]
        public IActionResult VerifyToken([FromHeader] string authorization)
        {
            authorization = authorization.Replace("Bearer ", "");
            authorization = authorization.Replace("\"", "");
            try
            {
                JwtDecoder.DecodeJwt(authorization);
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}

