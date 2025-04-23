using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba.Tecnica.Web.Application.Feature.Files.Commands;
using Prueba.Tecnica.Web.Application.Feature.Files.Queries;


namespace Prueba.Tecnica.Web.API.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var fileId = await _mediator.Send(new SaveFileCommand(file));
            return Ok(new { FileId = fileId });
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> Download(Guid fileId)
        {
            var fileDto = await _mediator.Send(new GetFileQuery(fileId));
            if (fileDto == null)
                return NotFound();

            return File(fileDto.Content, fileDto.ContentType, fileDto.FileName);
        }
    }
}
