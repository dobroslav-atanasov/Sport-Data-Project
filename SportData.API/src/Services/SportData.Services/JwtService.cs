namespace SportData.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using SportData.Common.Constants;
using SportData.Data.Models.Authentication;
using SportData.Data.Models.Entities.SportData;
using SportData.Services.Interfaces;

public class JwtService : IJwtService
{
    private readonly IConfiguration configuration;

    public JwtService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string GenerateRefreshToken()
    {
        var number = new byte[64];
        using var range = RandomNumberGenerator.Create();
        range.GetBytes(number);
        var refreshToken = Convert.ToBase64String(number);

        return refreshToken;
    }

    public TokenModel GenerateToken(User user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Email, user.Email),
            //new(ClaimTypes.DateOfBirth, user.BirthDate.HasValue ? user.BirthDate.Value.ToString("dd.MM.yyyy") : null),
            //new(ClaimTypes.Surname, user.LastName),
        };

        foreach (var role in roles)
        {
            claims.Add(new(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration[GlobalConstants.JWT_KEY]));

        var token = new JwtSecurityToken(
            issuer: this.configuration[GlobalConstants.JWT_ISSUER],
            audience: this.configuration[GlobalConstants.JWT_AUDIENCE],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(this.configuration[GlobalConstants.JWT_TOKEN_VALIDITY_IN_MINUTES])),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return new TokenModel
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo,
        };
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration[GlobalConstants.JWT_KEY]));

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidIssuer = this.configuration[GlobalConstants.JWT_ISSUER],
                ValidAudience = this.configuration[GlobalConstants.JWT_AUDIENCE],
                IssuerSigningKey = key
            }, out SecurityToken validatedToken);

            return claimsPrincipal;
        }
        catch (Exception ex)
        {
            throw new BadHttpRequestException("Invalid token!");
        }
    }
}