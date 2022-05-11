using System.Web.Http;
using SafeRide.src.Services;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Models;
using SafeRide.src.Interfaces;
using AuthorizeAttribute = Backend.Attributes.AuthorizeAttribute.AuthorizeAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;

namespace Backend.Controllers;

[Route("/api")]
public class SaveARouteController : ControllerBase
{
    private ISaveARoute _iSaveARouteDAO;
    private ISaveARouteService _routeService;
    private string api_key = "pk.eyJ1IjoiY2FudGRyaW5rbWlsayIsImEiOiJjbDAwZnFiOHkwM3kyM3FwaG1qcmFhazh6In0.ytVFjAsRLDJra61yH0ZT-w";

    public SaveARouteController(ISaveARoute iSaveARouteDAO, ISaveARouteService iSaveARouteService)
    {
        _iSaveARouteDAO = iSaveARouteDAO;
        _routeService = iSaveARouteService;
    }
    

    /*
     Summary
     Adds a saved route that the user request
     @param startLon start longitude
     @param startLat start lattitude
     @param endLon end longitude
     @param endLat end lattitude
     
    @return returns success messsage if user is successful
     */
    [HttpPost]
    [Route("addSaveRoute")]
    public async Task<IActionResult> AddSavedRoute(string startLon, string startLan, string endLon, string endLan)
    {
        // create string request from user coordinates
        var requestURL  = "https://api.mapbox.com/directions/v5/mapbox/cycling/${startLon},${startLan};${endLon},${endLan}?steps=true&geometries=geojson&access_token=${api_key}";

        //set string in coords to send to service
        // List<string> setOfCoords = new List<string>{ startLon, startLan, endLon, endLan};

        // create user with set of coords to store in database
        // var user = new User("apple@gmail.com", setOfCoords );

        //Service to add route in database store
        if (_routeService.AddSavedRoute("apple@gmail.com", 1, requestURL, "userRoute") == 1) 
        {
            string successMessage = "Route successfully added.";
            return Ok(new { successMessage});
        }

        string failMessage = "Route save unsuccessful";

        
        return BadRequest(new { failMessage });
    }
   /**
    * Summary
    * API GET
    * API call to get saved routes
    * saved routes are routes that are distinctively saved by the userwww
    * 
    * return List of JsonResponse for map route building
    */

    [HttpGet]
    [Route("getSavedRoutes")]
    public async Task<IActionResult> GetSavedRoutes(string userRequest) 
    {
        var listOfJsonResponse = new List<string>();
        var userRoutes = _iSaveARouteDAO.GetSavedRoutes("apple@gmail.com", "userRoutes");


        HttpClient client = new();

        foreach (var userCoords in  userRoutes)
        {
            
            HttpResponseMessage response = await client.GetAsync(userCoords);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            listOfJsonResponse.Add(jsonResponse);

            
        }

        return Ok( new { listOfJsonResponse });
    
    }

    /**
     * 
     * Summary 
     * API call to get recent routes
     * Recent routes are routes that are saved when user makes a search
     * 
     * 
     * return List of JsonResponse for map route building
     */
    [HttpGet]
    [Route("getRecentRoutes")]
    public async Task<IActionResult> GetRecentRoutes(string userRequest)
    {
        var listOfJsonResponse = new List<string>();
        var userRoutes = _iSaveARouteDAO.GetSavedRoutes("apple@gmail.com", "userTempRoutes");


        HttpClient client = new HttpClient();

        foreach (var userCoords in userRoutes)
        {

            HttpResponseMessage response = await client.GetAsync(userCoords);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            listOfJsonResponse.Add(jsonResponse);


        }

        return Ok(new { listOfJsonResponse });

    }
}