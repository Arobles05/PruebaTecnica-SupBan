using Microsoft.EntityFrameworkCore;
using Prueba.Tecnica.Web.Application.Interfaces;
using Prueba.Tecnica.Web.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Persistence.Implementatios
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IAppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(IAppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
