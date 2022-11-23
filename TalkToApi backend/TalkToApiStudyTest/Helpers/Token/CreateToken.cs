using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Models.dto;

namespace TalkToApiStudyTest.Helpers.Token
{
    public class CreateToken
    {

        public  static TokenDTO BuildToken(ApplicationUser usuario)
        {
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
              new Claim(JwtRegisteredClaimNames.Sub, usuario.Id)
           };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("chave-api-jwt-minhas-tarefas"));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: exp,
                signingCredentials: sign);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Guid.NewGuid().ToString();
            var expRefreshToken = DateTime.UtcNow.AddHours(2);
            var tokenDTO = new TokenDTO { UserId = usuario.Id, Token = tokenString, Expiration = exp, ExpirationRefreshToken = expRefreshToken, RefreshToken = refreshToken };

            return tokenDTO;
        }
    }
}
