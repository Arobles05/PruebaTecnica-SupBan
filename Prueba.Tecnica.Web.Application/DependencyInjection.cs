using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Prueba.Tecnica.Web.Application.Feature.Files.Commands;
using Prueba.Tecnica.Web.Application.Validatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(a => a.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient<IValidator<SaveFileCommand>, SaveFileCommandValidator>();
        }
    }
}
