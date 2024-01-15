namespace SportData.Data.Models.Authentication;

public class TokenModel
{
    public string AccessToken { get; set; }

    /// <summary>
    /// Refresh token
    /// </summary>
    /// <example>asd</example>
    public string RefreshToken { get; set; }
}