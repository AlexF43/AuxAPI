namespace AuxAPI.Models;

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string AuthorisationCode { get; set; }
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiry { get; set; }
}