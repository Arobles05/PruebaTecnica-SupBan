//using Prueba.Tecnica.Web.Infraestructure.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Prueba.Tecnica.Web.Infraestructure.Implemantations
//{
//    public class FileStorageService : IFileStorageService
//    {
//        private readonly string _basePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

//        public async Task<Guid> SaveFileAsync(string fileName, string contentType, byte[] content)
//        {
//            var id = Guid.NewGuid();
//            var filePath = Path.Combine(_basePath, id.ToString());

//            if (!Directory.Exists(_basePath))
//                Directory.CreateDirectory(_basePath);

//            await File.WriteAllBytesAsync(filePath, content);

//            // Aquí deberías guardar metadata (fileName, contentType) en DB con el id, por simplicidad lo omitimos  

//            return id;
//        }

//        public async Task<(string FileName, string ContentType, byte[] Content)?> GetFileAsync(Guid fileId)
//        {
//            var filePath = Path.Combine(_basePath, fileId.ToString());

//            if (!File.Exists(filePath))
//                return null;

//            var content = await File.ReadAllBytesAsync(filePath);

//            // Obtener metadata del archivo desde base de datos  
//            // Aquí devolveremos valores de ejemplo:  
//            return ("example.txt", "text/plain", content);
//        }
//    }
//}
