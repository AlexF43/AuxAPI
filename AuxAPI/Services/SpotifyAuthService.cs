using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AuxAPI.Models;
using SpotifyAPI.Web;

namespace AuxAPI.Services;

public class SpotifyAuthService
{
    private readonly IConfiguration _configuration;
    private HttpClient _httpClient;
    private readonly ILogger<SpotifyAuthService> _logger;
    
    public SpotifyAuthService(IConfiguration configuration, ILogger<SpotifyAuthService> logger)
    {
        _configuration = configuration;
        _httpClient = new HttpClient { };
        _logger = logger;

    }
    
    public string GetHelloWorld()
    {
        return "Hello World from SpotifyAuthService!";
    }

    // public void CreateUser(string AuthorisationCode)
    // {
    //     
    // }
    
    
    public async Task<UserAccessTokenReponse> GetUserAccessToken(string AuthorisationCode)
    {
        var path = "https://accounts.spotify.com/api/token";
        var clientId = _configuration["SpotifyClientID"];
        var clientSecret = _configuration["SpotifyClientSecret"];

        var accessTokenRequestData = new Dictionary<string, string>
        {
            {"grant_type", "authorization_code"},
            {"code", AuthorisationCode},
            {"redirect_uri", "http://localhost:5165/api/SpotifyAuth/spotify_callback"}
        };
    
        var request = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = new FormUrlEncodedContent(accessTokenRequestData)
        };

        var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", auth);
    
        var response = await _httpClient.SendAsync(request);
        var jsonString = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("got response: " + jsonString);
    
        if (response.IsSuccessStatusCode)
        {
            var accessTokenResponse = await response.Content.ReadFromJsonAsync<UserAccessTokenReponse>();
            return accessTokenResponse;
        }
    
        throw new ApplicationException($"Unable to create access token. Status: {response.StatusCode}, Message: {jsonString}");
    }
}