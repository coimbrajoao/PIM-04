using Course.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Course.Services
{
    public class TokenService
    {
        public string GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.DateOfBirth, user.Datebirth.ToString()),
                new Claim("loginTimestamp", DateTime.UtcNow.ToString())
                
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1asadeqw523q4wxdjdsaojefwofjamskomqpwofjrqwrfqmocefqpqmceifposdkap"));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(expires: DateTime.Now.AddMinutes(40), claims: claims, signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}