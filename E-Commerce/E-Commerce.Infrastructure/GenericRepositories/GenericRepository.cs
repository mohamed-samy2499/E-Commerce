using E_Commerce.Domain.BaseEntities;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.ApplicationDbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.GenericRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<ApplicationUser>? _userManager;

        public GenericRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public GenericRepository(
            ApplicationDbContext appDbContext,
            UserManager<ApplicationUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }


        public virtual async Task<T> CreateAsync(T entity, string loggedInUserId)
        {
            entity.CreatedBy = loggedInUserId;
            await _appDbContext.Set<T>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity, string loggedInUserId)
        {
            entity.UpdatedBy = loggedInUserId;
            _appDbContext.Entry(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<string> DeleteAsync(int id, string loggedInUserId)
        {
            var entity = await _appDbContext.Set<T>().FindAsync(id);

            if (entity != null)
            {
                entity.DeletedBy = loggedInUserId;
                entity.DeletedDate = DateTime.Now;
                entity.IsDeleted = true;
                //_appDbContext.Entry(entity).State = EntityState.Deleted;
                await _appDbContext.SaveChangesAsync();
                return "success";
            }
            return "Fails";
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>[]? includes = null,
                                        Expression<Func<T, bool>>[]? filters = null
            )
        {
            IQueryable<T> query = _appDbContext.Set<T>();

            // Apply filter
            if (filters != null)
            {
                foreach (var filterExpression in filters)
                    query = query.Where(filterExpression);
            }

            // Apply includes
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();

        }

        public virtual async Task<T> GetByIdAsync(int id, Expression<Func<T, object>>[]? includes = null)
        {
            IQueryable<T> query = _appDbContext.Set<T>();//.Where(x => x.IsDeleted == false);
            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var entity = await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
                return entity;
            return null;
        }



    }
}
