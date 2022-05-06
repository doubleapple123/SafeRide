using System.Text.Json;
using System.Web.Http;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Interfaces;

namespace Backend.Controllers;

[ApiController]
public class SavedRouteController : ControllerBase
{
    private SavedRouteService RouteService;
    private ISavedRouteDAO _savedRouteDao;

    public SavedRouteController(ISavedRouteDAO savedRouteDao)
    {
        _savedRouteDao = savedRouteDao;
        RouteService = new SavedRouteService(_savedRouteDao);
    }

    [Microsoft.AspNetCore.Mvc.HttpGet]
    [Microsoft.AspNetCore.Mvc.Route("/api/route/share")]
    public IActionResult ShareRoute([FromHeader] string authorization, [FromUri] string routeName)
    {
        var user = JwtDecoder.GetUser(authorization);
        if (user == null) return Unauthorized();

        var jsonStr = RouteService.GetSavedRoute(user, routeName);
        var encryptedString = RouteService.EncryptRoute(jsonStr);

        return Ok(new {encryptedString});
    }

    [Microsoft.AspNetCore.Mvc.HttpGet]
    [Microsoft.AspNetCore.Mvc.Route("/api/route/decrypt")]
    public IActionResult DecodeRoute([FromUri] string cipher)
    {
        try
        {
            var decrypted = RouteService.DecryptRoute(cipher);
            return Ok(new {decrypted});
        }
        catch
        {
            return BadRequest();
        }
    }

    [Microsoft.AspNetCore.Mvc.HttpPost]
    [Microsoft.AspNetCore.Mvc.Route("/api/route/add")]
    public IActionResult AddSavedRoute([FromHeader] string authorization, [FromUri] string routeName, [Microsoft.AspNetCore.Mvc.FromBody] Object routes)
    {
        var user = JwtDecoder.GetUser(authorization);
        if (user == null) return Unauthorized();
        var encodedJson = JsonSerializer.Serialize(routes);
        
        if (RouteService.AddSavedRoute(user, routeName, encodedJson))
        {
            return Ok();
        }

        return BadRequest();
    }
    
    [Microsoft.AspNetCore.Mvc.HttpGet]
    [Microsoft.AspNetCore.Mvc.Route("/api/route/get")]

    public IActionResult GetSavedRoute([FromHeader] string authorization, [FromUri] string routeName)
    {
        var user = JwtDecoder.GetUser(authorization);
        if (user == null) return Unauthorized();

        var json = RouteService.GetSavedRoute(user, routeName);
        
        return Ok(new {json});
    }

    [Microsoft.AspNetCore.Mvc.HttpGet]
    [Microsoft.AspNetCore.Mvc.Route("/api/route/all")]
    public IActionResult GetAllSavedRoutes([FromHeader] string authorization)
    {
        var user = JwtDecoder.GetUser(authorization);
        if (user == null) return Unauthorized();

        var routeList = RouteService.GetAllRoutes(user);

        return Ok(new {routeList});
    }
}