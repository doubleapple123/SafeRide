using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using SafeRide.src.Models;
using SafeRide.src.Interfaces;
using SafeRide.src.Services;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace SafeRide.src.Controllers;

    [Route("/api")]
    public class RouteHistoryController : Controller
    {
        private ISaveRouteService _iRouteService;
        private IRouteInformationDAO _routeInfoDao;
        private string api_key = "";

        public RouteHistoryController(IRouteInformationDAO iRouteInfoDao, ISaveRouteService iSaveRouteService)
        {
            _routeInfoDao = iRouteInfoDao;
            _iRouteService = iSaveRouteService;
        }

        [HttpGet]
        [Route("routeinfo/getinfo")]
        public async Task<IActionResult> GetRouteInformation (string userName)
        {
            // var routeInfo = RouteService.GetAllRoutes(userName);

            return Ok(new { routeInfo });

        }
    }
