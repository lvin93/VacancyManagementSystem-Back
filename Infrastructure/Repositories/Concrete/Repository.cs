using Application;
using Common.Entities;
using Domain.Common.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : AuditableEntity
    {
        private readonly VacancyManagementDbContext _context;
        public Repository(VacancyManagementDbContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>().Where(x => x.IsDeleted == false);
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includePropertie in includeProperties)
                {
                    query = query.Include(includePropertie);
                }
            }
            return query.AsQueryable();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => { _context.Set<T>().Update(entity); });
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            var entityToDelete = await _context.Set<T>().FindAsync(entity.Id);
            entityToDelete.IsDeleted = true;
            await UpdateAsync(entityToDelete);
            return entityToDelete;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>().Where(x => x.IsDeleted == false);
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includePropertie in includeProperties)
                {
                    query = query.Include(includePropertie);
                }
            }
            return await query.SingleOrDefaultAsync();
        }

    }
}
