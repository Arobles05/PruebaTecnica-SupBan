using Microsoft.EntityFrameworkCore;
using Prueba.Tecnica.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Application.Interfaces
{
    public interface IAppDbContext
    {
            DbSet<T> Set<T>() where T : class;  
            Task<int> SaveChangesAsync();
    }
}
