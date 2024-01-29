namespace SportData.Data.Models.Authentication;

public class TokenInputModel
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}