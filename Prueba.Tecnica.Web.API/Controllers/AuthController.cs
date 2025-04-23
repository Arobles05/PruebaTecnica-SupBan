using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Prueba.Tecnica.Web.Application.Feature.Auth.Login.Command;
using Prueba.Tecnica.Web.Application.Feature.Files.Queries;
using Prueba.Tecnica.Web.Application.ResponseModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prueba.Tecnica.Web.API.Controllers
{
    /// <summary>
    /// Controlador para la autenticacion de usuarios
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        public AuthController( ILogger<AuthController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
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
        ///        "username": "prueba",
        ///        "password": "tecnica"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Cuando ha obtenido la informacion de forma sastifactoria</response>
        /// <response code="500">Si ha occurrido algun error en la ejecucion.</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            _logger.LogInformation($"Intentando iniciar sesion con el usuario: {request.Username}");
            if (request.Username == "prueba" && request.Password == "tecnica")
            {
                var response = await _mediator.Send(request);

                _logger.LogInformation($"Inicio de sesion exitoso para el usuario: {request.Username}");
                return Ok(ApiResponse<LoginResponse>.CreateSuccessResponse(response));
            }
            
            return Unauthorized();
        }

     
    }
}
