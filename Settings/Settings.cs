using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuarioAPI.Models;

namespace UsuarioAPI.Settings
{
    public class Settings
    {
        public string PrivateKey = "13x#434af6543gd43&%1654683135p675!@#698dfg8$#%49jhk8234%&@5298qsad3";

        public string GenerateToken(MyUser user) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(PrivateKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),

                // Expira em 1 Hr o token.
                Expires = DateTime.UtcNow.AddMinutes(1),

                // Array de Byts gerados através da PrivateKey.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
