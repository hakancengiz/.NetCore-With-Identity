using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;
using Core.Utilities.Results;

namespace Core.DataAccess
{
    public interface IEntityRepository<TEntity>
        where TEntity:class,IEntity,new()
    {
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        List<TEntity> GetList(Expression<Func<TEntity, bool>> filter=null);
        IResult Add(TEntity entity);
        IResult Update(TEntity entity);
        IResult Delete(TEntity entity);
    }
}
