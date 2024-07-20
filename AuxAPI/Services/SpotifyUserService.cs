using System.Net.Http.Headers;
using System.Text;
using AuxAPI.Models;
using Microsoft.AspNetCore.Authentication;

namespace AuxAPI.Services;

public class SpotifyUserService (IConfiguration configuration, ILogger<SpotifyAuthService> logger)
{
    private readonly HttpClient _httpClient = new() { };
    
    public async Task<CurrentUserResponse?> GetCurrentUserDetails(string authenticationToken)
    {
        var path = "https://api.spotify.com/v1/me";
        
        var request = new HttpRequestMessage(HttpMethod.Get, path) { };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
    
        var response = await _httpClient.SendAsync(request);
        var jsonString = await response.Content.ReadAsStringAsync();
        logger.LogInformation("got response: " + jsonString);
    
        if (response.IsSuccessStatusCode)
        {
            var accessTokenResponse = await response.Content.ReadFromJsonAsync<CurrentUserResponse>();
            return accessTokenResponse;
        }
    
        throw new ApplicationException($"Unable to create user. Status: {response.StatusCode}, Message: {jsonString}");
    }
    
}