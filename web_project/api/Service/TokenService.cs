using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration _configuration;
        private  readonly SymmetricSecurityKey _secKey;
        public TokenService(IConfiguration configuration)
        {
            _configuration=configuration;
            _secKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
        }

        public string CreateToken(AppUser user)
        {
            var claims=new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),

            };
            var creds=new SigningCredentials(_secKey,SecurityAlgorithms.HmacSha512Signature);
            var tokendescriptior=new SecurityTokenDescriptor
            {
                Subject= new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials=creds,
                Issuer=_configuration["JWT:Issuer"],
                Audience=_configuration["JWT:Audience"]
            };
            
            var tokenHandler=new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateToken(tokendescriptior);
            return tokenHandler.WriteToken(token);
        }
    }
}