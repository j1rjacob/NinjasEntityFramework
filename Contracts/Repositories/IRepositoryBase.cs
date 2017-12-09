using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Contracts.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T entity);

        IEnumerable<T> AddRange(IEnumerable<T> entitiesToAdd);

        IEnumerable<T> RemoveRange(IEnumerable<T> entitiesToDelete);

        void Truncate(T entity);

        void Commit();

        void Delete(Expression<Func<T, bool>> where);

        void Delete(object id);

        void Delete(T entity);

        void Dispose();

        T Get(Expression<Func<T, bool>> where);

        IEnumerable<T> GetAll();

        T GetById(int id);

        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        void Update(T entity);
    }
}
