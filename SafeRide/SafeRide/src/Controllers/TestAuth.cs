﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SafeRide.Controllers
{
    [Route("api/testing")]
    [ApiController]
    public class TestAuth : ControllerBase
    {
        [Authorize(Roles = "test")]
        [HttpGet]
        [Route("test1")]
        public IActionResult Test1()
        {
            return Ok();
        }

        [Authorize()]
        [HttpGet]
        [Route("test2")]
        public IActionResult Test2()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("test3")]
        public IActionResult Test3()
        {
            return Ok();
        }
    }
}
