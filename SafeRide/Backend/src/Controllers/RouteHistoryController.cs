using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using Backend.src.Models;
using Backend.src.Interfaces;
using Backend.src.Services;
using Backend.Services;

namespace Backend.src.Controllers;

    [ApiController]
    public class RouteHistoryController : Controller
    {
        private SavedRouteService RouteService;
        private IRouteInformationDAO _routeInfoDao;

        public RouteHistoryController(IRouteInformationDAO routeInfoDao)
        {
            _routeInfoDao = routeInfoDao;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/routeinfo/getinfo")]
        public IActionResult GetRouteInformation ([FromUri] string userName)
        {
            // var user = JwtDecoder.GetUser(authorization);
            // if (user == null) return Unauthorized();

            var routeInfo = RouteService.GetAllRoutes(userName);
            return Ok(new { routeInfo });

        }
    }
