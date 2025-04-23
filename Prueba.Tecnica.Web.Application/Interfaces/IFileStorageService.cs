using Prueba.Tecnica.Web.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<Guid> SaveFileAsync(string fileName, string contentType, byte[] content);
        Task<FileDto> GetFileAsync(Guid fileId);
    }
}
