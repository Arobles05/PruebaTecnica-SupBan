using MediatR;
using Prueba.Tecnica.Web.Application.Dtos;
using Prueba.Tecnica.Web.Application.Feature.Files.Queries;
using Prueba.Tecnica.Web.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Application.Feature.Files.Handlers
{
    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, FileDto>
    {
        private readonly IFileStorageService _storageService;

        public GetFileQueryHandler(IFileStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<FileDto> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            return await _storageService.GetFileAsync(request.FileId);
        }
    }
}
