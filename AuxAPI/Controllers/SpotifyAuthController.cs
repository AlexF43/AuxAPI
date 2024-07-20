using AuxAPI.Data;
using AuxAPI.Models;
using AuxAPI.Services;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

namespace AuxAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpotifyAuthController: ControllerBase
{
    private readonly SpotifyAuthService _spotifyAuthService;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SpotifyAuthController> _logger;
    private readonly SpotifyUserService _spotifyUserService;

    public SpotifyAuthController(SpotifyAuthService spotifyAuthService, ApplicationDbContext context, ILogger<SpotifyAuthController> logger, SpotifyUserService spotifyUserService)
    {
        _spotifyAuthService = spotifyAuthService;
        _spotifyUserService = spotifyUserService;
        _context = context;
        _logger = logger;
        
    }
    
    [HttpPost("additem")]
    public async Task<IActionResult> AddTestItem([FromBody] string name)
    {
        var newItem = new TestItem { Name = name };
        _context.TestItems.Add(newItem);
        await _context.SaveChangesAsync();

        return Ok($"Added item with ID: {newItem.Id} and name: {newItem.Name}");
    }
    

    [HttpGet("spotify_callback")]
    public async Task<IActionResult> SpotifyCallback(
        [FromQuery] string code,
        [FromQuery] string state
    )
    {
        _logger.LogInformation($"Received callback with code: {code} and state: {state}");
    
        try
        {
            var response = await _spotifyAuthService.GetUserAccessToken(code);
            _logger.LogInformation("Successfully obtained access token");
            var user = await _spotifyUserService.GetCurrentUserDetails(response.access_token);
            return Ok(new { message = "got user", username = user.display_name });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error obtaining access token");
            return BadRequest(new { error = "Failed to obtain access token", details = ex.Message });
        }
    }
}