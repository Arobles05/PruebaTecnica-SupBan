using MediatR;
using Prueba.Tecnica.Web.Application.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Application.Feature.Auth.Login.Command
{
    public record LoginCommand(string Username, string Password) : IRequest<LoginResponse>
    {
      
    }
    
}
