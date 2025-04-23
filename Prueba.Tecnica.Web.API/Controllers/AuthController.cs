using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Prueba.Tecnica.Web.Application.Feature.Auth.Login.Command;
using Prueba.Tecnica.Web.Application.ResponseModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prueba.Tecnica.Web.API.Controllers
{
    /// <summary>
    /// Controlador para la autenticacion de usuarios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// EndPont para simular el login para la autentificacion 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Retorna  el token de acceso tipo Bearer</returns>
        /// <remarks>
        /// Ejemplo del parametro de consulta:
        ///
        ///     POST 
        ///     {
        ///        "username": "usuario",
        ///        "password": "password"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Cuando ha obtenido la informacion de forma sastifactoria</response>
        /// <response code="500">Si ha occurrido algun error en la ejecucion.</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] LoginCommand request)
        {
        
            if (request.Username == "usuario" && request.Password == "password")
            {
                var expires = DateTime.Now.AddHours(1);
                var token = GenerateJwtToken(request.Username, expires);

                var response = new LoginResponse
                {
                    Token = token,
                    Expiration = expires
                };
                return Ok(ApiResponse<LoginResponse>.CreateSuccessResponse(response));
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(string username,DateTime expires)
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
