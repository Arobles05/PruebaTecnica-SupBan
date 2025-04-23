using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Domain.Models
{
    public class FileEntity: BaseEntity
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public Guid CodeFile { get; set; }
        public byte[] Content { get; set; }
    }
}
