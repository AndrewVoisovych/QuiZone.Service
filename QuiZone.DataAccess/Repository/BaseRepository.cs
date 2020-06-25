using Microsoft.EntityFrameworkCore;
using QuiZone.Common.GlobalErrorHandling;
using QuiZone.DataAccess.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace QuiZone.DataAccess.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        private readonly QuiZoneContext context;
        protected DbSet<T> entities;

        protected BaseRepository(QuiZoneContext context)
        {
            this.context = context;
        }

        public virtual Task<T> GetByIdAsync(int id)
        {
            var result = ComplexEntities.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);

            if (result.Result == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Об'єкту не знайдено");
            }

            return result;

        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return ComplexEntities.AsNoTracking().ToListAsync();
        }

        public virtual Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return ComplexEntities.AsNoTracking().Where(predicate).ToListAsync();

        }

        public virtual IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return ComplexEntities.Where(expression).AsNoTracking().AsQueryable();
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            return (await Entities.AddAsync(entity)).Entity;
        }

        public virtual async Task<T> RemoveByIdAsync(int id)
        {
            var entryToDelete = await Entities.FindAsync(id);

            if (entryToDelete == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Об'єкту не знайдено");
            }

            return Entities.Remove(entryToDelete).Entity;
        }

        public virtual T Update(T entity)
        {
            return Entities.Update(entity).Entity;
        }

        public virtual T UpdateWithIgnoreProperty<TProperty>(T entity, Expression<Func<T, TProperty>> ignorePropertyExpression)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.Entry(entity).Property(ignorePropertyExpression).IsModified = false;

            return entity;
        }


        // DbSet Entry
        protected virtual DbSet<T> Entities => entities ??= context.Set<T>();

        protected virtual IQueryable<T> ComplexEntities => Entities;

        public IQueryable<T> GetQueryable() => ComplexEntities;


    }
}

