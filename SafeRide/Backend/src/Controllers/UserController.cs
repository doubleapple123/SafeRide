using System.Text.RegularExpressions;
using System.Web.Http;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using AuthorizeAttribute = Backend.Attributes.AuthorizeAttribute.AuthorizeAttribute;

namespace SafeRide.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly UserManagementService _UMservice;
        private string generatedToken = null;
        /*
        private IUserSecurityDAO _userSecurityDao;
        */

        private readonly string SECRET_KEY = "this is my custom Secret key for authnetication"; //needs many characters
        private readonly string ISSUER = "www.saferide.net";
        private readonly string MAPBOX_API_KEY = "pk.eyJ1IjoiYXBwbGVmdSIsImEiOiJja3p5dWV1eTkwM3gyM2lteGZqZGszNTBjIn0.CLc4mochtSCflbpW9BPH4Q";

        public UserController(IUserSecurityDAO _userSecurityDao)
        {
            this.tokenService = new TokenService();
            _UMservice = new UserManagementService(_userSecurityDao);
        }
        [Microsoft.AspNetCore.Mvc.Route("createUser")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult CreateUser([Microsoft.AspNetCore.Mvc.FromBody] UserSecurityModel user
            //,[FromUri]string passphrase
            )
        {
            user.Role = "user";
            user.Valid = true;
            
            IActionResult response = BadRequest();
            if (_UMservice.CreateUser(user))
            {
                /*if (!Regex.IsMatch(user.Email, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$"))
                    return response;
                if (Regex.IsMatch(passphrase, "^[a-zA-Z0-9.,@!]*$") && passphrase.Length > 8)
                    return response;
                else*/
                response = Ok(response);
            }

            return response;
        }
     
        
        [AuthorizeAttribute.ClaimRequirementAttribute("role", "admin")]
        [Microsoft.AspNetCore.Mvc.Route("updateUser")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult UpdateUser([FromUri] string username, [Microsoft.AspNetCore.Mvc.FromBody] UserSecurityModel user)
        {
            IActionResult response = BadRequest();
            
            if (_UMservice.UpdateUser(username, user))
            {
                response = Ok();
            }
            

            return response;
        }
        
        [AuthorizeAttribute.ClaimRequirementAttribute("role", "admin")]
        [Microsoft.AspNetCore.Mvc.Route("deleteUser")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult DeleteUser([FromUri] string username)
        {
            IActionResult response = BadRequest();

            if (_UMservice.DeleteUser(username))
            {
                response = Ok();
            }

            return response;
        }
        
        [AuthorizeAttribute.ClaimRequirementAttribute("role", "admin")]
        [Microsoft.AspNetCore.Mvc.Route("disableAccount")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult DisableAccount([FromUri] string username)
        {
            IActionResult response = BadRequest();

            if (_UMservice.DisableAccount(username))
            {
                response = Ok();
            }

            return response;
        }
        
        [AuthorizeAttribute.ClaimRequirementAttribute("role", "admin")]
        [Microsoft.AspNetCore.Mvc.Route("enableAccount")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult EnableAccount([FromUri] string username)
        {
            IActionResult response = BadRequest();
            
            if (_UMservice.EnableAccount(username))
            {
                response = Ok();
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
