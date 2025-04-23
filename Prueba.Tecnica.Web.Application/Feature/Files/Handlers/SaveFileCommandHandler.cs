using MediatR;
using Prueba.Tecnica.Web.Application.Feature.Files.Commands;
using Prueba.Tecnica.Web.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Prueba.Tecnica.Web.Application.Feature.Files.Handlers
{
    public class SaveFileCommandHandler : IRequestHandler<SaveFileCommand, Guid>
    {
        private readonly IFileStorageService _storageService;

        public SaveFileCommandHandler(IFileStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<Guid> Handle(SaveFileCommand request, CancellationToken cancellationToken)
        {
            using var memoryStream = new MemoryStream();
            await request.File.CopyToAsync(memoryStream);
            
            var fileId = await _storageService.SaveFileAsync(request.File.FileName, request.File.ContentType, memoryStream.ToArray());
            return fileId;
        }
    }
}
