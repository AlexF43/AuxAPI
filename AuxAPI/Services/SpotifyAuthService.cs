using SpotifyAPI.Web;

namespace AuxAPI.Services;

public class SpotifyAuthService
{
    private readonly IConfiguration _configuration;

    public SpotifyAuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GetHelloWorld()
    {
        return "Hello World from SpotifyAuthService!";
    }
}