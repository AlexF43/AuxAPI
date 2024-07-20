using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AuxAPI.Models;
using SpotifyAPI.Web;

namespace AuxAPI.Services;

public class SpotifyAuthService(IConfiguration configuration, ILogger<SpotifyAuthService> logger)
{
    private readonly HttpClient _httpClient = new() { };

    public string GetHelloWorld()
    {
        return "Hello World from SpotifyAuthService!";
    }
    
    public async Task<UserAccessTokenReponse?> GetUserAccessToken(string authorisationCode)
    {
        var path = "https://accounts.spotify.com/api/token";
        var clientId = configuration["SpotifyClientID"];
        var clientSecret = configuration["SpotifyClientSecret"];

        var accessTokenRequestData = new Dictionary<string, string>
        {
            {"grant_type", "authorization_code"},
            {"code", authorisationCode},
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
        logger.LogInformation("got response: " + jsonString);
    
        if (response.IsSuccessStatusCode)
        {
            var accessTokenResponse = await response.Content.ReadFromJsonAsync<UserAccessTokenReponse>();
            return accessTokenResponse;
        }
    
        throw new ApplicationException($"Unable to create access token. Status: {response.StatusCode}, Message: {jsonString}");
    }
    
    public async Task<UserAccessTokenReponse?> RefreshUserAccessToken(string refreshToken)
    {
        var path = "https://accounts.spotify.com/api/token";
        var clientId = configuration["SpotifyClientID"];
        var clientSecret = configuration["SpotifyClientSecret"];

        var accessTokenRequestData = new Dictionary<string, string>
        {
            {"grant_type", "authorization_code"},
            {"refresh_token", refreshToken},
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
        logger.LogInformation("got response: " + jsonString);
    
        if (response.IsSuccessStatusCode)
        {
            var accessTokenResponse = await response.Content.ReadFromJsonAsync<UserAccessTokenReponse>();
            return accessTokenResponse;
        }
    
        throw new ApplicationException($"Unable to refresh access token. Status: {response.StatusCode}, Message: {jsonString}");
    }
}