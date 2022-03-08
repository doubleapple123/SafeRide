﻿using Backend.Attributes.AuthorizeAttribute;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Models;
using System.Net.Http.Headers;
using System.Security.Claims;
using SafeRide.src.Interfaces;
using SafeRide.src.Security.Interfaces;

namespace SafeRide.src.Services
{
    [Route("api")]
    [Controller]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;

        private readonly IOTPService otpService;
        
        private string generatedToken = null;

        private readonly string SECRET_KEY = "this is my custom Secret key for authnetication"; //needs many characters
        private readonly string ISSUER = "www.saferide.net";

        public AuthController(ITokenService tokenService, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
            //this.otpService = otpService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UserSecurityModel user)
        {
            IActionResult response = Unauthorized();
            var valid = true;
            UserSecurityModel validUser = null;
            try
            {
                validUser = this.userRepository.GetUser(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                valid = false;
            }

            if (valid && validUser != null)
            {
                //promt user on frontend to input their otp
                otpService.SetUser(validUser);
                // continue calling ValidateOTP until user successfuly completes validation 
                while (otpService._isValidated == false) {
                    // stop validating if the user has reached the 24hr limit
                    if (_disableAcct) {
                        Console.WriteLine("User has failed 5 consecutive authentication attempts in the last 24hrs. Account must be disabled");
                        // TODO: figure out how to disable the account at this point
                        break; 
                    }
                    else {
                        otpService.SendEmail();
                        //ValidateOTP();
                    }
                }
                generatedToken = tokenService.BuildToken(SECRET_KEY, ISSUER, validUser);

                if (generatedToken != null)
                {
                    response = Ok(new { token = generatedToken });
                }
            }
            return response;
        }
        
        
        [HttpPost]
        [Route("otp")]
        public IActionResult Validate([FromBody] UserSecurityModel user, [FromUri] string providedOTP)
        {
            IActionResult response = Unauthorized();
            if (otpService.ValidateOTP(providedOTP)) {
                response = Ok();
            return response;
            }
        }
        
        [AuthorizeAttribute.ClaimRequirementAttribute("role", "admin")]
        [HttpPost]
        [Route("verifyToken")]
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
