using System.Linq.Expressions;
using ChatApplication.Core.Entities;
using ChatApplication.Core.Repositories;
using ChatApplication.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Repository.Repositories;

public class BaseRepository<TEntity> : IBaseRepositories<TEntity> where TEntity : class, IBaseEntity, new()
{
    #region Definition

    private readonly BaseContext _context;
    private readonly DbSet<TEntity> _dbSet;

    #endregion

    #region Constructor

    public BaseRepository(BaseContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    #endregion

    #region Create
    #region AddAsync
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var addedEntity = await _dbSet.AddAsync(entity);
        addedEntity.Entity.CreatedAt = DateTime.Now;
        addedEntity.Entity.ChangedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return addedEntity.Entity;
    }
    #endregion
    #endregion

    #region Read

    #region GetByIdAsync
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        var getEntityById = await _dbSet.Where(e => e.Id == id).SingleOrDefaultAsync();
        return getEntityById;
    }
    #endregion

    #region GetAll
    public IQueryable<TEntity> GetAll(bool includeDeleted = false)
    {
        IQueryable<TEntity> queryableEntities;
        if (!includeDeleted)
        {
            queryableEntities = _dbSet.Where(e => !e.IsDeleted).AsNoTracking().AsQueryable();
            return queryableEntities;
        }

        queryableEntities = _dbSet.AsNoTracking().AsQueryable();
        return queryableEntities;
    }
    #endregion

    #region Where
    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter)
    {
        var query = _dbSet.Where(filter);
        return query;
    }
    #endregion

    #region AnyAsync
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
    {
        var checkEntityExist = await _dbSet.AnyAsync(filter);
        return checkEntityExist;
    }
    #endregion

    #endregion

    #region Update

    #region Update
    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
    #endregion

    #endregion

    #region Delete

    #region Delete
    public void Update(TEntity entity)
    {
        entity.ChangedAt = DateTime.Now;
        _dbSet.Update(entity);
    }
    #endregion

    #endregion




}