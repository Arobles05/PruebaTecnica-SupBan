using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prueba.Tecnica.Web.Application.Interfaces;
using Prueba.Tecnica.Web.Infraestructure.Implemantations;
using Prueba.Tecnica.Web.Infraestructure.Interfaces;
using Prueba.Tecnica.Web.Persistence.Implementatios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Prueba.Tecnica.Web.API")));
            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());
            services.AddScoped<IFileStorageService, FileStorageService>();
        }
    }
}
