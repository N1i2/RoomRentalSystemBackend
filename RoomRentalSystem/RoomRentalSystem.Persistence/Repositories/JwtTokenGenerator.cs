using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using RoomRentalSystem.Domain.Entities;
using RoomRentalSystem.Domain.IRepositories;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RoomRentalSystem.Persistence.Options;

namespace RoomRentalSystem.Persistence.Repositories;

public class JwtTokenGenerator(IOptions<JwtOptions> options) : IJwtTokenGenerator
{
    public string GenerateToken(UserEntity user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
        };

        claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name)));
        var token = new JwtSecurityToken(
            issuer: options.Value.Issuer,
        audience: options.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(options.Value.ExpirationMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}