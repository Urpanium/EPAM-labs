using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using T5.Models;

namespace T5.Service
{
    public interface IRepository
    {
        ApplicationDbContext Context { get; }
        
        IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> predicate = null) where TEntity: class;
        
        TEntity SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity: class;

        TEntity Find<TEntity>(int? id) where TEntity: class;
        
        void Add<TEntity>(TEntity entity) where TEntity: class;
        
        void Remove<TEntity>(TEntity entity) where TEntity: class;
        
        void Save();
    }
}