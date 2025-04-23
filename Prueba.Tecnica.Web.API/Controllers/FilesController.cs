using Azure.Core;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba.Tecnica.Web.API.Middlewares.Behaviors;
using Prueba.Tecnica.Web.Application.Feature.Files.Commands;
using Prueba.Tecnica.Web.Application.Feature.Files.Queries;
using Prueba.Tecnica.Web.Application.Validatos;


namespace Prueba.Tecnica.Web.API.Controllers
{
    /// <summary>
    /// Controlador para la subida y descarga de archivos
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<FilesController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        public FilesController(ILogger<FilesController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }


        /// <summary>
        ///  Endpoint para subir un archivo
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <response code="200">Cuando ha procesado la informacion de forma sastifactoria</response>
        /// <response code="400">Si ha occurrido algun error.</response>
        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            _logger.LogInformation($"Subiendo Archivo: {file.FileName}");

            var command = new SaveFileCommand(file);
            var validator = new SaveFileCommandValidator();

            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var fileId = await _mediator.Send(command);
            return Ok(new { FileId = fileId });
        }

        /// <summary>
        ///  Endpoint para obtener el Archivo mediante el Guid
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        /// <response code="200">Cuando ha procesado la informacion de forma sastifactoria</response>
        /// <response code="400">Si ha occurrido algun error.</response>
        [HttpGet("{fileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Download(Guid fileId)
        {
            _logger.LogInformation("Descargando Archivo");
            var fileDto = await _mediator.Send(new GetFileQuery(fileId));
            if (fileDto == null)
                return NotFound();

            return File(fileDto.Content, fileDto.ContentType, fileDto.FileName);
        }
    }
}
