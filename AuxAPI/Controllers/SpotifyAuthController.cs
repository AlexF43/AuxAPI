using AuxAPI.Services;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

namespace AuxAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpotifyAuthController: ControllerBase
{
    private readonly SpotifyAuthService _spotifyAuthService;

    public SpotifyAuthController(SpotifyAuthService spotifyAuthService)
    {
        _spotifyAuthService = spotifyAuthService;
    }

    [HttpGet("hello")]
    public IActionResult HelloWorld()
    {
        var message = _spotifyAuthService.GetHelloWorld();
        return Ok(message);
    }
}