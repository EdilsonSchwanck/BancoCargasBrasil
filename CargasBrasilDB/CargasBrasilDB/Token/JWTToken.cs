using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CargasBrasilDB.Token
{
    public class JWTToken
    {
        public static object GerarToken(string cnpj)
        {
            var claims = new List<Claim>
            {
                new("CNPJ", cnpj.Trim())
            };

            var expires = DateTime.Now.AddHours(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ProjetoCargasBrasil2024"));
            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                expires: expires,
                claims: claims
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
            return new
            {
                acess_token = token,
                expiration = expires
            };
        }
    }
}
