using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using SafeRide.src.Models;
using SafeRide.src.Interfaces;
using SafeRide.src.Services;
using System.Net.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace SafeRide.src.Controllers;

    [Route("/api")]
    public class RouteHistoryController : Controller
    {
        private IRouteInformationDAO _iRouteInfoDao;

        public RouteHistoryController(IRouteInformationDAO iRouteInfoDao)
        {
            _iRouteInfoDao = iRouteInfoDao;
        }

        [HttpGet]
        [Route("routeinfo/getinfo")]
        public async Task<IActionResult> GetRouteInformation (string request)
        {
            // var routeInfo = RouteService.GetAllRoutes(userName);
            var jsonResponses = new List<string>();
            var recentRoutes = _iRouteInfoDao.getRouteHistory("Orange", "recentSearches");
            HttpClient client = new HttpClient();
            foreach (var route in recentRoutes)
            {
                HttpResponseMessage response = await client.GetAsync(route);
                response.EnsureSuccessStatusCode();
                string jResponse = await response.Content.ReadAsStringAsync();
                jsonResponses.Add(jResponse); 
            }
            return Ok(new { jsonResponses });

        }
    }
