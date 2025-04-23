using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Prueba.Tecnica.Web.Application.Feature.Auth.Login.Command;
using Prueba.Tecnica.Web.Application.ResponseModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Application.Feature.Auth.Login.Handlers
{
    public class LoginCommandHandler:IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }   

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var expires = DateTime.Now.AddHours(1);
            var token = GenerateJwtToken(request.Username, expires);

            return new LoginResponse
            {
                Token = token,
                Expiration = expires
            };
        }

        private string GenerateJwtToken(string username, DateTime expires)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                claims: new[] { new Claim(ClaimTypes.Name, username) },
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
