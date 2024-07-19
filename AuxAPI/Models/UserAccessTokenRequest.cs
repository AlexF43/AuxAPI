namespace AuxAPI.Models;

public class UserAccessTokenRequest
{
    public string grant_type { get; set; }
    public string code { get; set; }
    public string redirect_uri { get; set; }

    public UserAccessTokenRequest(string grantType, string code, string redirectUri)
    {
        grant_type = grantType;
        this.code = code;
        redirect_uri = redirectUri;
    }
}