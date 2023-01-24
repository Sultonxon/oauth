
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace backend.Services;

public class JwtGenerator
{
    public readonly RsaSecurityKey _key;
    public JwtGenerator(string secretKey)
    {
        RSA privateRSA = RSA.Create();
        privateRSA.ImportRSAPrivateKey(Convert.FromBase64String(secretKey), out _);
        _key = new RsaSecurityKey(privateRSA);
    }

    public string CreateUserAuthToken(string userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.Sid, userId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, "NameIdentifier"),
            new Claim(ClaimTypes.UserData, "NameIdentifier"),
            new Claim(ClaimTypes.Name, "NameIdentifier"),
            new Claim(ClaimTypes.Email, "NameIdentifier"),
            new Claim(ClaimTypes.GivenName, "NameIdentifier"),
            new Claim(ClaimTypes.DateOfBirth, "NameIdentifier"),


        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = "MyApi",
            Issuer = "AuthService",
            Subject = new System.Security.Claims.ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(60),
            SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.RsaSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}