﻿using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using Backend.src.Models;
using Backend.src.Interfaces;
using Backend.Services;

namespace Backend.src.Controllers
{
    [ApiController]
    public class RouteHistoryController : Controller
    {
        private IRouteInformationDAO _routeInfoDao;

        public RouteHistoryController(IRouteInformationDAO routeInfoDao)
        {
            _routeInfoDao = routeInfoDao;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/routeinfo/getinfo")]
        public IActionResult GetRouteInformation([FromHeader] string authorization)
        {
            var user = JwtDecoder.GetUser(authorization);
            if (user == null) return Unauthorized();

            var routeInfo = _routeInfoDao.GetRouteInformation(user);
            return Ok(new {routeInfo});
        }
    }
}