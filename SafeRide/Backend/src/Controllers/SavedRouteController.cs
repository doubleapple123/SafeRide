using System.Web.Http;
using Backend.src.Services;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Interfaces;
using AuthorizeAttribute = Backend.Attributes.AuthorizeAttribute.AuthorizeAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Backend.Controllers;

public class SavedRouteController : ControllerBase
{
    private ISavedRouteDAO _savedRoutesDAO;
    private SavedRouteService _routeService;

    public SavedRouteController(ISavedRouteDAO savedRouteDAO)
    {
        _savedRoutesDAO = savedRouteDAO;
    }

    [HttpPost]
    [Route("/api/add")]
    public IActionResult AddSavedRoute([FromUri] string routeName)
    {
        
        return Ok(new { routeName });
    }

    [HttpGet]
    [Route("/api/getAllRoutes")]
    public IActionResult GetAllSavedRoutes()
    {

        return Ok(new { });
    
    }
}