using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookCase.Domain.User;
using BookCase.Repository.User;

namespace BookCase.Service.Authenticate
{
    public class AuthenticateService
    {
        private UserRepository Repository { get; set; }

        private IConfiguration Configuration { get; set; }

        public AuthenticateService(UserRepository repository, IConfiguration configuration)
        {
            this.Repository = repository;
            this.Configuration = configuration;
        }

        public string AuthenticateUser(string mail, string password)
        {
            var user = this.Repository.GetUserByEmail(mail);

            if (user == null)
                return null;

            if (password != user.Password)
                return null;

            return CreateToken(user);
        }

        private string CreateToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(this.Configuration["Token:Secret"]);

            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Mail));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Audience = "BookCase-API",
                Issuer = "BookCase-API"
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);

            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
