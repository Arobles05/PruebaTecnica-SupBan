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
        public required T? Data { get; set; }
        public required string? ErrorMessage { get; set; }

        public static ApiResponse<T> CreateSuccessResponse(T data)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                ErrorMessage = null
            };
        }

        public static ApiResponse<T> CreateErrorResponse(string errorMessage)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Data = default,
                ErrorMessage = errorMessage
            };
        }
    }
}
