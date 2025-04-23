using MediatR;
using Prueba.Tecnica.Web.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Application.Feature.Files.Queries
{
    public record GetFileQuery(Guid FileId) : IRequest<FileDto>;
}
