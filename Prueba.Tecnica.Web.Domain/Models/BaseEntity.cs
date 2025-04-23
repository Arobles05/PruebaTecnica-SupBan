using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Domain.Models
{
    public abstract class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
