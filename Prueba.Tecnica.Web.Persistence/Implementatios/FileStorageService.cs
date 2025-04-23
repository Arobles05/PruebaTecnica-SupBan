using Azure.Core;
using MediatR;
using Prueba.Tecnica.Web.Application.Dtos;
using Prueba.Tecnica.Web.Domain.Models;
using Prueba.Tecnica.Web.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Infraestructure.Implemantations
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _basePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
        private readonly IGenericRepository<FileEntity> _fileRepository;

        public FileStorageService(IGenericRepository<FileEntity> fileRepository)
        {
            _fileRepository = fileRepository;

            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
        }

        public async Task<Guid> SaveFileAsync(string fileName, string contentType, byte[] content)
        {
            var id = Guid.NewGuid();
            var filePath = Path.Combine(_basePath, id.ToString());

            await File.WriteAllBytesAsync(filePath, content);

            var fileEntity = new FileEntity
            {
                Id = id,
                FileName = fileName,
                ContentType = contentType,
                FilePath = filePath,
                Content = content,
            };

            await _fileRepository.AddAsync(fileEntity);
            await _fileRepository.SaveChangesAsync();

            return id;
        }

        public async Task<FileDto?> GetFileAsync(Guid fileId)
        {

            var fileEntity = await _fileRepository.GetByIdAsync(fileId);
            if (fileEntity == null)
                return null!;

            return new FileDto
            {
                FileName = fileEntity.FileName,
                ContentType = fileEntity.ContentType,
                Content = fileEntity.Content
            };

        }

      
    }
}
