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
    [Authorize]
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<FilesController> _logger;
        public FilesController(ILogger<FilesController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("upload")]
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

        [HttpGet("{fileId}")]
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
