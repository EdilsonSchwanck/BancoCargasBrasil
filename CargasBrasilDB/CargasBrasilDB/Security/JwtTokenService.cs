using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CargasBrasilDB.Models.DTOs;
using CargasBrasilDB.Models.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CargasBrasilDB.Security
{
    public interface ITokenService
    {
        TokenResponse CreateDriverToken(Driver driver);
        TokenResponse CreateAdminToken(string usuario);
    }

    public sealed class JwtTokenService : ITokenService
    {
        private readonly JwtSettings _settings;

        public JwtTokenService(IOptions<JwtSettings> options) => _settings = options.Value;

        public TokenResponse CreateDriverToken(Driver driver) => Create(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, driver.Id.ToString()),
            new Claim(AppClaims.Cpf, driver.Cpf),
            new Claim(ClaimTypes.Role, Roles.Motorista)
        });

        public TokenResponse CreateAdminToken(string usuario) => Create(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario),
            new Claim(ClaimTypes.Role, Roles.Admin)
        });

        private TokenResponse Create(IEnumerable<Claim> claims)
        {
            var expires = DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims.Append(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())),
                expires: expires,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new TokenResponse(new JwtSecurityTokenHandler().WriteToken(token), expires);
        }
    }
}
