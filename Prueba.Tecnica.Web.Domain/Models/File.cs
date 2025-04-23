using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Domain.Models
{
    public class FileEntity
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public string FilePath { get; set; } = null!;  // Ruta física o nombre de archivo almacenado  
    }
}
