using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace ShopNetCoreApi.Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly ShopDbContext _shopDbContext;
        private readonly DbSet<T> _entities;

        public RepositoryBase(ShopDbContext shopDbContext)
        {
            this._shopDbContext = shopDbContext;
            _entities = shopDbContext.Set<T>();
        }
        public virtual EntityEntry<T> Add(T entity)
        {
            return _entities.Add(entity);
        }

        public virtual bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return _shopDbContext.Set<T>().Count<T>(predicate) > 0; ;
        }

        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return _entities.Count(where);
        }

        public virtual EntityEntry<T> Delete(T entity)
        {
            return _entities.Remove(entity);
        }

        public virtual void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _entities.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _entities.Remove(obj);
        }

        public virtual IEnumerable<T> GetAll(string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _shopDbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsQueryable();
            }

            return _shopDbContext.Set<T>().AsQueryable();
        }

        public virtual IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _shopDbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }

            return _shopDbContext.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }

        public virtual IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            if (includes != null && includes.Count() > 0)
            {
                var query = _shopDbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? _shopDbContext.Set<T>().Where<T>(predicate).AsQueryable() : _shopDbContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public virtual T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _shopDbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }
            return _shopDbContext.Set<T>().FirstOrDefault(expression);
        }

        public virtual T GetSingleById(int id)
        {
            return _entities.Find(id);
        }

        public virtual void Update(T entity)
        {
            _entities.Attach(entity);
            _shopDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
