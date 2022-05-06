using System.Web.Http;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using AuthorizeAttribute = Backend.Attributes.AuthorizeAttribute.AuthorizeAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Backend.Controllers;

public class SavedRouteController : ControllerBase
{
    private ISavedRoutesDAO _savedRoutesDAO;

    public SavedRouteController(ISavedRoutesDAO savedRouteDAO)
    {
        _savedRoutesDAO = savedRouteDAO;
    }

    [HttpPost]
    [Route("/api/add")]
    public IActionResult GetSavedRoute([FromUri] string routeName)
    {
        var routeList = RouteService.GetAllRoutes(user)
        return Ok(new { routeList });
    }
}