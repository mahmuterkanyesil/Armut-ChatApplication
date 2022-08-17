using System.Linq.Expressions;
using ChatApplication.Core.Entities;

namespace ChatApplication.Core.Repositories;

public interface IBaseRepositories<TEntity> where TEntity : class, IBaseEntity, new() 
{
    #region Create

    Task<TEntity> AddAsync(TEntity entity);

    #endregion

    #region Read

    Task<TEntity?> GetByIdAsync(int id);

    IQueryable<TEntity> GetAll(bool includeDeleted = false);

    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);

    #endregion

    #region Update

    void Update(TEntity entity);

    #endregion

    #region Delete

    void Delete(TEntity entity);

    #endregion
}