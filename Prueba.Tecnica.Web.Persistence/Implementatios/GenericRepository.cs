using Microsoft.EntityFrameworkCore;
using Prueba.Tecnica.Web.Application.Interfaces;
using Prueba.Tecnica.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Tecnica.Web.Persistence.Implementatios
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly IAppDbContext _context;
        protected readonly IUserService _userService;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(IAppDbContext context,IUserService userService)
        {
            _context = context;
            _userService = userService;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = _userService.GetCurrentUsername(); 
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
