using ChatApplication.Core.DTOs;
using ChatApplication.Core.Entities;

namespace ChatApplication.Core.Service;

public interface IBaseService<TEntityView, TEntityService>
    where TEntityView : class, IBaseEntity, new()
    where TEntityService : class, IBaseModelDto, new()
{
    #region Create

    Task<TEntityService> AddAsync(TEntityView entity, CancellationToken cancellationToken);

    #endregion

    #region Read

    Task<TEntityService> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<List<TEntityService>> GetAll(CancellationToken cancellationToken);

    #endregion

    #region Update

    Task<TEntityService> Update(TEntityView entity, CancellationToken cancellationToken);

    #endregion

    #region Delete

    Task<bool> Delete(int id, CancellationToken cancellationToken);

    #endregion
}