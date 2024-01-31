namespace SportData.Data.Models.Authenticate;

public class TokenRequestModel
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}