using System.Text.RegularExpressions;
using System.Web.Http;
using Backend.src.Services.Security.UserSecurity;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.Security.Interfaces;
using SafeRide.src.Security.UserSecurity;
using AuthorizeAttribute = Backend.Attributes.AuthorizeAttribute.AuthorizeAttribute;

namespace SafeRide.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private string generatedToken = null;
        private IUserSecurityDAO _userSecurityDao;
        private readonly IOTPService otpService;
        private readonly IEmailService emailService;
        private static OTP _otp;
        private static UserSecurityModel _user;

        private readonly string SECRET_KEY = "this is my custom Secret key for authnetication"; //needs many characters
        private readonly string ISSUER = "www.saferide.net";
        private readonly string MAPBOX_API_KEY = "pk.eyJ1IjoiYXBwbGVmdSIsImEiOiJja3p5dWV1eTkwM3gyM2lteGZqZGszNTBjIn0.CLc4mochtSCflbpW9BPH4Q";

        public UserController(IUserSecurityDAO userSecurityDao)
        {
            _userSecurityDao = userSecurityDao;
            this.tokenService = new TokenService();
            this.otpService = new OTPService();
            this.emailService = new EmailService();
        }


        [Microsoft.AspNetCore.Mvc.Route("createUser")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult CreateUser([Microsoft.AspNetCore.Mvc.FromBody] UserSecurityModel user, [FromUri] string passphrase)
        {
            user.Role = "user";
            user.Valid = true;
            _user = user;
            string message = "";

            IActionResult response = BadRequest();
            //if (_userSecurityDao.Create(user))
            //{
            try
            {

                // validate email
                if (!Regex.IsMatch(user.Email, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
            + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$"))
                {
                    message = "Account Creation Failed - Invalid Email Address";
                }

                // validate password
                if (!Regex.IsMatch(passphrase, "^[a-zA-Z0-9.,@!]*$") && passphrase.Length > 8)
                {
                    message = "Account Creation Failed - Invalid Password \n Your password must be a minimum of 8 characters. Passpwords can only contain blank spaces, letters a-z and A-Z, numbers 0-9, or characters .,@1";

                }

                else
                {
                    otpService.GenerateOTP();
                    _otp = otpService.GetOTP();
                    bool success = emailService.SendOTP(user.Email, _otp);

                    if (success)
                    {
                        message = "A temporary One-Time password has been sent to the email provided. Please enter your unique OTP to verify your email and complete account creation. Note: This is a temporary passphrase that will expire 2 minutes after being sent.";
                        //Console.WriteLine(message);
                    }
                }
                response =  Ok(new { message });
            }

            catch (Exception ex)
            {
                response = BadRequest();
            }
        //}
            return response;
        }   

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("verifyEmail")]
        public IActionResult VerifyEmail([FromUri] string otpPassphrase)
        {

            IActionResult response = Unauthorized();
            string message = "";
            //string otpPassphrase = otp.Passphrase;
            try {

                if (otpService.ValidateOTP(_otp, otpPassphrase))
                {
                    message = "Email verification complete";

                    // only write the new user to database once their OTP has been verified
                    if (_userSecurityDao.Create(_user))
                    {

                        generatedToken = tokenService.BuildToken(SECRET_KEY, ISSUER, _user);
                        if (generatedToken != null)
                        {
                            message = $"Email verification complete - Account creation successful. User is now logged in with the active authentication token: \n {generatedToken}";
                        }
                    }
                    else
                    {
                        message = "Account Creation Failed - User credentials cannot be written to the internal database at this time";
                    }
                }
                else
                {
                    message = "Account creation failed - The OTP provided does not match the OTP that was sent out. Please submit your credentials again to retry.";
                }
                response = Ok(new { message });
            }
            catch (Exception ex)
            {
                response = Unauthorized();
            }
            return response;
        }


        [AuthorizeAttribute.ClaimRequirementAttribute("role", "admin")]
        [Microsoft.AspNetCore.Mvc.Route("updateUser")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult UpdateUser([FromUri] string username, [Microsoft.AspNetCore.Mvc.FromBody] UserSecurityModel user)
        {
            IActionResult response = BadRequest();

            if (!username.Equals(user.UserName))
            {
                response = BadRequest("username must be equal");
            }
            else
            {
                if (_userSecurityDao.Update(username, user))
                {
                    response = Ok();
                }
            }

            return response;
        }
        /*[Route("getUser")]
        [HttpGet]
        public IActionResult GetUser([FromBody] string token, [FromBody] string userId)
        {
            UserModel thisUser = userDAO.Read(userId);

            IActionResult response = Unauthorized();

            if (this.tokenService.IsTokenValid(SECRET_KEY, ISSUER, token)) return Ok(new {  });


            return response;
        }*/
    }
}
