using Microsoft.AspNetCore.Http;
using Prueba.Tecnica.Web.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Persistence.Implementatios
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
        }
    }
}
