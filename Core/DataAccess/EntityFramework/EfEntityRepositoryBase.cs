using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Entities;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public IResult Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                try
                {
                    var addedEntity = context.Entry(entity);
                    addedEntity.State = EntityState.Added;
                    context.SaveChanges();
                    return new SuccessResult();
                }
                catch (Exception e)
                {
                    return new ErrorResult(e.Message);
                }

            }
        }

        public IResult Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                try
                {
                    var deletedEntity = context.Entry(entity);
                    deletedEntity.State = EntityState.Deleted;
                    context.SaveChanges();
                    return new SuccessResult();
                }
                catch (Exception e)
                {
                    return new ErrorResult(e.Message);
                }
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                var entity = context.Set<TEntity>().SingleOrDefault(filter);
                return entity;
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                var entities = filter == null
                        ? context.Set<TEntity>().ToList()
                        : context.Set<TEntity>().Where(filter).ToList();
                    return entities;
            }
        }

        public IResult Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                try
                {
                    var updatedEntity = context.Entry(entity);
                    updatedEntity.State = EntityState.Modified;
                    context.SaveChanges();
                    return new SuccessResult();
                }
                catch (Exception e)
                {
                    return new ErrorResult(e.Message);
                }
            }
        }
    }
}
