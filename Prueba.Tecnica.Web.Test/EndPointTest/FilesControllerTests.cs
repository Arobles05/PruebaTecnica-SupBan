using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Prueba.Tecnica.Web.API.Controllers;
using Prueba.Tecnica.Web.Application.Feature.Files.Commands;

namespace Prueba.Tecnica.Web.Test.EndPointTest
{
    /// <summary>
    /// Tests para el controlador de archivos
    /// </summary>
    [TestFixture]
    public class FilesControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<ILogger<FilesController>> _loggerMock;
        private FilesController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<FilesController>>();
            _controller = new FilesController(_loggerMock.Object, _mediatorMock.Object);
        }

        private IFormFile CreateMockFormFile(string fileName = "test.txt", string contentType = "text/plain", string content = "Hello, World!")
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            return new FormFile(stream, 0, stream.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
        }
        /// <summary>
        /// Este test verifica que el controlador devuelva un OkObjectResult con un FileId cuando se sube un archivo valido.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Upload_ValidFile_ReturnsOkWithFileId()
        {
            // Arrange  
            var file = CreateMockFormFile();

            var fakeGuid = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<SaveFileCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeGuid);

            // Act  
            var result = await _controller.Upload(file);

            // Assert  
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = (OkObjectResult)result;
            Assert.IsNotNull(okResult.Value);


            dynamic response = okResult.Value;
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(fakeGuid, response.Data.FileId);
        }
        /// <summary>
        /// Este test verifica que el controlador devuelva un BadRequest cuando se sube un archivo nulo.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Upload_InvalidFile_ReturnsBadRequest()
        {
            // Arrange  
            var file = CreateMockFormFile();

            IFormFile nullFile = null;

            // Act  
            var result = await _controller.Upload(nullFile);

            // Assert  
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Este test verifica que el controlador devuelva un BadRequest cuando el archivo no es valido.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Upload_MediatorReturnsEmptyGuid_ReturnsBadRequest()
        {
            // Arrange  
            var file = CreateMockFormFile();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<SaveFileCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Guid.Empty);

            // Act  
            var result = await _controller.Upload(file);

            // Assert  
            var badResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual("Error al guardar el archivo.", badResult.Value);
        }
    }
}
