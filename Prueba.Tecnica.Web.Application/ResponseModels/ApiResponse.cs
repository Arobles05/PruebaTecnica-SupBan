using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Application.ResponseModels
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public ApiResponse()
        {
            Success = true;
            Message = string.Empty;
            Data = default(T);
            Errors = new List<string>();
        }
    }
}
