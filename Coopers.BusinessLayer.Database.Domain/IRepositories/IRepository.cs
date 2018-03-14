using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.Domain.IRepositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {

        #region SYNC METHODS

        TEntity GetEntityById(TKey id, bool bIsAsTrackable = false);
        IQueryable<TEntity> GetFilteredEntities(bool bIsAsTrackable = false);
        int Create(TEntity entity);
        int Update(TEntity entity);
        int DeleteEntityByID(TKey id);

        #endregion


        #region ASYNC METHODS

        Task<int> CreateEntityAsync(TEntity entity);

        Task<int> DeleteEntityAsync(TEntity entity);

        Task<TEntity> GetEntityByIdAsync(TKey id, bool bIsAsTrackable = false);

        Task<int> UpdateEntityAsync(TEntity entity);

        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filterExpression);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filterExpression);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> filterExpression);

        #endregion

    }
}
