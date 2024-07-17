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

    public SpotifyAuthController(SpotifyAuthService spotifyAuthService, ApplicationDbContext context)
    {
        _spotifyAuthService = spotifyAuthService;
        _context = context;
    }

    [HttpGet("hello")]
    public IActionResult HelloWorld()
    {
        var message = _spotifyAuthService.GetHelloWorld();
        return Ok(message);
    }
    
    [HttpPost("additem")]
    public async Task<IActionResult> AddTestItem([FromBody] string name)
    {
        var newItem = new TestItem { Name = name };
        _context.TestItems.Add(newItem);
        await _context.SaveChangesAsync();

        return Ok($"Added item with ID: {newItem.Id} and name: {newItem.Name}");
    }
}