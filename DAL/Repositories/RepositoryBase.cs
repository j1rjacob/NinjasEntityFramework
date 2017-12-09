using Contracts.Repositories;
using DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        internal DataContext context;

        internal IDbSet<T> dbSet;

        protected RepositoryBase(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            this.dbSet.Add(entity);
        }

        public virtual IEnumerable<T> AddRange(IEnumerable<T> entitiesToAdd)
        {
            return ((DbSet<T>)this.dbSet).AddRange(entitiesToAdd);
        }

        public virtual IEnumerable<T> RemoveRange(IEnumerable<T> entitiesToDelete)
        {
            return ((DbSet<T>)this.dbSet).RemoveRange(entitiesToDelete);
        }

        public virtual void Truncate(T entity)
        {
            this.context.Database.ExecuteSqlCommand(string.Format("TRUNCATE TABLE {0}", typeof(T).Name), new object[0]);
        }

        public virtual void Update(T entity)
        {
            this.dbSet.Attach(entity);
            this.context.Entry<T>(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            this.dbSet.Remove(entity);
        }

        public virtual void Delete(object id)
        {
            T entity = this.dbSet.Find(new object[]
            {
                id
            });
            this.Delete(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> enumerable = this.dbSet.Where(where).AsEnumerable<T>();
            foreach (T current in enumerable)
            {
                this.dbSet.Remove(current);
            }
        }

        public virtual T GetById(int id)
        {
            return this.dbSet.Find(new object[]
            {
                id
            });
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this.dbSet.ToList<T>();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return this.dbSet.Where(where).ToList<T>();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return this.dbSet.Where(where).FirstOrDefault<T>();
        }

        public virtual void Commit()
        {
            this.context.SaveChanges();
        }

        public virtual void Dispose()
        {
            this.context.Dispose();
        }
    }
}
