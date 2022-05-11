using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using SafeRide.src.Models;
using SafeRide.src.Interfaces;
using SafeRide.src.Services;

namespace SafeRide.src.Controllers;

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
