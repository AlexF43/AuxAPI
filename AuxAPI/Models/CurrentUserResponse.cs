namespace AuxAPI.Models;

public class CurrentUserResponse
{
    public string country { get; set; }
    public string display_name { get; set; }
    public string email { get; set; }
    public ExplicitContentSettings explicit_content { get; set; }
    public ExternalUrls external_urls { get; set; }
    public Followers followers { get; set; }
    public string href { get; set; }
    public string id { get; set; }
    public string product { get; set; }
    public string uri { get; set; }
}

public class ExplicitContentSettings
{
    public bool filter_enabled { get; set; }
    public bool filter_locked { get; set; }
}

public class ExternalUrls
{
    public string spotify { get; set; }
}

public class Followers
{
    public string href { get; set; }
    public int total { get; set; } 
}

public class Image
{
    public string url { get; set; }
    public string height { get; set; }
    public string width { get; set; }
}