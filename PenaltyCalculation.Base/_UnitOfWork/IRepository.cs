using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PenaltyCalculation.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(Expression<Func<TEntity, bool>> where);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        bool Exists(object primaryKey);
        IEnumerable<TEntity> Get();
        TEntity Get(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetAll();
        TEntity GetByID(object id);
        TEntity GetFirst(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        IQueryable<TEntity> GetManyQueryable(Expression<Func<TEntity, bool>> where);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate, params string[] include);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}
