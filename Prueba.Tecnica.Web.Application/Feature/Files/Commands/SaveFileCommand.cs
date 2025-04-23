using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Application.Feature.Files.Commands
{
    public record SaveFileCommand(IFormFile File) : IRequest<Guid>;
}
